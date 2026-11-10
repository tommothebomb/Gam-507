using System;
using UnityEngine;
using UnityEngine.AI;

// Libby/Jordan Script \\
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : MonoBehaviour, IDamageable, ITriggerCheckable
{
    [Tooltip("make sure is assigned as the players model")]
    public GameObject player;
    public PoolManager pooler;

    // from IDamageable interface \\
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float damageDeals;

    // from ITriggerCheckable interface \\
    public bool IsAggroable { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackDist { get; set; }

    // animation triggers \\
    public enum AnimationTriggers
    {
        Idle,
        Moving, // use this for walking, called moving in case some enemies cant walk
        Attack1, // lots of options so more versitile between enemy types
        EndAttack1, 
        Attack2, // dont need to use all of them
        EndAttack2, 
        Attack3,
        TakingDamage,
        Death
    }
    public EnemyAnimations enemyAnimController;

    #region // state machine variables \\
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyPatrolState patrolState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyDeathState deathState { get; set; }
    #endregion

    #region // scriptableObject variables \\
    [SerializeField] EnemyPatrolSOBase enemyPatrolBase;
    [SerializeField] EnemyChaseSOBase enemyChaseBase;
    [SerializeField] EnemyAttackSOBase enemyAttackBase;
    [SerializeField] EnemyDeathSOBase enemyDeathBase;

    public EnemyPatrolSOBase enemyPatrolBaseInstance { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    public EnemyDeathSOBase enemyDeathBaseInstance { get; set; }
    #endregion

    [Header("Navmesh Settings")]
    public NavMeshAgent agent;
    [Tooltip("any values left at 0 will be assigned the navmesh agents default values")]
    public float speed;
    [Tooltip("any values left at 0 will be assigned the navmesh agents default values")]
    public float turningSpeed;
    [Tooltip("any values left at 0 will be assigned the navmesh agents default values")]
    public float acceleration;
    [Tooltip("stopping distance default is already zero so will set this to whatever you put here")]
    public float stoppingDist;

    [Header("Per Enemy Variables")]
    [Tooltip("if this doesnt apply to this enemy type, assign gameobject this script is on")]
    public Transform shootPoint;
    [Tooltip("if this doesnt apply to this enemy type, assign gameobject this script is on")]
    public GameObject playerStuckWeb;
    [Tooltip("if this doesnt apply to this enemy type, assign gameobject this script is on")]
    public Transform ratNest;


    private void Awake()
    {
        enemyPatrolBaseInstance = Instantiate(enemyPatrolBase);
        enemyChaseBaseInstance = Instantiate(enemyChaseBase);
        enemyAttackBaseInstance = Instantiate(enemyAttackBase);
        enemyDeathBaseInstance = Instantiate(enemyDeathBase);

        stateMachine = new EnemyStateMachine();

        patrolState = new EnemyPatrolState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        enemyPatrolBaseInstance.Initialize(gameObject, this);
        enemyChaseBaseInstance.Initialize(gameObject, this);
        enemyAttackBaseInstance.Initialize(gameObject, this);
        enemyDeathBaseInstance.Initialize(gameObject, this);

        stateMachine.Initialise(patrolState);

        // set navmesh agent values if required
        if(speed != 0) agent.speed = speed;
        if(turningSpeed != 0) agent.angularSpeed = turningSpeed;
        if(acceleration != 0) agent.acceleration = acceleration;
        agent.stoppingDistance = stoppingDist; // default stopping distance is 0 so just always set it to the value in inspector
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();

        // walk and idle animations \\
        if (agent.velocity.magnitude >= 0.1f && enemyAnimController.isMoving == false && enemyAnimController.isAttacking == false)
        {
            enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Moving);
            enemyAnimController.isMoving = true;
        }
        else if (agent.velocity.magnitude < 0.1f && enemyAnimController.isMoving == true && enemyAnimController.isAttacking == false)
        {
            enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Idle);
            enemyAnimController.isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    // from IDamageable interface \\
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        // play damage animation somehow
        // depenidng on how materials work, could use enemy material and flash red?
        // confirmed will work for spider
        if (CurrentHealth <= 0) stateMachine.ChangeState(deathState);
    }

    // from ITriggerCheckable interface \\
    public void SetAggroZoneStatus(bool isAggroable) 
    {
        IsAggroable = isAggroable;
    }
    public void SetAggroStatus(bool isAggroed) 
    {
        IsAggroed = isAggroed;
    }
    public void SetAttackDistBool(bool isWithinAttackDist) 
    {
        IsWithinAttackDist = isWithinAttackDist;
    }

    // animation handler \\
    // will allow for animations to be handled from the animator inside of unity
    public void AnimationTriggerEvent(AnimationTriggers trigger)
    {
        stateMachine.currentEnemyState.AnimationTriggerEvent(trigger);
    }
}
