using UnityEngine;

// Libby/Jordan Script \\
public class WebProjectileLogic : MonoBehaviour
{
    [Header("Set By State")]
    public EnemyBase enemyBase;
    [Header("Set In Inspector")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float webSpeed;
    [SerializeField] float lifetime;
    float timer;


    private void Start()
    {
        timer = 0;
        rb.linearVelocity = Vector3.zero;
    }

    private void Update()
    {
        rb.linearVelocity = transform.forward * webSpeed;

        timer += Time.deltaTime;
        if (timer >= lifetime) FadeWebThenSetInactive();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyBase.player.GetComponent<PlayerMovement>().enabled = false;
            enemyBase.playerStuckWeb.SetActive(true);
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
            FadeWebThenSetInactive();
        }
        else if (!collision.gameObject.CompareTag("Spider")) FadeWebThenSetInactive(); // to prevent spider spawning it to se it inactive immediately
    }

    void FadeWebThenSetInactive()
    {
        // fade web to transparent
        gameObject.SetActive(false);
    }
}
