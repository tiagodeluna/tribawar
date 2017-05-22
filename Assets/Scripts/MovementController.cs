using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private const int NUMBER_OF_AVAILABLE_MOVEMENTS = 3;
    private const float STEP_DURATION = 0.6f;

    [SerializeField] private Battlefield battlefield;

    private GameObject[] movementList;
    private Movement[] availableMovements;
	private Movement selected;


	void Start () {
        this.availableMovements = new Movement[NUMBER_OF_AVAILABLE_MOVEMENTS];
		ReloadMovements();
    }

    /// <summary>
    /// Creates an array with all movements from 'Resources' folder
    /// </summary>
    /// <param name="levelNumber"></param>
    public void LoadMovements(int levelNumber)
    {
        this.movementList = Resources.LoadAll<GameObject>(PrefabsPath.MOVEMENTS);
    }

    /// <summary>
    /// Select and confirm the selection of a movement
    /// </summary>
    /// <param name="mov">The selected movement</param>
    /// <param name="units">The selected units</param>
    /// <returns>The confirmation of the selected movement</returns>
    public bool SelectMovement(Movement mov, Unit[] units)
    {
		//Select the movement if it is not selected yet or if it has changed
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

    /// <summary>
    /// Start the movement with selected units
    /// </summary>
    /// <param name="units">The selected units</param>
    /// <returns>Coroutine result</returns>
    public IEnumerator MoveUnits(Unit[] units)
    {
        Debug.Log("Executing movement!");

        for (int step = 0; step < selected.NumberOfSteps; step++) {
            StartCoroutine( ExecuteStep(step, units) );
            yield return new WaitForSeconds(STEP_DURATION);
        }

        //Destroy the used movement object
        Destroy (selected.gameObject);
		//Instantiate a new movement set
		ReloadMovements();
		//Clears the variables
		this.selected = null;
	}

    void ReloadMovements()
    {
        Vector3 posicao = this.transform.position;
        GameObject randomMov = this.movementList[Random.Range(0, this.movementList.Length)];
        availableMovements[0] = Instantiate(randomMov, posicao, Quaternion.identity).GetComponent<Movement>();
    }

    private IEnumerator ExecuteStep(int step, Unit[] units)
    {
        //Move units one by one
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i] != null)
            {
                //Update unit reference from the grid
                units[i] = this.battlefield.Grid[units[i].X, units[i].Y];

                //TODO This is a Coroutine/Thread
                yield return StartCoroutine( selected.DoStep(step, units[i], this.battlefield, STEP_DURATION) );
            }
        }
    }


}
