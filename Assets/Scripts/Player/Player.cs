using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] Vector3 point;

    private float distance;
    private float minDistance = 0.1f;

    void Start()
    {
        transform.position = initialPos;
        point = transform.position;
    }
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        distance = Vector3.Distance(transform.position, point);
        if (Input.GetKeyDown(KeyCode.UpArrow) && distance < minDistance)
        {
            point += Vector3.forward;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && distance < minDistance)
        {
            point += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && distance < minDistance)
        {
            point += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && distance < minDistance)
        {
            point += Vector3.right;
        }

        CheckExactMovement();
    }

    private void CheckExactMovement()
    {
        if (transform.position != point)
        {
            transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * speed);    
        }
    }
}
