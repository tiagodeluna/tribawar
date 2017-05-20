﻿using UnityEngine;

public class LevelController : MonoBehaviour {

	public static float ITEM_WIDTH = 0.7f;
	public static float ADJUSTMENT = 0.295f;
    //4 = ?
    //5 = ?
    //6 = 0.295
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
				bttlFld.AddUnit(InstantiateCharacter (model[x, y], x, y, bttlFld.XSize));
			}
		}
	}

	//Instantiates a new unit at the position
	private Unit InstantiateCharacter (int charId, int x, int y, int xSize) {

		GameObject obj = null;

		switch (charId) {
		case 1:
			obj = Resources.Load<GameObject> ("Prefabs/Player/player_fighter");
			break;
		case 2:
			obj = Resources.Load<GameObject> ("Prefabs/Player/player_bowman");
			break;
		case 11:
			obj = Resources.Load<GameObject> ("Prefabs/Enemy/enemy_fighter");
			break;
		case 12:
			obj = Resources.Load<GameObject> ("Prefabs/Enemy/enemy_bowman");
			break;
		default:
			obj = Resources.Load<GameObject> ("Prefabs/Other/civilian");
			break;
		}

		Unit character = ((GameObject) Instantiate(obj, 
			new Vector3(x * ITEM_WIDTH - xSize*ADJUSTMENT, y), Quaternion.identity)).GetComponent<Unit>();
		character.OnItemPositionChanged (x, y);

		return character;
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

}
