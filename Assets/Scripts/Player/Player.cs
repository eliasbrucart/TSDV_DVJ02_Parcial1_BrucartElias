using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] Transform point;

    private float distance;
    private float minDistance = 0.1f;

    void Start()
    {
        transform.position = initialPos;
    }
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        distance = Vector3.Distance(transform.position, point.position);
        if (Input.GetKeyDown(KeyCode.UpArrow) && distance < minDistance)
        {
            point.position += Vector3.forward;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && distance < minDistance)
        {
            point.position += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && distance < minDistance)
        {
            point.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && distance < minDistance)
        {
            point.position += Vector3.right;
        }

        CheckExactMovement();
    }

    private void CheckExactMovement()
    {
        if(transform.position != point.position)
        {
            transform.position = Vector3.Lerp(transform.position, point.position, Time.deltaTime * speed);
        }
    }
}
