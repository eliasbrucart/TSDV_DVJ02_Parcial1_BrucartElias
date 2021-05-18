using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float cameraSpeed;
    [SerializeField] Vector3 offset;
    private void Start()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(player.transform.position + offset, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, (cameraSpeed * distance) * Time.deltaTime);
    }
}
