using UnityEngine;

// Libby/Jordan Script \\
public class EnemyStateMachine
{
    // tracks which state the enemy is currently in \\

    public EnemyState currentEnemyState { get; set; }


    public void Initialise(EnemyState startingState)
    {
        currentEnemyState = startingState;
        currentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newState;
        currentEnemyState.EnterState();
    }
}
