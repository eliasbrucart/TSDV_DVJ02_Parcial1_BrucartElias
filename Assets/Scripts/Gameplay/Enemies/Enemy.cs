using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Back,
        Left,
        Right,
    }

    [SerializeField] private Direction dir;
    [SerializeField] private float speed;
    float distance = 0.55f;
    Vector3[] directions;
    int actualPosX;
    int actualPosZ;
    int maxDirections = 4;

    static public event Action enemyDead;
    void Start()
    {
        actualPosX = (int)transform.position.x;
        actualPosZ = (int)transform.position.z;
        dir = Direction.Left;
        directions = new Vector3[maxDirections];
        directions[(int)Direction.Up] = Vector3.forward;
        directions[(int)Direction.Back] = Vector3.back;
        directions[(int)Direction.Left] = Vector3.left;
        directions[(int)Direction.Right] = Vector3.right;
    }
    void Update()
    {
        MoveEnemy();
        DetectBlockedRoute();

        if (Moved())
            NewRoute();

    }
    void MoveEnemy()
    {
        transform.position += directions[(int)dir] * speed * Time.deltaTime;
        transform.LookAt(transform.position + directions[(int)dir]);
    }
    void DetectBlockedRoute()
    {
        Ray ray = new Ray(transform.position, directions[(int)dir]);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Bomb")
            {
                GoBack();
                return;
            }
            else if (hit.transform.gameObject.tag == "NormalColumn" || hit.transform.gameObject.tag == "DestructibleColumn")
            {
                Debug.Log("Entrooo");
                NewRoute();
            }
            else if (hit.transform.gameObject.tag == "Player")
            {
                FindObjectOfType<Player>().ReciveDamage();
                GoBack();
            }
        }
    }
    bool Moved()
    {
        if (actualPosX != (int)transform.position.x || actualPosZ != (int)transform.position.z)
        {
            actualPosX = (int)transform.position.x;
            actualPosZ = (int)transform.position.z;
            return true;
        }
        return false;
    }
    void NewRoute()
    {
        int maxPossibilitiesToNewRoute = 10;
        int selectNewPos = UnityEngine.Random.Range(0, maxPossibilitiesToNewRoute);
        if (selectNewPos != 0 && !CheckForObstacle((int)dir))
            return;

        int actualDir = (int)dir;
        int newDir;
        do
        {
            newDir = UnityEngine.Random.Range(0, maxDirections);
        } while (actualDir == newDir || CheckForObstacle(newDir));
        dir = (Direction)newDir;
        return;
    }

    bool CheckForObstacle(int newDir)
    {
        Ray ray = new Ray(transform.position, directions[newDir]);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Bomb" || hit.transform.gameObject.tag == "DestructibleColumn" || hit.transform.gameObject.tag == "NormalColumn" || hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }

    void GoBack()
    {
        switch (dir)
        {
            case Direction.Up:
                dir = Direction.Back;
                break;
            case Direction.Back:
                dir = Direction.Up;
                break;
            case Direction.Right:
                dir = Direction.Left;
                break;
            case Direction.Left:
                dir = Direction.Right;
                break;
        }
    }

    private void OnDestroy()
    {
        enemyDead?.Invoke();
    }
}