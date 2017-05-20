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

    public bool DoStep(int step, Unit unit, Battlefield bttlFld)
    {
        //Move unit
        Unit target = null;

        if (steps[step] == DirectionEnum.TOP)
        {
            target = bttlFld.GetUnitAt(unit.X, unit.Y + 1);
        }
        else if (steps[step] == DirectionEnum.RIGHT)
        {
            target = bttlFld.GetUnitAt(unit.X + 1, unit.Y);
        }
        else if (steps[step] == DirectionEnum.BOTTOM)
        {
            target = bttlFld.GetUnitAt(unit.X, unit.Y - 1);
        }
        else if (steps[step] == DirectionEnum.LEFT)
        {
            target = bttlFld.GetUnitAt(unit.X - 1, unit.Y);
        }

        //TODO Play animation
        return DoAction(actions[step], unit, target);
    }
    
    public int NumberOfSteps {
		get { return numberOfSteps; }
	}
    
    bool DoAction(CommandEnum action, Unit unit, Unit target)
    {
        //TODO
        return false;
    }

    //Captures a click/touch over this item
    void OnMouseDown () {
		if (OnMouseOverItemEventHandler != null) {
			OnMouseOverItemEventHandler (this);
		}
	}

	void ExecuteStep(Unit unit, DirectionEnum direction, CommandEnum command) {
		//TODO move to direction
		//TODO command
		switch(command) {
		case CommandEnum.MEELEE_ATTACK:
			unit.MeeleeAttack ();
			break;
		case CommandEnum.RANGE_ATTACK:
			unit.RangeAttack();
			break;
		case CommandEnum.CAST_SPELL:
			unit.CastSpell ();
			break;
		case CommandEnum.REVIVE:
			unit.Revive();
			break;
		}
			
		/*
		//Gets second item to swap
		Unit item = GetItemAt (direction);
		if (item != null) {
			//Swaps the two selected items and tries to match them
			StartCoroutine (TryMatch (currentSelectedItem, item));
		}
		else {
			Debug.LogError ("You can't swap these items!");
		}
		currentSelectedItem = null;
		*/

	}

	public delegate void OnMouseOverItem (Movement item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
