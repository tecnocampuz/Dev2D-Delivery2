using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private float visionRange = 5f;
    [SerializeField]
    private float visionAngle = 45f;

    [SerializeField]
    private float chaseSpeed = 2.5f;
    public float patrolSpeed = 2f;

    private Transform player;
    private float playerDistance;

    //Patrol
    public Transform[] waypoints;
    private int currentWaypointIndex;

    [SerializeField]
    private GameObject alarm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaypointIndex = 0;
    }
    
    bool IsPlayerInFOV() {
        if (playerDistance > visionRange)
            return false;

        Vector2 origin = transform.position;
        Vector2 directionP = player.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, directionP, visionRange);
        if (hit.collider != null)
        {
            if (hit.collider.transform != player || hit.collider.CompareTag("Wall"))
            {
                playerDistance = Vector3.Distance(transform.position, player.position);
                float playerAngle = Vector3.Angle(transform.right, player.position - transform.position) - 3;
            }
        }
        
        var direction = player.position - transform.position;
        if (direction == Vector3.zero)
            return true;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float ad = Mathf.Abs(angle - transform.rotation.eulerAngles.z);
        float d = Mathf.Min(ad, 360 - ad);
        if (d > visionAngle / 2)
            return false;

        return true;
    }

    void Update()
    {
        if (player == null)
            return;

        playerDistance = Vector3.Distance(transform.position, player.position);

        //Patrol
        if (IsPlayerInFOV())
        {
            alarm.GetComponent<EnemyAlarm>().PlayerDetected();
            PlayerChase();
        }
        else
        {
            alarm.GetComponent<EnemyAlarm>().PlayerLeft();
            Patrol();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        var direction = Quaternion.AngleAxis(visionAngle/2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * visionRange);
        var direction2 = Quaternion.AngleAxis(-visionAngle/2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * visionRange);

        
        if (player != null)
        {
            Gizmos.color = playerDistance > visionRange ? Color.green : Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.white;
    }

    private void Patrol()
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

    private void PlayerChase()
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
