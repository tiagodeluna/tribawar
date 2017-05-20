using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	[SerializeField] private DirectionEnum step1 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum command1;
	[SerializeField] private DirectionEnum step2 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum command2;
	[SerializeField] private DirectionEnum step3 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum command3;
	[SerializeField] private DirectionEnum step4 = DirectionEnum.NONE;
	[SerializeField] private CommandEnum command4;
	[SerializeField] private MovementSetEnum set;

	public int NumberOfSteps {
		get {
			int steps = 1; //There will always be the first step.
			steps += (step2 != DirectionEnum.NONE ? 1 : 0);
			steps += (step3 != DirectionEnum.NONE ? 1 : 0);
			steps += (step4 != DirectionEnum.NONE ? 1 : 0);
			return steps;
		}
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

	public void Move(int step, Unit unit) {

		switch (step) {
		case 1:
			ExecuteStep (unit, this.step1, this.command1);
			break;
		case 2:
			ExecuteStep (unit, this.step2, this.command2);
			break;
		case 3:
			ExecuteStep (unit, this.step3, this.command3);
			break;
		case 4:
			ExecuteStep (unit, this.step4, this.command4);
			break;
		}
	}

	public delegate void OnMouseOverItem (Movement item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
