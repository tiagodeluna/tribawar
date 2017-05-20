public class Battlefield
{
    private Unit[,] grid;
    private int xSize = 8;
    private int ySize = 8;

    public Battlefield()
    {
        grid = new Unit[xSize, ySize];
    }

    public Battlefield(int xSize, int ySize)
    {
        this.xSize = xSize;
        this.ySize = ySize;
        grid = new Unit[xSize, ySize];
    }

    public Unit[,] Grid {
        get { return grid; }
        set { this.grid = value; }
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

        if (x < 0 || x >= xSize || y < 0 || y >= ySize)
        {
            return null;
        }

        return grid[x,y];
    }

    public void AddUnit(Unit unit)
    {
        grid[unit.X, unit.Y] = unit;
    }

}
