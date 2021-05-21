using System.Collections.Generic;
using UnityEngine;

public class SpawnDesColumn : MonoBehaviour
{
    [SerializeField] GameObject destructibleColumn;
    [SerializeField] int minX;
    [SerializeField] int maxX;
    [SerializeField] int minZ;
    [SerializeField] int maxZ;
    [SerializeField] float gridY;
    [SerializeField] Vector3 gridOrigin = Vector3.zero;
    [SerializeField] int columnsAmount;

    private float centerOnX = 12.0f;
    private float centerOnZ = 8.0f;

    public List<Vector3> usedPos = new List<Vector3>();

    void Awake()
    {
        SpawnDestructibleColumns();
    }

    void SpawnDestructibleColumns()
    {
        while (columnsAmount > 0)
        {
            int x = 0;
            int z = 0;
            if ((CreatePosInX(ref x, minX, maxX) == true && CreatePosInZ(ref z, minZ, maxZ) == true) || (CreatePosInX(ref x, minX, maxX) == false && CreatePosInZ(ref z, minZ, maxZ) == false))
            {
                Vector3 positionColumn = new Vector3(x+centerOnX, gridY, z+centerOnZ);
                GameObject go = Instantiate(destructibleColumn, positionColumn, Quaternion.identity);
                usedPos.Add(go.transform.position);
                columnsAmount--;
            }
        }
    }

    bool CreatePosInX(ref int pair, int min, int max)
    {
        pair = Random.Range(min, max);
        if (pair % 2 != 0)
            return false;
        else
            return true;
    }

    bool CreatePosInZ(ref int pair, int min, int max)
    {
        pair = Random.Range(min, max);
        if (pair % 2 == 0)
            return false;
        else
            return true;
    }

    public bool UsedPos(int x, int z)
    {
        Debug.Log("x " + x + " z " + z);
        for (int i = 0; i < usedPos.Count; i++)
        {
            if (x == usedPos[i].x && z == usedPos[i].z)
                return true;
        }
        return false;
    }
}
