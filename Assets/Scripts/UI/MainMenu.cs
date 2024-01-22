using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        Loader.Instance.Load(ScenesCons.VILLAGE, ScenesCons.GAME);

    }

    public void onCavePress()
    {

        Loader.Instance.Load(ScenesCons.BASEMENTMAIN, ScenesCons.VILLAGE);
       
    }

    public void onStorePress()
    {

        Loader.Instance.Load(ScenesCons.SHOP, ScenesCons.VILLAGE);

    }

    public void onVillagePress()
    {
        Loader.Instance.Load(ScenesCons.VILLAGE, ScenesCons.SHOP);
    }

    public void onQuitPress()
    {
        Application.Quit();
    }
}
