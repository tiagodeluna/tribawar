using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private const string GAME_OBJECT_NAME = "MovementController";
    private const string PREFABS_FOLDER = "Prefabs/Movements";
    private const int NUMBER_OF_AVAILABLE_MOVEMENTS = 3;
    private const float STEP_DURATION = 0.6f;

    [SerializeField] private LevelController levelController;

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
        this.movementList = Resources.LoadAll<GameObject>(PREFABS_FOLDER);
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

        //Disable all rigidbodys
        ChangeRigidbodySatus(false);

        for (int step = 0; step < selected.NumberOfSteps; step++) {
            StartCoroutine(ExecuteStep(step, units));
            yield return new WaitForSeconds(STEP_DURATION);
        }

        //Enable all rigidbodys
        ChangeRigidbodySatus(true);

        //Destroy the used movement object
        Destroy (selected.gameObject);
		//Instantiate a new movement set
		ReloadMovements();
		//Clears the variables
		this.selected = null;
	}

    void ReloadMovements()
    {
        Vector3 posicao = GameObject.Find(GAME_OBJECT_NAME).transform.position;
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
                Unit unitAtPosition = this.levelController.BattleField.Grid[units[i].X, units[i].Y];
                if (units[i].Equals(unitAtPosition))
                {
                    this.levelController.CreateBlankUnit(units[i]);
                }

                //TODO This is a Coroutine/Thread
                yield return StartCoroutine( selected.DoStep(step, units[i], this.levelController.BattleField, STEP_DURATION) );
            }

            //Debug.Log("Updating battlefield");
            //this.levelController.UpdateBattlefield();
            //TODO Check if unit is alive
            //units[i].DestroyIfKilled(bttlFld);
        }
    }

    //Enables/disables rigidbody's kinematic
    void ChangeRigidbodySatus(bool status)
    {
        foreach (Unit item in this.levelController.BattleField.Grid)
        {
            item.GetComponent<Rigidbody2D>().isKinematic = !status;
        }
    }

}
