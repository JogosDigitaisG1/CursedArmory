using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

   public enum DoorType
    {
        left, right, up, down
    }

    public DoorType doorType;
}
