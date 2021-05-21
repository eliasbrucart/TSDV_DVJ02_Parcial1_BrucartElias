using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Directions
    {
        Forward,
        Right,
        Left,
        Back
    }
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 actualPos;
    [SerializeField] private float raycastSize;
    [SerializeField] private Directions selectedDirection;
    [SerializeField] private float timeToChangeDirection;

    private Vector3 direction;
    private float timerToChangeDirection;
    private bool changeDirection;
    private bool isBloqued;
    private Vector3 lastDirection;
    private Vector3 newDirection;
    void Start()
    {
        transform.position = actualPos;
        SetDirection(selectedDirection);
        timerToChangeDirection = 0.0f;
        changeDirection = false;
        isBloqued = false;
    }

    void Update()
    {
        MoveEnemy();
        CheckPath(direction);
        ChangeDirection();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "DestructibleColumn")
    //        ChangeSpawnPos();
    //}
    //
    //void ChangeSpawnPos()
    //{
    //
    //}

    void SetDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Forward:
                direction = transform.forward;
                lastDirection = direction;
                break;
            case Directions.Right:
                direction = transform.right;
                lastDirection = direction;
                break;
            case Directions.Left:
                direction = -transform.right;
                lastDirection = direction;
                break;
            case Directions.Back:
                direction = -transform.forward;
                lastDirection = direction;
                break;
        }
    }

    void MoveEnemy()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(transform.position + direction);
    }

    void CheckPath(Vector3 dir)
    {
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastSize, Color.red);
        if(Physics.Raycast(ray, out hit, raycastSize))
        {
            if (hit.collider.gameObject.tag == "NormalColumn" || hit.collider.gameObject.tag == "DestructibleColumn" || hit.collider.gameObject.tag == "Enemy")
                ReverseDirection(selectedDirection);
        }
    }

    void ReverseDirection(Directions selectedDirection)
    {
        switch (selectedDirection)
        {
            case Directions.Forward:
                direction = -transform.forward;
                lastDirection = direction;
                break;
            case Directions.Right:
                direction = -transform.right;
                lastDirection = direction;
                break;
            case Directions.Left:
                direction = transform.right;
                lastDirection = direction;
                break;
            case Directions.Back:
                direction = transform.forward;
                lastDirection = direction;
                break;
        }
    }

    void ChangeDirection()
    {
        if (timeToChangeDirection >= timerToChangeDirection)
            timerToChangeDirection += Time.deltaTime;

        Debug.Log(timerToChangeDirection);

        if((timerToChangeDirection >= timeToChangeDirection && !changeDirection) || (timerToChangeDirection >= timeToChangeDirection && isBloqued))
        {
            int min = 0;
            int max = 3;
            int value = Random.Range(min, max);
            Debug.Log("el random es: " + value);
            switch (value)
            {
                case 0:
                    direction = transform.forward;
                    newDirection = direction;
                    changeDirection = true;
                    isBloqued = false;
                    break;
                case 1:
                    direction = transform.right;
                    newDirection = direction;
                    changeDirection = true;
                    isBloqued = false;
                    break;
                case 2:
                    direction = -transform.right;
                    newDirection = direction;
                    changeDirection = true;
                    isBloqued = false;
                    break;
                case 3:
                    direction = -transform.forward;
                    newDirection = direction;
                    changeDirection = true;
                    isBloqued = false;
                    break;
                default:
                    break;
            }
        }
        if (changeDirection)
        {
            timerToChangeDirection = 0.0f;
            changeDirection = false;
        }
        if(lastDirection == newDirection)
            isBloqued = true;
    }
}
