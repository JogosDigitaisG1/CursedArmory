using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        SoundManager.Instance.PlayVillageTheme();
        Loader.Instance.Load(ScenesCons.VILLAGE, ScenesCons.GAME);

    }

    public void onCavePress()
    {
        SoundManager.Instance.PlayDungeonTheme();
        Loader.Instance.Load(ScenesCons.BASEMENTMAIN, ScenesCons.VILLAGE);
       
    }

    public void onStorePress()
    {
        SoundManager.Instance.PlayVillageTheme();
        Loader.Instance.Load(ScenesCons.SHOP, ScenesCons.VILLAGE);

    }

    public void onVillagePress()
    {
        SoundManager.Instance.PlayVillageTheme();
        Loader.Instance.Load(ScenesCons.VILLAGE, ScenesCons.SHOP);
    }

    public void onQuitPress()
    {
        Application.Quit();
    }
}
