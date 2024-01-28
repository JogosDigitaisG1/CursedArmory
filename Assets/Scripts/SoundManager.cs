using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject sound;
    public AudioSource soundSourceMusic;
    public AudioSource soundSourceCoin;
    public AudioSource soundSourcePickcup;
    public AudioSource soundSourcePowerup;
    public AudioSource soundSourceSlash;
    public AudioSource soundSourceDamage;
    public AudioSource soundSourceSpell;
    public AudioSource soundSourceStomp;
    public AudioSource soundSourceSpecial;
    public AudioClip beforeGameSound;
    public AudioClip dungeonsGameSound;
    public AudioClip coinSound;
    public AudioClip pickupSound;
    public AudioClip powerUpSound;
    public AudioClip slashSound;
    public AudioClip damageSound;
    public AudioClip spellSound;
    public AudioClip stompSound;
    public AudioClip specialSound;
    public AudioClip bossMusic;


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

    public void PlayBossTheme()
    {

        if (soundSourceMusic.clip != bossMusic)
        {
            soundSourceMusic.clip = bossMusic;
            soundSourceMusic.Play();
        }

    }

    public void PlayCoinSound()
    {
        if (!soundSourceCoin.isPlaying)
        {
            soundSourceCoin.clip = coinSound;
            soundSourceCoin.Play();
        }


    }

    public void PlayPickupSound()
    {
        if (!soundSourcePickcup.isPlaying)
        {
            soundSourcePickcup.clip = pickupSound;
            soundSourcePickcup.Play();
        }


    }

    public void PlayPowerupSound()
    {
        if (!soundSourcePowerup.isPlaying)
        {
            soundSourcePowerup.clip = powerUpSound;
            soundSourcePowerup.Play();
        }


    }

    public void PlaySlashSound()
    {
        if (!soundSourceSlash.isPlaying)
        {
            soundSourceSlash.clip = slashSound;
            soundSourceSlash.Play();
        }


    }

    public void PlayDamageSound()
    {
        if (!soundSourceDamage.isPlaying)
        {
            soundSourceDamage.clip = damageSound;
            soundSourceDamage.Play();
        }


    }

    public void PlaySpellSound()
    {
        if (!soundSourceSpell.isPlaying)
        {
            soundSourceSpell.clip = spellSound;
            soundSourceSpell.Play();
        }


    }

    public void PlayStompSound()
    {
        if (!soundSourceStomp.isPlaying)
        {
            soundSourceStomp.clip = stompSound;
            soundSourceStomp.Play();
        }


    }

    public void PlaySpecialSound()
    {
        if (!soundSourceSpecial.isPlaying)
        {
            soundSourceSpecial.clip = specialSound;
            soundSourceSpecial.Play();
        }


    }
}
