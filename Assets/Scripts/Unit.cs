using UnityEngine;

public class Unit : MonoBehaviour {

	[SerializeField] private ClassEnum unitClass;
	[SerializeField] private SideEnum side;
    [SerializeField] private int id;
    [SerializeField] private int hp;
    private int x;
    private int y;

    public int Id
    {
        get { return id; }
    }

    public int X {
		get { return x; }
        set { x = value; }
	}

	public int Y {
		get {return y; }
        set { y = value; }
	}

    public ClassEnum Class {
        get { return unitClass; }
        set { unitClass = value; }
    }

    public SideEnum Side
    {
        get { return side; }
        set { side = value; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
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

	public void MeeleeAttack(Unit target) {
        //if (this.Class.Equals(ClassEnum.WARRIOR) &&
        //    (target.Class.Equals(ClassEnum.WARRIOR) || 
        //    target.Class.Equals(ClassEnum.ARCHER)))
        if (target.Side.Equals(SideEnum.ENEMY))
        {
            while (this.HP > 0 && target.HP > 0)
            {
                //Attack
                target.HP -= 1;

                //Counter-attack
                if (target.HP > 0)
                {
                    this.HP -= 1;
                }
            }
        }
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
