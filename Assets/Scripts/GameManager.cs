using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO:
 * 	- Rename 'SwipeController' to 'TouchController'
 *  - 
*/
public class GameManager : MonoBehaviour {

	private const float MOVEMENT_DURATION = 0.1f;
	private const float DELAY_BETWEEN_MATCHES = 0.1f;

	[SerializeField] private MovementController movementController;
	[SerializeField] private LevelController levelController;

	private Unit[] selectedUnits;
	private PlayerInfo player;

	private bool canPlay;
	private bool canMove;

	// Use this for initialization
	void Start () {
		//Initialize control variables
		this.canPlay = true;
		this.canMove = false;
		this.selectedUnits = new Unit[3];
		this.player = new PlayerInfo();

		//Create battlefield
		this.levelController.LoadLevel (this.player.LevelNumber);

		//Load movements
		this.movementController.LoadMovements(this.player.LevelNumber);
        Debug.Log("A");

		//Assign events
		Unit.OnMouseOverItemEventHandler += OnMouseOverItem;
		Movement.OnMouseOverItemEventHandler += OnMouseOverItem;
	}

	void OnDisable () {
		//Unassign events
		Unit.OnMouseOverItemEventHandler -= OnMouseOverItem;
		Movement.OnMouseOverItemEventHandler -= OnMouseOverItem;
	}

	void Update () {
		if (canPlay && this.canMove) {
			//Execute movement with selected units
			this.movementController.MoveUnits (this.selectedUnits);
			//Clear selected units
			this.selectedUnits = new Unit[3];
			//Disable movement
			this.canMove = false;
		}

		/*
		//Gets the swipe direction
		DirectionEnum direction = SwipeController.Instance.SwipeTo ();

		if (canPlay && currentSelectedItem != null && direction != DirectionEnum.NONE) {
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
		}
		*/
	}

	//Unit selection
	void OnMouseOverItem(Unit item) {
		//Enemy unit selected...
		if (item.side == SideEnum.ENEMY) {
			Debug.LogError ("Hey, você não pode comandar unidades inimigas!");
		} else if (item.side == SideEnum.ALLY) {
			this.selectedUnits[0] = item;
			Debug.Log ("Unidade selecionada!");
			/*
			foreach (Unit u in selectedUnits) {
				//TODO Mesmo item selecionado. Desmarcar!
				if (u.X == item.X && u.Y == item.Y) {

				}
				//TODO Outro item selecionado.
					//TODO Não pode mais selecionar. Avisar!
					//TODO Novo item. Selecionar!
			}
			*/


			//TODO Old code. Delete...
			//if (currentSelectedItem == null) {
			//	currentSelectedItem = item;
			//}
		}
	}

	//Movement selection
	void OnMouseOverItem(Movement item) {
		this.canMove = this.movementController.SelectMovement (item, this.selectedUnits);
	}

	/*
	private Unit GetItemAt (DirectionEnum direction) {
		int x, y;
		switch (direction) {
		case DirectionEnum.BOTTOM:
			x = currentSelectedItem.X;
			y = currentSelectedItem.Y - 1;
			break;
		case DirectionEnum.LEFT:
			x = currentSelectedItem.X - 1;
			y = currentSelectedItem.Y;
			break;
		case DirectionEnum.RIGHT:
			x = currentSelectedItem.X + 1;
			y = currentSelectedItem.Y;
			break;
		case DirectionEnum.TOP:
			x = currentSelectedItem.X;
			y = currentSelectedItem.Y + 1;
			break;
		default:
			x = y = -1;
			break;
		}

		if (x >= 0 && x < GetLenghtX() && y >= 0 && y < GetLenghtY()) {
			return gridItems [x, y];
		}

		return null;
	}
	*/
	/*
	private IEnumerator Swap (Unit a, Unit b) {
		//Disables all rigidbodys
		ChangeRigidbodySatus (false);

		//Moves i1 to i2 position and vice versa
		Vector3 i1Position = a.transform.position;
		StartCoroutine (a.transform.Move (b.transform.position, MOVEMENT_DURATION));
		StartCoroutine (b.transform.Move (i1Position, MOVEMENT_DURATION));

		//Waits till movement ends to update the indexes in the grid
		yield return new WaitForSeconds (MOVEMENT_DURATION);
		UpdateIndexes (a, b);

		//Enables all rigidbodys
		ChangeRigidbodySatus (true);
	}
	*/
	/*
	private IEnumerator TryMatch (Unit a, Unit b) {
		//Waits coroutine 'Swap' be completed to get possible matches

		yield return StartCoroutine (Swap (a, b));

		MatchInfo matchA = MatchController.GetMatchInformation (gridItems, a);
		MatchInfo matchB = MatchController.GetMatchInformation (gridItems, b);

		//Checks if there was no valid match to 'a' or 'b'
		if (matchA.ValidMatch) {
			yield return StartCoroutine (DestroyItems (matchA.match));
			yield return StartCoroutine (UpdateGridAfterMatch (matchA));
		}
		else if (matchB.ValidMatch) {
			yield return StartCoroutine (DestroyItems (matchB.match));
			yield return StartCoroutine (UpdateGridAfterMatch (matchB));
		}
		else {
			//Undo the swap between 'a' and 'b'
			yield return StartCoroutine (Swap (a, b));
			yield break;
		}

	}
	*/
	/*
	IEnumerator UpdateGridAfterMatch (MatchInfo match) {
		canPlay = false;

		if (match.IsHorizontal) {
			for (int x = match.matchStartingX; x <= match.matchEndingX; x++) {
				for (int y = match.matchStartingY; y < GetLenghtY() - 1; y++) {
					//Gets the current element and the element above it to update the index
					Unit upperIndex = gridItems [x, y + 1];
					Unit current = gridItems [x, y];
					//Updates references into the grid
					gridItems [x, y] = upperIndex;
					gridItems [x, y + 1] = current;
					//Updates element's new position
					gridItems [x, y].OnItemPositionChanged (gridItems [x, y].X, gridItems [x, y].Y - 1);
				}
				//Instantiates new crystal into the last index
				////gridItems [x, ySize - 1] = InstantiateCrystal (x, ySize - 1);
			}
		} else if (match.IsVertical) {
			int matchHeight = (match.matchEndingY - match.matchStartingY) + 1;
			for (int y = match.matchStartingY + matchHeight; y <= GetLenghtY() - 1; y++) {
				//Gets the current element and the element above it to update the index
				Unit lowerIndex = gridItems [match.matchStartingX, y - matchHeight];
				Unit current = gridItems [match.matchStartingX, y];
				//Updates references into the grid
				gridItems [match.matchStartingX, y - matchHeight] = current;
				gridItems [match.matchStartingX, y] = lowerIndex;
			}

			//Updates elements' new position
			for (int y = 0; y < GetLenghtY() - matchHeight; y++) {
				gridItems [match.matchStartingX, y].OnItemPositionChanged (match.matchStartingX, y);
			}

			//Instantiates new crystals
			for (int i = 0; i < match.match.Count; i++) {
				int newY = (GetLenghtY() - i - 1);
				////gridItems [match.matchStartingX, newY] = InstantiateCrystal (match.matchStartingX, newY);
			}
		}

		//Checks if there are any new matches

		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				MatchInfo newMatch = MatchController.GetMatchInformation(gridItems, gridItems[x,y]);
				if (newMatch.ValidMatch) {
					yield return StartCoroutine (DestroyItems (newMatch.match));
					yield return new WaitForSeconds (DELAY_BETWEEN_MATCHES);
					yield return StartCoroutine (UpdateGridAfterMatch (newMatch));
				}
			}
		}

		yield return new WaitForSeconds (DELAY_BETWEEN_MATCHES);

		canPlay = true;
	}
	*/
	private IEnumerator DestroyItems (List<Unit> items) {
		foreach (Unit item in items) {
			yield return StartCoroutine (item.transform.Scale (Vector3.zero, 0.1f));
			Destroy (item.gameObject);
		}
	}
	/*
	//Enables/disables rigidbody's kinematic
	void ChangeRigidbodySatus (bool status) {
		foreach (Unit item in gridItems) {
			item.GetComponent<Rigidbody2D> ().isKinematic = !status;
		}
	}

	//Updates indexes of the swaped items
	private void UpdateIndexes (Unit i1, Unit i2) {
		Unit temp_i1 = gridItems [i1.X, i1.Y];
		gridItems [i1.X, i1.Y] = i2;
		gridItems [i2.X, i2.Y] = temp_i1;
		int i2_X = i2.X; int i2_Y = i2.Y;
		i2.OnItemPositionChanged (i1.X, i1.Y);
		i1.OnItemPositionChanged (i2_X, i2_Y);
	}

	private int GetLenghtX() {
		return gridItems.GetLength (0);
	}

	private int GetLenghtY() {
		return gridItems.GetLength (1);
	}
	*/
}
