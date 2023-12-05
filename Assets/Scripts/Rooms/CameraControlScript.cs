using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagsCons.playerTag)
        {
            print("enter room " + gameObject.name);
            enabled = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == TagsCons.playerTag)
        {
            print("exit room " + gameObject.name);
            enabled = false;
        }

    }
}
