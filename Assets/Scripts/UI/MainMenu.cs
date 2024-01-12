using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        SceneManager.LoadScene(ScenesCons.VILLAGE);
    }

    public void onCavePress()
    {
        SceneManager.LoadScene(ScenesCons.BASEMENTMAIN);
    }

    public void onQuitPress()
    {
        Application.Quit();
    }
}
