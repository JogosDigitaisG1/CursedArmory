using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerScript playerControllerScript;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    public Slider healthSlider;

    private void Awake()
    {
        playerControllerScript = FindAnyObjectByType<PlayerControllerScript>();
        characterStatsScript = playerControllerScript.GetComponent<CharacterStatsScript>();

    }

    void Start()
    {
        healthSlider.maxValue = characterStatsScript.GetMaxHp();
    }

    void Update()
    {
        healthSlider.maxValue = characterStatsScript.GetMaxHp();
        healthSlider.value = characterStatsScript.GetCurrentHp();
    }
}
