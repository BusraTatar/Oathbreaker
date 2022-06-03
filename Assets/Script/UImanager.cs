using UnityEngine;
using UnityEngine.SceneManagement;


public class UImanager : MonoBehaviour
{
    public AudioSource audio;

    
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Audio()
    {
        //ses dosyasý eklenince düzenlenecek
        audio.volume = 0;
    }
}