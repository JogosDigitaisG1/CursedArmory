using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
            if (sceneToUnload.Contains("Basement"))
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name.Contains("Basement") && scene.isLoaded )
                    {
                        yield return SceneManager.UnloadSceneAsync(scene);
                    }
                }
            } else {
                yield return SceneManager.UnloadSceneAsync(sceneToUnload);
            }
            
        }

        // Load the target scene additively
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneLoad.isDone && !sceneName.Contains("Basement"))
        {
            float progress = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            yield return null;
        }




        while (sceneName.Contains("Basement") && !GameManager.Instance.bossSpawned)
        {
            yield return null;
        }

            Debug.Log("escenea que para " + sceneName);



        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);

        if (sceneName.Contains("Basement"))
        {
            yield return new WaitUntil(() => GameManager.Instance.bossSpawned);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        


            

        // Unload the loading screen scene
        yield return SceneManager.UnloadSceneAsync(loadingScreenSceneName);

    }

    public void RestartScene(string sceneName)
    {
        StartCoroutine(RestartSceneCoroutine(sceneName));
    }

    private IEnumerator RestartSceneCoroutine(string sceneName)
    {
        // Load the loading screen scene additively
        yield return SceneManager.LoadSceneAsync(loadingScreenSceneName, LoadSceneMode.Additive);

        // Ensure a minimum loading screen time
        float elapsedTime = 1f;
        while (elapsedTime < minLoadingScreenTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }



        // Unload the current scene
        yield return SceneManager.UnloadSceneAsync(sceneName);

        // Load the target scene additively
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            float progress = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            // Optionally, update any loading progress UI here
            yield return null;
        }

        while (sceneName.Contains("Basement") && !GameManager.Instance.bossSpawned)
        {
            yield return null;
        }



        // Activate the loaded scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Unload the loading screen scene
        yield return SceneManager.UnloadSceneAsync(loadingScreenSceneName);
    }
}


