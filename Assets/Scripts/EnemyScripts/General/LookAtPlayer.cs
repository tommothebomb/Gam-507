using UnityEngine;

// Libby/Jordan Script \\
public class LookAtPlayer : MonoBehaviour
{
    [Tooltip("must assign player model to this or will target wrong transform")]
    [SerializeField] Transform playerModel;
    Vector3 playerPos;


    private void Update()
    {
        playerPos = new Vector3(playerModel.position.x, gameObject.transform.position.y, playerModel.position.z);
        transform.LookAt(playerPos);
    }
}
