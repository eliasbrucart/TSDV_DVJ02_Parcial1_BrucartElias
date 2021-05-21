using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject normalColumn;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] Transform parent;
    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(normalColumn, new Vector3(i*2, 0, j*2), Quaternion.identity, parent);
            }
        }
    }

    void Update()
    {
        
    }
}
