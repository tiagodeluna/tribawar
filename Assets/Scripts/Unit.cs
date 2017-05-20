using UnityEngine;

public class Unit : MonoBehaviour {

	private int x;
	private int y;
	public int id;
	public WarriorEnum type;
	public SideEnum side;

	public int X {
		get { return x; }
	}

	public int Y {
		get {return y; }
	}

	public void OnItemPositionChanged (int x, int y) {
		this.x = x;
		this.y = y;
	}

	//Captures a click/touch over this item
	void OnMouseDown () {
		if (OnMouseOverItemEventHandler != null) {
			OnMouseOverItemEventHandler (this);
		}
	}

	public void MeeleeAttack() {

	}

	public void RangeAttack() {

	}

	public void CastSpell() {

	}

	public void Revive() {

	}

	public delegate void OnMouseOverItem (Unit item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;

}
