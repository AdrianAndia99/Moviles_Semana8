using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float maxHeight;

    void Update()
    {
        if (playerTransform.position.y > maxHeight)
        {
            maxHeight = playerTransform.position.y;
            UpdateScore((int)maxHeight);
        }
    }

    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
