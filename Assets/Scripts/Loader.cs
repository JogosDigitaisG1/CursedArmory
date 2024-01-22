using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Loader : MonoBehaviour
{

    //private static Action onLoaderCallback;

    //public static void Load(string scene)
    //{

    //    onLoaderCallback = () =>
    //    {
    //        SceneManager.LoadScene(scene);
    //    };


    //    SceneManager.LoadScene(ScenesCons.LOADING);


    //}

    //public static void LoaderCallback()
    //{
    //    if (onLoaderCallback != null)
    //    {
    //        onLoaderCallback();
    //        onLoaderCallback = null;
    //    }
    //}

    public static Loader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }


    public static string loadingScreenSceneName = ScenesCons.LOADING;
    public static float minLoadingScreenTime = 1.5f;  // Minimum time to show the loading screen


    public void Load(string sceneName, string sceneToUnload)
    {
        StartCoroutine(LoadSceneAsync(sceneName, sceneToUnload));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string sceneToUnload)
    {

        // Load the loading screen scene additively
        yield return SceneManager.LoadSceneAsync(loadingScreenSceneName, LoadSceneMode.Additive);

        // Ensure a minimum loading screen time
        float elapsedTime = 0f;
        while (elapsedTime < minLoadingScreenTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // If there's a scene to unload, do it here
        if (!string.IsNullOrEmpty(sceneToUnload))
        {
            yield return SceneManager.UnloadSceneAsync(sceneToUnload);
        }

        // Load the target scene additively
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            float progress = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            yield return null;
        }

        // Activate the loaded scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Unload the loading screen scene
        yield return SceneManager.UnloadSceneAsync(loadingScreenSceneName);

    }

}
