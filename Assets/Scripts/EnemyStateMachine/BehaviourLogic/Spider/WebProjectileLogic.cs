using UnityEngine;

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
        lifetime += Time.deltaTime;
        if (lifetime >= 5) gameObject.SetActive(false);
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
