using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	[SerializeField] private LevelController levelController;

	private GameObject[] movements;
	private Movement selectedMovement;

	// Use this for initialization
	void Start () {
		ReloadMovements();
        Debug.Log("B");
    }

    void ReloadMovements() {
		Vector3 posicao = GameObject.Find ("MovementController").transform.position;
		GameObject randomMovement = this.movements [Random.Range (0, this.movements.Length)];
		Movement newMovement = Instantiate (randomMovement, posicao, Quaternion.identity).GetComponent<Movement> ();
        //newMovement.GetComponent<SpriteRenderer>().sortingLayerName = "GameElements";
        Debug.Log("Active movement is " + newMovement.name);

    }

    //Creates an array with all movements from 'Resources' folder
    public void LoadMovements(int levelNumber) {
		this.movements = Resources.LoadAll<GameObject> ("Prefabs/Movements");
        Debug.Log("C");
    }

    public bool SelectMovement(Movement mov, Unit[] units) {
		//Selects movement if it is not selected yet or if it has changed
		if (mov.Equals (this.selectedMovement) == false) {
			Debug.Log ("Selecionando movimento...");
			this.selectedMovement = mov;
		} else {
			//When the player confirms his choice and there are selected units, the movement begins
			if (units[0] == null) {
				Debug.LogError ("Selecione pelo menos uma unidade!");
			} else {
				Debug.Log ("canMove = true");
				return true;
			}
		}

		return false;
	}

	public void MoveUnits(Unit[] units) {

		Debug.Log("Executando movemento!");

		for (int step = 0; step < 4; step++) {
			foreach (Unit unit in units) {
				if (unit != null) {
					this.selectedMovement.Move (step, unit);
				}
			}
		}

		//Destroys the used movement object
		Destroy (selectedMovement.gameObject);
		//Instantiates a new movement
		ReloadMovements();
		//Clears the variables
		this.selectedMovement = null;
	}
}
