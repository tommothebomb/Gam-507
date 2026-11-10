using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;
    public enum AnimationStates { idle, walking, attack1, attack2, death };
    public AnimationStates lastState;
    public AnimationStates currentState;


    private void Start()
    {
        lastState = AnimationStates.idle;
    }

    private void Update()
    {
        if (currentState != lastState)
        {
            OnEnumValChanged();
            lastState = currentState;
        }
    }

    void OnEnumValChanged()
    {
        switch (currentState)
        {
            case AnimationStates.idle: animator.SetTrigger("ToIdle"); break;
            case AnimationStates.walking: animator.SetTrigger("ToWalk"); break;
            case AnimationStates.attack1: animator.SetTrigger("ToAttack1"); break;
            case AnimationStates.attack2: animator.SetTrigger("ToAttack2"); break;
            case AnimationStates.death: animator.SetTrigger("ToDeath"); break;
        }
    }
}
