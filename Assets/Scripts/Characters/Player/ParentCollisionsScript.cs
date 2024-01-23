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

        if (bodyCollider.IsTouching(collision))
        {
            bool isPickup = collision.gameObject.tag == TagsCons.pickupTag && _characterStatsScript.BagNotFull();
            bool isGold = collision.gameObject.tag == TagsCons.goldTag;

            if (isPickup || isGold)
            {
                PickupScript pickupScript = collision.gameObject.GetComponentInParent<PickupScript>();
                if (pickupScript != null)
                {
                    _characterStatsScript.GetPickup(pickupScript.PickupSO);
                    Destroy(collision.transform.parent.gameObject);
                }
                else
                {
                    Debug.LogWarning("PickupScript not found on the collided object.");
                }
            }
        }
    }
}
