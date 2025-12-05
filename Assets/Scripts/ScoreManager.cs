using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public TMP_Text currentScoreText;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    // use Awake instead of Start to get these objects before the PauseMenu script disables the panel
    public void Awake()
    {
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        finalScoreText = GameObject.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        
        // test manual set
        // currentScoreText.text = "420";

        // Initialize scores for pause menu
        ChangeScore(0);
        HighScoreUpdate();
    }


    public void ChangeScore(int points)
    {
        currentScore += points;
        currentScoreText.text = currentScore.ToString();
    }



    public void HighScoreUpdate()
    {

        // is there already a highscore?
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            // is this score higher?
            if (currentScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
                PlayerPrefs.SetInt("SavedHighScore", currentScore);
            } 
        } else
        {
            // if there is no highscore saved
            PlayerPrefs.SetInt("SavedHighScore", currentScore);
        }

        // update TMP
        finalScoreText.text = currentScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }

}