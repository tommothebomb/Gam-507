using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool isMoving;
    public bool isAttacking;
    public enum AnimationStates
    {
        Idle,
        Moving, // use this for walking, called moving in case some enemies cant walk
        Attack1, // lots of options so more versitile between enemy types
        Attack2, // dont need to use all of them
        Attack3,
        Hurt,
        Death
    }


    private void Start()
    {
        isAttacking = false;
    }

    public void ChangeAnimation(AnimationStates state)
    {
        switch (state)
        {
            case AnimationStates.Idle: animator.SetTrigger("ToIdle"); break;
            case AnimationStates.Moving: animator.SetTrigger("ToWalk"); break;
            case AnimationStates.Attack1: isAttacking = true; animator.SetTrigger("ToAttack1"); break;
            case AnimationStates.Attack2: isAttacking = true; animator.SetTrigger("ToAttack2"); break;
            case AnimationStates.Attack3: isAttacking = true; animator.SetTrigger("ToAttack3"); break;
            case AnimationStates.Death: isAttacking = true; animator.SetTrigger("ToDeath"); break;
            case AnimationStates.Hurt: isAttacking = true; animator.SetTrigger("ToHurt"); break;
            default: animator.SetTrigger("ToIdle"); break;
        }
    }
}
