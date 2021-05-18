using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] Vector3 point;
    [SerializeField] float distanceRayCast;
    [SerializeField] GameObject bombPrefab;

    void Start()
    {
        transform.position = initialPos;
        point = transform.position;
    }
    void Update()
    {
        MovePlayer();
        SpawnBomb();
    }

    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !CheckColumn(Vector3.forward))
        {
            point += Vector3.forward;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && !CheckColumn(Vector3.back))
        {
            point += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !CheckColumn(Vector3.left))
        {
            point += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !CheckColumn(Vector3.right))
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

    private bool CheckColumn(Vector3 dir)
    {
        transform.LookAt(transform.position + dir);
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceRayCast))
            if (hit.collider.gameObject.tag == "NormalColumn")
                return true;
        return false;
    }

    void SpawnBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
}
