using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel = GameObject.Find("PausePanel");

        // brief delay to allow ScoreManager script to load child objects - TODO fix/remove
        // Invoke("DeactivatePausePanel", 0.01f);

        DeactivatePausePanel();
        // pausePanel.SetActive(false);
    }


    void DeactivatePausePanel()
    {
        pausePanel.SetActive(false);
    }







    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }
}
