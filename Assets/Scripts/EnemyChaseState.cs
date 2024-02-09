using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour

{
    public float speed = 2.5f;
    public float visionRange;

    private Transform player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);
        
        Vector2 dir = player.position - animator.transform.position;
        animator.transform.position += (Vector3)dir.normalized * speed * Time.deltaTime;
    }
    
    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, player.position);
        return dist < visionRange;
    }
}
