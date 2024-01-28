using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDeleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Basement") && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
