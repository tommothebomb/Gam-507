using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool isAttacking;
    public enum AnimationStates
    {
        Idle,
        Walking, // use this for walking, called chasing in case some enemies cant walk
        Attack1, // lots of options so more versitile between enemy types
        Attack2, // dont need to use all of them
        Attack3,
        TakingDamage,
        Death
    }


    public void ChangeAnimation(AnimationStates state)
    {
        switch (state)
        {
            case AnimationStates.Idle: animator.SetTrigger("ToIdle"); break;
            case AnimationStates.Walking: animator.SetTrigger("ToWalk"); break;
            case AnimationStates.Attack1: animator.SetTrigger("ToAttack1"); break;
            case AnimationStates.Attack2: animator.SetTrigger("ToAttack2"); break;
            case AnimationStates.Attack3: animator.SetTrigger("ToAttack3"); break;
            case AnimationStates.TakingDamage: animator.SetTrigger("ToTakeDamage"); break;
            case AnimationStates.Death: animator.SetTrigger("ToDeath"); break;
        }
    }
}
