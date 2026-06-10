using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Score UI")]
    public TextMeshProUGUI TxtScore;
    public TextMeshProUGUI TxtFinalScore;

    private int totalScore;

    public int distanceMultiplier = 1;

    private Transform player;

    public int TotalScore
    {
        get { return totalScore; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (GameStateManager.instance != null &&
            GameStateManager.instance.currentState == GameState.Playing)
        {
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        totalScore = Mathf.FloorToInt(player.position.z * distanceMultiplier);

        if (TxtScore != null)
        {
            TxtScore.text = totalScore.ToString();
        }
    }

    public void UpdateFinalScore()
    {
        if (TxtFinalScore != null)
        {
            TxtFinalScore.text = "Score: " + totalScore.ToString();
        }
    }

    public void ResetScore()
    {
        totalScore = 0;

        if (TxtScore != null)
        {
            TxtScore.text = "0";
        }
    }
}