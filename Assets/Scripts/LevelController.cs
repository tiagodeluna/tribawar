using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static float ITEM_WIDTH = 0.7f;
	//public static float ADJUSTMENT = 0.295f;
    //4 = ?
    //5 = ?
    //6 = adjust: 0.295, pos: -1.77
    //7 = ?
    //8 = 0.31

    private Battlefield bttlFld;

    public Battlefield BattleField
    {
        get { return bttlFld; }
    }

	public void LoadLevel(int levelNumber) {

		int[,] model;

		if (levelNumber == 1) {
			model = GetGrid6x6 ();
		} else {
			model = GetGrid8x8 ();
		}

		bttlFld = new Battlefield(model.GetLength(0), model.GetLength(1));

		for (int x = 0; x < bttlFld.XSize; x++) {			
			for (int y = 0; y < bttlFld.YSize; y++) {
				//Instantiate unit in (x, y) position
				bttlFld.AddUnit(InstantiateUnit (model[x, y], x, y));
			}
		}
	}

	//Instantiates a new unit at the position
	private Unit InstantiateUnit (int charId, int x, int y) {

		GameObject obj = null;

		switch (charId) {
		case 1:
			obj = Resources.Load<GameObject> ("Prefabs/Player/player_fighter");
			break;
		case 11:
			obj = Resources.Load<GameObject> ("Prefabs/Enemy/enemy_fighter");
			break;
		default:
			obj = Resources.Load<GameObject> ("Prefabs/Other/blank");
			break;
		}

        float posX = InitPosX() + (x * ITEM_WIDTH);
        float posY = InitPosY() + (y * ITEM_WIDTH);
		Unit newUnit = ((GameObject) Instantiate(obj, new Vector3(posX, posY), Quaternion.identity)).GetComponent<Unit>();
		newUnit.OnItemPositionChanged (x, y);

		return newUnit;
	}

    public void CreateBlankUnit(Unit unit)
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Other/blank");
        Unit blank = ((GameObject)Instantiate(obj, unit.transform.position, Quaternion.identity)).GetComponent<Unit>();
        blank.OnItemPositionChanged(unit.X, unit.Y);

        bttlFld.Grid[unit.X, unit.Y] = blank;
    }

	private int [,] GetGrid8x8() {
		return new int[,] {
			{1,2,1,0,0,0,11,11},
			{1,0,1,0,11,11,0,11},
			{1,1,1,1,0,0,11,12},
			{1,0,0,0,11,11,0,11},
			{1,1,0,1,0,11,12,11},
			{2,1,1,0,0,0,0,11},
			{1,0,1,0,0,11,11,11},
			{1,1,0,1,0,11,11,11}
		};
	}

	private int [,] GetGrid6x6() {
		return new int[,] {
			{1,0,0,0,0,11},
			{1,0,0,0,0,11},
			{1,0,0,0,0,11},
			{1,0,0,0,0,11},
			{1,0,0,0,0,11},
			{1,0,0,0,0,11},
		};
	}

    //TODO Temporary. I need a better method to align components on screen
    private float InitPosX()
    {
        float value = -1.77f;
        switch(this.bttlFld.XSize)
        {
            case 4:
            case 5:
            case 6:
                value = -1.77f;
                break;
            case 7:
            case 8:
                value = -2.45f;
                break;
        }
        return value;
    }

    private float InitPosY()
    {
        return -1.4f;
    }

}
