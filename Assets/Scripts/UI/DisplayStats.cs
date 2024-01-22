using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerScript playerControllerScript;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI swordsText;

    [SerializeField]
    private TextMeshProUGUI shieldsText;

    [SerializeField]
    private TextMeshProUGUI staffsText;

    [SerializeField]
    private TextMeshProUGUI bowsText;

    [SerializeField]
    private TextMeshProUGUI bagFilledText;

    [SerializeField]
    private TextMeshProUGUI bagCapacityText;

    private void Awake()
    {
        playerControllerScript = FindAnyObjectByType<PlayerControllerScript>();
        characterStatsScript = playerControllerScript.GetComponent<CharacterStatsScript>();
    }

    void Start()
    {
    }

    void Update()
    {
        int gold = characterStatsScript.GetCurrentPlayerStats().Gold;
        int swords = characterStatsScript.GetCurrentPlayerStats().SwordSpirits;
        int shields = characterStatsScript.GetCurrentPlayerStats().ShieldSpirits;
        int staffs = characterStatsScript.GetCurrentPlayerStats().StaffSpirits;
        int bows = characterStatsScript.GetCurrentPlayerStats().BowSpirits;
        int bagFilled = characterStatsScript.GetCurrentPlayerStats().BagActualCapacity;
        int bagCapacity = characterStatsScript.GetCurrentPlayerStats().BagMaxCapacity;

        goldText.text = gold.ToString();
        swordsText.text = swords.ToString();
        shieldsText.text = shields.ToString();
        staffsText.text = staffs.ToString();
        bowsText.text = bows.ToString();
        bagFilledText.text = bagFilled.ToString();
        bagCapacityText.text = bagCapacity.ToString();
    }
}
