using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instanceGameManager;

    static public GameManager Instance{ get { return instanceGameManager; } }

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
