using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator Animator;//the animatior the player uses
    InputAction attackKey;

    private void Awake()
    {
        attackKey = InputSystem.actions.FindAction("Attack");

        attackKey.performed += ctx => Attack();
    }

    void Attack()
    {
        //FIND THE CURRENT PLAYER STATE AND TRY TO ATTACK
        //POSSIBLE ATTACK STATES
        //IDLE, WALKING


        //IF ATTACK WHILE ATTACKING THEN DO A FOLLOW UP ANIMATION
    }



}
