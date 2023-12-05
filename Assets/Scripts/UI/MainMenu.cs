using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        SceneManager.LoadScene(1);
    }

    public void onQuitPress()
    {
        Application.Quit();
    }
}
