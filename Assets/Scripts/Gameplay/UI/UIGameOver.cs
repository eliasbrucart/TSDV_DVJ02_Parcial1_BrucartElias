using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Text pointsEarnedText;
    void Start()
    {
        
    }

    void Update()
    {
        pointsEarnedText.text = "Points Earned: " + GameManager.instanceGameManager.points;
    }
}
