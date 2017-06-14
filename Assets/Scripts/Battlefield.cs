using UnityEngine;

public class Battlefield : MonoBehaviour {

    private const float ITEM_WIDTH = 0.7f;
    //public static float ADJUSTMENT = 0.295f;
    //6 = adjust: 0.295, pos: -1.77
    //8 = 0.31
    private Unit[,] grid;
    private int xSize = 8;
    private int ySize = 8;

    public Unit[,] Grid
    {
        get { return grid; }
    }

    public int XSize
    {
        get { return xSize; }
        set { this.xSize = value; }
    }

    public int YSize
    {
        get { return ySize; }
        set { this.ySize = value; }
    }

    public void CreateGrid(int[,] model)
    {
        this.xSize = model.GetLength(0);
        this.ySize = model.GetLength(1);
        grid = new Unit[xSize, ySize];

        for (int x = 0; x < this.xSize; x++)
        {
            for (int y = 0; y < this.ySize; y++)
            {
                //Instantiate unit in (x, y) position
                Vector3 position = new Vector3(InitPosX() + (x * ITEM_WIDTH), InitPosY() + (y * ITEM_WIDTH));
                AddUnit(InstantiateUnit(model[x, y], position, x, y));
            }
        }
    }

    //Instantiates a new unit at the position
    private Unit InstantiateUnit(int id, Vector3 position, int x, int y)
    {
        GameObject obj = PrefabsID.GetPrefab(id);

        Unit unit = ((GameObject)Instantiate(obj, position, Quaternion.identity)).GetComponent<Unit>();
        unit.OnItemPositionChanged(x, y);

        return unit;
    }

    public void InstantiateBlankAt(Vector3 position, int x, int y)
    {
        InstantiateUnit(PrefabsID.BLANK, position, x, y);
    }

    public void UpdateUnit(Unit oldUnit)
    {
        //Get id and create new unit
        int id = PrefabsID.GetId(oldUnit.Side, oldUnit.Class, oldUnit.HP);
        Unit unit = InstantiateUnit(id, oldUnit.transform.position, oldUnit.X, oldUnit.Y);
        //Update object in the grid
        grid[unit.X, unit.Y] = unit;
        //Destroy old object
        Destroy(oldUnit.gameObject);
    }

    public Unit GetUnitAt(Unit unit, DirectionEnum direction)
    {
        int x = -1, y = -1;

        if (direction == DirectionEnum.TOP)
        {
            x = unit.X;
            y = unit.Y + 1;
        }
        else if (direction == DirectionEnum.RIGHT)
        {
            x = unit.X + 1;
            y = unit.Y;
        }
        else if (direction == DirectionEnum.BOTTOM)
        {
            x = unit.X;
            y = unit.Y - 1;
        }
        else if (direction == DirectionEnum.LEFT)
        {
            x = unit.X - 1;
            y = unit.Y;
        }

        //Position doesn't exist
        if (x < 0 || x >= xSize || y < 0 || y >= ySize)
        {
            return null;
        }

        //Target is empty. Return a blank unit.
        /*
        if (target == null)
        {
            target = new Unit();
            target.X = x;
            target.Y = y;
            target.Class = ClassEnum.NONE;
            target.Side = SideEnum.NEUTRAL;
        }*/

        return grid[x, y];
    }

    public void AddUnit(Unit unit)
    {
        grid[unit.X, unit.Y] = unit;
    }

    //TODO Temporary. I need a better method to align components on screen
    private float InitPosX()
    {
        float value = -1.77f;
        switch (this.xSize)
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
