using UnityEngine;

public class MovementController : MonoBehaviour {

	[SerializeField] private LevelController levelController;

	private GameObject[] movementList;
    private Movement[] availableMovements;
	private Movement selected;

	// Use this for initialization
	void Start () {
		ReloadMovements();
    }

    //Creates an array with all movements from 'Resources' folder
    public void LoadMovements(int levelNumber)
    {
        this.movementList = Resources.LoadAll<GameObject>("Prefabs/Movements");
    }

    void ReloadMovements()
    {
		Vector3 posicao = GameObject.Find ("MovementController").transform.position;
		GameObject randomMov = this.movementList [Random.Range (0, this.movementList.Length)];
		//Movement newMovement = 
        Instantiate (randomMov, posicao, Quaternion.identity).GetComponent<Movement> ();
    }

    public bool SelectMovement(Movement mov, Unit[] units)
    {
		//Selects movement if it is not selected yet or if it has changed
		if (mov.Equals (this.selected) == false) {
			Debug.Log ("Selecionando movimento...");
			this.selected = mov;
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

	public void MoveUnits(Unit[] units, Battlefield bttlFld)
    {
        Debug.Log("Executando movemento!");
        int numberOfSteps = selected.NumberOfSteps;

        for (int step = 0; step < numberOfSteps; step++) {
            ExecuteStep(step, units, bttlFld);
        }

		//Destroys the used movement object
		Destroy (selected.gameObject);
		//Instantiates a new movement
		ReloadMovements();
		//Clears the variables
		this.selected = null;
	}

    private void ExecuteStep(int step, Unit[] units, Battlefield bttlFld)
    {
        //Move units one by one
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i] != null)
            {
                //TODO This is a Coroutine/Thread
                selected.DoStep(step, units[i], bttlFld);
            }

            //TODO Check if unit is alive
            //units[i].DestroyIfKilled(bttlFld);
        }
    }
}
