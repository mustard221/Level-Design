using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null) // Making sure only one scoreManager exists across scenes
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy the instance if one already exists
        }

        scoreText.gameObject.SetActive(false); // Set active false on game start
    }

    private void Start()
    {
        StartCoroutine(ShowTextWithDelay(4f));
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Points: " + score;
        scoreText.gameObject.SetActive(true);
    }

    private IEnumerator ShowTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait after time
        scoreText.gameObject.SetActive(true); // Set active
        scoreText.text = "Points: " + score;
    }
}
