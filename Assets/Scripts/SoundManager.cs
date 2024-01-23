using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject sound;
    public AudioSource soundSourceMusic;
    public AudioSource soundSourceCoin;
    public AudioSource soundSourcePickcup;
    public AudioSource soundSourcePowerup;
    public AudioClip beforeGameSound;
    public AudioClip dungeonsGameSound;
    public AudioClip coinSound;
    public AudioClip pickupSound;
    public AudioClip powerUpSound;


    public static SoundManager Instance { get; private set; }

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


    // Start is called before the first frame update
    void Start()
    {
        soundSourceMusic.clip = beforeGameSound;
        soundSourceMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

    }

    public void PlayVillageTheme()
    {
        if (soundSourceMusic.clip != beforeGameSound)
        {
            soundSourceMusic.clip = beforeGameSound;
            soundSourceMusic.Play();
        }

    }


    public void PlayDungeonTheme()
    {

        if (soundSourceMusic.clip != dungeonsGameSound)
        {
            soundSourceMusic.clip = dungeonsGameSound;
            soundSourceMusic.Play();
        }

    }

    public void PlayCoinSound()
    {

        soundSourceCoin.clip = coinSound;
        soundSourceCoin.Play();

    }

    public void PlayPickupSound()
    {

        soundSourcePickcup.clip = pickupSound;
        soundSourcePickcup.Play();

    }

    public void PlayPowerupSound()
    {

        soundSourcePowerup.clip = powerUpSound;
        soundSourcePowerup.Play();

    }
}
