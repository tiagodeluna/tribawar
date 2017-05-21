using UnityEngine;

public class PrefabsID
{
    public const int BLANK = 0;
    public const int ALLY_WARRIOR_LV1 = 111;
    public const int ALLY_WARRIOR_LV2 = 112;
    public const int ALLY_WARRIOR_LV3 = 113;
    public const int ENEMY_WARRIOR_LV1 = 211;
    public const int ENEMY_WARRIOR_LV2 = 212;
    public const int ENEMY_WARRIOR_LV3 = 213;

    //HP = 0..3
    //Class = 00..90
    //Side = 000..200
    public static int GetId(SideEnum side, ClassEnum unitClass, int hp)
    {
        return ((int)side) + ((int)unitClass) + hp;
    }

    public static GameObject GetPrefab(SideEnum side, ClassEnum unitClass, int hp)
    {
        return GetPrefab(GetId(side, unitClass, hp));
    }

    public static GameObject GetPrefab(int id)
    {
        GameObject obj;

        //TODO Include all other unit prefabs
        switch (id)
        {
            case PrefabsID.ALLY_WARRIOR_LV1:
                obj = Resources.Load<GameObject>(PrefabsPath.UNIT_ALLY_WARRIOR_LV1);
                break;
            case PrefabsID.ALLY_WARRIOR_LV2:
                obj = Resources.Load<GameObject>(PrefabsPath.UNIT_ALLY_WARRIOR_LV2);
                break;
            case PrefabsID.ENEMY_WARRIOR_LV1:
                obj = Resources.Load<GameObject>(PrefabsPath.UNIT_ENEMY_WARRIOR_LV1);
                break;
            case PrefabsID.ENEMY_WARRIOR_LV2:
                obj = Resources.Load<GameObject>(PrefabsPath.UNIT_ENEMY_WARRIOR_LV2);
                break;
            case PrefabsID.BLANK:
            default:
                obj = Resources.Load<GameObject>(PrefabsPath.UNIT_BLANK);
                break;
        }

        return obj;
    }
}
