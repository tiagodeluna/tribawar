using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    private const int LIMIT_OF_STEPS = 4;

	[SerializeField] private DirectionEnum step1 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum action1;
	[SerializeField] private DirectionEnum step2 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum action2;
	[SerializeField] private DirectionEnum step3 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum action3;
	[SerializeField] private DirectionEnum step4 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum action4;
	[SerializeField] private MovementSetEnum set;

    //Auxiliary variables to facilitate coding and decrease the amount of conditional structures and loops
    private DirectionEnum[] steps;
    private CommandEnum[] actions;
    private int numberOfSteps;

    void Start()
    {
        //Calculate the number of steps
        numberOfSteps = (step1 != DirectionEnum.NONE ? 1 : 0);
        numberOfSteps += (step2 != DirectionEnum.NONE ? 1 : 0);
        numberOfSteps += (step3 != DirectionEnum.NONE ? 1 : 0);
        numberOfSteps += (step4 != DirectionEnum.NONE ? 1 : 0);

        //Instantiate the arrays steps and actions
        steps = new DirectionEnum[LIMIT_OF_STEPS];
        steps[0] = step1;
        steps[1] = step2;
        steps[2] = step3;
        steps[3] = step4;
        actions = new CommandEnum[LIMIT_OF_STEPS];
        actions[0] = action1;
        actions[1] = action2;
        actions[2] = action3;
        actions[3] = action4;
    }

    public DirectionEnum GetStepDirection(int step)
    {
        return steps[step];
    }

    public IEnumerator DoStep(int step, Unit unit, Battlefield bttlfld, float duration)
    {
        //Get target position
        Unit target = bttlfld.GetUnitAt(unit, steps[step]);

        if (target != null)
        {
            //Create blank space at the initial position
            bttlfld.InstantiateBlankAt(unit.transform.position, unit.X, unit.Y);

            //Visually move unit
            StartCoroutine(unit.transform.Move(target.transform.position, duration));
            yield return new WaitForSeconds(duration);

            //TODO Play action animation and effects
            DoAction(actions[step], unit, target, bttlfld);
        }
    }
    
    public int NumberOfSteps {
		get { return numberOfSteps; }
	}
    
    bool DoAction(CommandEnum action, Unit unit, Unit target, Battlefield bttlfld)
    {
        switch (action)
        {
            case CommandEnum.MEELEE_ATTACK:
                unit.MeeleeAttack(target);
                //Win
                if (target.HP == 0)
                {
                    //Unit assumes enemy's position
                    unit.OnItemPositionChanged(target.X, target.Y);
                    //Destroy enemy
                    Destroy(target.gameObject);
                    //Update unit GameObject
                    bttlfld.UpdateUnit(unit);
                }
                else //Lose
                {
                    //Destroy unit
                    Destroy(unit.gameObject);
                    //Update target GameObject
                    bttlfld.UpdateUnit(target);
                }
                break;
            case CommandEnum.RANGE_ATTACK:
                break;
            case CommandEnum.CAST_SPELL:
                break;
            case CommandEnum.REVIVE:
                break;
            default:
                break;
        }

        return false;
    }

    //Captures a click/touch over this item
    void OnMouseDown () {
		if (OnMouseOverItemEventHandler != null) {
			OnMouseOverItemEventHandler (this);
		}
	}

	public delegate void OnMouseOverItem (Movement item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
