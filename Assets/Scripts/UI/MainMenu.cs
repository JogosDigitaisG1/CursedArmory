using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        Loader.Load(ScenesCons.VILLAGE);
    }

    public void onCavePress()
    {

        Loader.Load(ScenesCons.BASEMENTMAIN);
       
    }

    public void onStorePress()
    {

        Loader.Load(ScenesCons.SHOP);

    }

    public void onQuitPress()
    {
        Application.Quit();
    }
}
