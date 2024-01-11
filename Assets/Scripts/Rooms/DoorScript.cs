using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private RoomScript roomScript;

    private void Start()
    {
        roomScript = GetComponentInParent<RoomScript>();
    }
    public enum DoorType
    {
        left, right, up, down
    }

    public DoorType doorType;


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == TagsCons.playerTag)
        {
            roomScript.DoorCollisionDetected(this, collision);
        }
        

    }

}
