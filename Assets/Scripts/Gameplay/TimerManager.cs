using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timer;
    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
