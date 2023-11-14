using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    void Awake () { 
    
        if(this == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }

    }
}
