using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToExplode;
    [SerializeField] private float distanceRay;

    private float timerToExplode;
    private bool destroyed;

    static public event Action BombExploded;
    static public event Action PlayerReciveDamage;

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
            BombExploded?.Invoke();
            Explosion(transform.forward);
            Explosion(-transform.right);
            Explosion(transform.right);
            Explosion(-transform.forward);
            StartCoroutine(DestroyBomb());
        }
    }

    private void Explosion(Vector3 dir)
    {
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceRay))
        {
            if (hit.collider.gameObject.tag == "DestructibleColumn")
                Destroy(hit.collider.gameObject);
            if(hit.collider.gameObject.tag == "Enemy")
            {
                GameManager.instanceGameManager.AddPoints();
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.tag == "Player")
            {
                PlayerReciveDamage?.Invoke();
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}