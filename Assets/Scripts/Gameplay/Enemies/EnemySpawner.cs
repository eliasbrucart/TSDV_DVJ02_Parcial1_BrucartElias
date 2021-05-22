using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minX;
    [SerializeField] private int maxX;
    [SerializeField] private int minZ;
    [SerializeField] private int maxZ;
    public int enemiesAmount;

    [SerializeField] SpawnDesColumn spawnDesColumn;

    public int enemiesAlive = 0;
    public int enemyDamage;

    private int centerOnX = 12;
    private int centerOnZ = 8;
    private int x = 0;
    private float y = 0.0f;
    private int z = 0;
    void Start()
    {
        SpawnEnemy();
        Enemy.enemyDead += ReduceCantOfEnemies;
    }

    void SpawnEnemy()
    {
        while (enemiesAmount > 0)
        {
            if ((CreatePosInX(ref x, minX, maxX) == true && CreatePosInZ(ref z, minZ, maxZ) == true || CreatePosInX(ref x, minX, maxX) == false && CreatePosInZ(ref z, minZ, maxZ) == false))
            {
                if (!spawnDesColumn.UsedPos(x + centerOnX, z + centerOnZ))
                {
                    Vector3 positionEnemy = new Vector3(x + centerOnX, y, z + centerOnZ);
                    GameObject go = Instantiate(enemyPrefab, positionEnemy, Quaternion.identity);
                    enemiesAlive++;
                    enemiesAmount--;
                }
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

    void ReduceCantOfEnemies()
    {
        enemiesAlive--;
    }

    private void OnDisable()
    {
        Enemy.enemyDead -= ReduceCantOfEnemies;
    }
}