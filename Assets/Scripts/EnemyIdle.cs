using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyIdle : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;

    private float timer;
    private Transform player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", timeUp && !playerClose);
    }
    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return (timer > StayTime);
    }
    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, player.position);
        return (dist < VisionRange);
    }
}
