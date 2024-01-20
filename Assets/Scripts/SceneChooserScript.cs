using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChooserScript : MonoBehaviour
{

    public CharacterStatsScript _characterStatsScript;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void GoToVillage()
    {
        _characterStatsScript.StatsAfterChangeScene();
        Loader.Load(ScenesCons.VILLAGE);
    }


    public void Reset()
    {
        _characterStatsScript.StatsAfterDeathRestart();
        Loader.Load(ScenesCons.BASEMENTMAIN);
    }
}
