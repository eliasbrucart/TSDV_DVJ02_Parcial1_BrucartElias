using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToExplode;

    private float timerToExplode;
    private bool destroyed;

    static public event Action<Bomb> BombExploded;

    void Start()
    {
        destroyed = false;
        timerToExplode = 0.0f;
    }

    void Update()
    {
        timerToExplode += Time.deltaTime;
        if (timerToExplode >= timeToExplode)
            Explode();
    }

    private void Explode()
    {
        if (!destroyed)
        {
            timerToExplode = 0.0f;
            destroyed = true;
            BombExploded?.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}