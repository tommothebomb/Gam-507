using UnityEngine;

// Libby/Jordan Script \\
public class WebProjectileLogic : MonoBehaviour
{
    public EnemyBase enemyBase;
    float lifetime;


    private void Start()
    {
        lifetime = 0;
    }

    private void Update()
    {
        // linearVel += forward

        lifetime += Time.deltaTime;
        if (lifetime >= 4) gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // make player 'stuck'
            // spawn visual for web trap
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
            gameObject.SetActive(false);
        }
    }
}
