using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol: StateMachineBehaviour
{
    public float speed = 2.5f;
    public Transform[] waypoints;
    private int currentWaypointIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentWaypointIndex = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var targetPosition = waypoints[currentWaypointIndex].position;
        var direction = targetPosition - animator.transform.position;
        animator.transform.position += direction.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(animator.transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}