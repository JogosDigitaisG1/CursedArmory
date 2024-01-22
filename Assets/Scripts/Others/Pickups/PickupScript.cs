using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    [SerializeField]
    private PickupSO pickupSO;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            //Debug.Log("Got pickup");
        }
    }
}
