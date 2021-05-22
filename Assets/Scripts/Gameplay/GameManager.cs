using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instanceGameManager;

    static public GameManager Instance{ get { return instanceGameManager; } }

    [SerializeField] private Player player;
    [SerializeField] private EnemySpawner enemySpawner;
    public int points;

    private void Awake()
    {
        if(instanceGameManager != null && instanceGameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instanceGameManager = this;
        }
    }

    void Update()
    {
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (player.lives == 0)
            ScenesManager.instanceScenesManager.ChangeScene("GameOver");
    }

    public void AddPoints()
    {
        points += enemySpawner.enemyDamage;
    }
}
