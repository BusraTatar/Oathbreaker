using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    public GameObject PausePanel;
    

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void Resume()
    {
        //düzenlenecek
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


