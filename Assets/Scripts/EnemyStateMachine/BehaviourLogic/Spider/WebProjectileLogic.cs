using UnityEngine;

public class WebProjectileLogic : MonoBehaviour
{
    public EnemyBase enemyBase;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // make player 'stuck'
            // spawn visual for web trap
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
        }
    }
}
