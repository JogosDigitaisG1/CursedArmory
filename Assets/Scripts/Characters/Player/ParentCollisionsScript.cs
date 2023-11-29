using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParentCollisionsScript : MonoBehaviour
{

    public AttackScript AttackScript;
    private CharacterStatsScript _characterStatsScript;
    public Collider2D bodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        _characterStatsScript = GetComponent<CharacterStatsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackScript.OnChildTriggerEnter2D(collision);

        if (collision.gameObject.tag == TagsCons.pickupTag && bodyCollider.IsTouching(collision))
        {
            Debug.Log("Got pickup");
            _characterStatsScript.GetPickup(collision.gameObject.GetComponentInParent<PickupScript>().PickupSO);
            

                Destroy(collision.gameObject);
        }
    }
}
