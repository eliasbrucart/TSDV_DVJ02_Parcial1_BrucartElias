using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDesColumn : MonoBehaviour
{
    [SerializeField] GameObject destructibleColumn;
    public List<GameObject> normalColumns = new List<GameObject>();
    [SerializeField] int minX;
    [SerializeField] int maxX;
    [SerializeField] int minZ;
    [SerializeField] int maxZ;
    [SerializeField] float gridY;
    [SerializeField] float spacing = 1.0f;
    [SerializeField] Vector3 gridOrigin = Vector3.zero;
    [SerializeField] int columnsAmount;

    void Start()
    {
        SpawnDestructibleColumns();
    }

    void Update()
    {
        //SpawnDestructibleColumns();
    }

    void SpawnDestructibleColumns()
    {
        while (columnsAmount > 0)
        {
            int x = 0;
            int z = 0;
            if ((CreatePosInX(ref x, minX, maxX) == true && CreatePosInZ(ref z, minZ, maxZ) == true) || (CreatePosInX(ref x, minX, maxX) == false && CreatePosInZ(ref z, minZ, maxZ) == false))
            {
                Vector3 positionColumn = new Vector3(x-0.5f, gridY, z+0.5f);
                positionColumn = positionColumn + gridOrigin;
                GameObject go = Instantiate(destructibleColumn, positionColumn, Quaternion.identity);
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
}
