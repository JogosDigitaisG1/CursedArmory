using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript = GetComponent<CharacterStatsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
