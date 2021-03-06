using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] Vector3 point;
    [SerializeField] float distanceRayCast;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] private bool canSpawnBomb;

    public int lives;
    public Bomb bomb;

    void Start()
    {
        transform.position = initialPos;
        point = transform.position;
        canSpawnBomb = true;
        Bomb.BombExploded += BombExploded;
        Bomb.PlayerReciveDamage += ReciveDamage;
    }
    void Update()
    {
        MovePlayer();
        SpawnBomb();
    }

    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !CheckFreeWay(Vector3.forward))
        {
            point += Vector3.forward;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && !CheckFreeWay(Vector3.back))
        {
            point += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !CheckFreeWay(Vector3.left))
        {
            point += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !CheckFreeWay(Vector3.right))
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

    private bool CheckFreeWay(Vector3 dir)
    {
        transform.LookAt(transform.position + dir);
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceRayCast))
            if (hit.collider.gameObject.tag == "NormalColumn" || hit.collider.gameObject.tag == "DestructibleColumn" || hit.collider.gameObject.tag == "Bomb" || hit.collider.gameObject.tag == "Enemy")
                return true;
        return false;
    }

    void SpawnBomb()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && canSpawnBomb)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            canSpawnBomb = false;
        }
    }

    void BombExploded()
    {
        canSpawnBomb = true;
    }

    public void ReciveDamage()
    {
        if(lives > 0)
            lives--;
    }

    private void OnDisable()
    {
        Bomb.BombExploded -= BombExploded;
        Bomb.PlayerReciveDamage -= ReciveDamage;
    }
}
