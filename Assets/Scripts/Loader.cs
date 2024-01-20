using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    private static Action onLoaderCallback;
 
    public static void Load(string scene)
    {

        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene);
        };


        SceneManager.LoadScene(ScenesCons.LOADING);
        
        
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}
