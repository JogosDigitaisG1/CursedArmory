using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadSoundScript : MonoBehaviour
{
    private CharacterStatsScript healthScript;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponentInParent<CharacterStatsScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.IsAlive())
        {
            audioSource.Play();
        }
    }
}
