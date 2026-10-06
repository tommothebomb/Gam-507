using System;
using UnityEngine;

// Libby/Jordan Script \\
public class EnemyBase : MonoBehaviour, IDamageable, ITriggerCheckable
{
    // from IDamageable interface \\
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    // from ITriggerCheckable interface \\
    public bool IsAggroable { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackDist { get; set; }

    // animation triggers \\
    public enum AnimationTriggers
    {
        Idle,
        Chasing,
        Attack1, // lots of options so more versitile between enemy types
        Attack2, // dont need to use all of them
        Attack3,
        TakingDamage,
        PlayFootsteps
    }

    #region // state machine variables \\
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyPatrolState patrolState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyDeathState deathState { get; set; }
    #endregion

    // stats \\
    [SerializeField] protected float damageDeals;
    [SerializeField] protected float speed;


    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        patrolState = new EnemyPatrolState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        stateMachine.Initialise(patrolState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    // from IDamageable interface \\
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0) Die();
    }
    public void Die()
    {
        // play death anim
        Destroy(gameObject);
    }

    // from ITriggerCheckable interface \\
    public void SetAggroZoneStatus(bool isAgroable) 
    {
        IsAggroable = isAgroable;
    }
    public void SetAggroStatus(bool isAgroed) 
    {
        IsAggroed = isAgroed;
    }
    public void SetAttackDistBool(bool isWithinAttackDist) 
    {
        IsWithinAttackDist = isWithinAttackDist;
    }

    // animation handler \\
    // will allow for animations to be handled from the animator inside of unity
    void AnimationTriggerEvent(AnimationTriggers trigger)
    {
        stateMachine.currentEnemyState.AnimationTriggerEvent(trigger);
    }
}
