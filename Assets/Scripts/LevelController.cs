using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField] private Battlefield battlefield;

    public Battlefield BattleField
    {
        get { return battlefield; }
    }

	public void LoadLevel(int levelNumber) {

		int[,] model;

		if (levelNumber == 1) {
			model = GetGrid6x6 ();
		} else {
			model = GetGrid8x8 ();
		}

		battlefield.CreateGrid(model);
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
			{111,211,0,0,0,211},
			{111,0,212,0,0,211},
			{111,212,0,0,0,211},
			{112,212,0,211,0,211},
			{111,211,0,0,0,211},
			{111,0,211,0,0,211},
		};
	}
}
