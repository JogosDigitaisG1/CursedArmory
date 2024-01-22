using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject sound;
    public AudioSource soundSource;
    public AudioClip beforeGameSound;
    public AudioClip dungeonsGameSound;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(sound);
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = beforeGameSound;
        soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == ScenesCons.BASEMENTMAIN)
        {
            soundSource.clip = dungeonsGameSound;
            soundSource.Play();
        }
    }
}
