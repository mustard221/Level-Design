using TMPro;
using UnityEngine;

public class ScoreConnector : MonoBehaviour
{
    public TextMeshProUGUI newScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null) // Check the ScoreManager instance exists
        {
            ScoreManager.Instance.scoreText = newScoreText;
            ScoreManager.Instance.scoreText.text = "Points: " + ScoreManager.Instance.score;
            ScoreManager.Instance.scoreText.gameObject.SetActive(true);
        }
    }
}
