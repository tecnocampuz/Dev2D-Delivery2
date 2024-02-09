using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private float visionRange;
    private Transform player;
    [SerializeField]
    private float chaseSpeed = 2.5f;
    private float playerDistance;
    [SerializeField]
    private float visionAngle;
    //Patrol
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    private int currentWaypointIndex;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        visionRange = 5f;
        visionAngle = 45f;
        //Patrol
        currentWaypointIndex = 0;
    }
    
    void Update()
    {
        if (player != null)
        {
            playerDistance = Vector3.Distance(transform.position, player.position);
            //Patrol
            if (playerDistance <= visionRange)
            {
                playerChase();
            }
            else
            {
                patrol();
            }
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

        
        if (player != null)
        {
            Gizmos.color = playerDistance > visionRange ? Color.green : Color.yellow;
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
    //Patrol
    void patrol()
    {
        if (waypoints.Length == 0) return;

        var targetPosition = waypoints[currentWaypointIndex].position;
        var direction = targetPosition - transform.position;
        transform.position += direction.normalized * patrolSpeed * Time.deltaTime;
        //Calcula la direccio del moviment
        if(direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void playerChase()
    {
        var targetPosition = player.position;
        var direction = targetPosition - transform.position;
        transform.position += direction.normalized * chaseSpeed * Time.deltaTime;

        if(direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
