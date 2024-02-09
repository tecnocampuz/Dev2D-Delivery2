using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private float visionRange;
    private Transform player;
    private float playerDistance;
    [SerializeField]
    private float visionAngle;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        visionRange = 5f;
        visionAngle = 45f;
        //VisionRange = GetComponent<Animator>()
        //.GetBehaviour<EnemyIdle>().VisionRange;
    }
    
    void Update()
    {
        if (player != null)
        {
            playerDistance = Vector3.Distance(transform.position, player.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        var direction = Quaternion.AngleAxis(visionAngle/2, transform.forward) 
            * transform.right;
        Gizmos.DrawRay(transform.position, direction * visionRange);
        var direction2 = Quaternion.AngleAxis(-visionAngle/2, transform.forward) 
            * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * visionRange);

        Gizmos.color = Color.yellow;
        if (player != null && playerDistance < visionRange)
        {
            Vector3.Distance(transform.position, player.position);
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.white;
    }
    
    private float getPlayerDistance(Transform transform)
    {
        float dist = 0.0f;
        if (player != null) Vector3.Distance(transform.position, player.position);
        return dist;
    }
}
