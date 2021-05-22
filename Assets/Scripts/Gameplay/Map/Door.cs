using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SpawnDesColumn spawnDesColumn;
    [SerializeField] private EnemySpawner enemySpawner;
    private int x;
    private int z;
    public bool isOpen;
    void Start()
    {
        isOpen = false;
        SpawnDoor();
    }

    private void Update()
    {
        if (enemySpawner.enemiesAlive <= 0)
            isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isOpen)
            ScenesManager.instanceScenesManager.ChangeScene("GameOver");
    }

    void SpawnDoor()
    {
        transform.position = spawnDesColumn.usedPos[Random.Range(0, spawnDesColumn.usedPos.Count)] + new Vector3(0, -0.4f, 0);
    }
}
