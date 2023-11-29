using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField]
    private PickupSO pickupSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PickupSO PickupSO { get {  return pickupSO; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == TagsCons.playerTag)
        {
            //Debug.Log("Got pickup");
        }
    }
}
