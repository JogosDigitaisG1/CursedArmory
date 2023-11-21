using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectScript : MonoBehaviour
{

    public int detectRadius = 5;
    public LayerMask playerLayer;
    private string playerTag;

    [SerializeField]
    private bool detectedPlayer;


    public Collider2D playerObj;
    public List<Collider2D> visibleTargets = new List<Collider2D>();
    public List<Transform> visibleTargetsTest = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        detectedPlayer = false;
        playerTag = TagsCons.playerTag;
    }

    // Update is called once per frame
    void Update()
    {


        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRadius);

        
        if (visibleTargets.Count > 0 && !hitColliders.Contains(visibleTargets[0]))
        {
            detectedPlayer = false;
            playerObj = null;
            visibleTargets.Clear();
        }

        foreach (var hitCollider in hitColliders)
        {
            
            
            if (hitCollider.gameObject.tag == playerTag && !visibleTargets.Contains(hitCollider))
            {
                detectedPlayer = true;
                visibleTargets.Add(hitCollider);
                playerObj = hitCollider;

            }
        }

        
    }


    public bool DetectedPlayer()
    {
        return detectedPlayer;
    }

    public Collider2D GetPlayerCollider()
    {
        return playerObj;
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

    }
}
