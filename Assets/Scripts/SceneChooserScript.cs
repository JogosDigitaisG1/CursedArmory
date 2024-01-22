using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChooserScript : MonoBehaviour
{

    public CharacterStatsScript _characterStatsScript;

    public GameObject gameOver;
    public GameObject win;

    private void Awake()
    {
        gameObject.SetActive(false);
        win.SetActive(false);
    }
    public void GoToVillage()
    {
        _characterStatsScript.StatsAfterChangeScene();
        Loader.Instance.Load(ScenesCons.VILLAGE, ScenesCons.BASEMENTMAIN);
    }


    public void Reset()
    {
        _characterStatsScript.StatsAfterDeathRestart();
        Loader.Instance.Load(ScenesCons.BASEMENTMAIN, ScenesCons.BASEMENTMAIN);
    }
}
