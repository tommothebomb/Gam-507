using UnityEngine;

public class ShootPointLookAt : MonoBehaviour
{
    [Tooltip("must assign player model to this or will target wrong transform")]
    [SerializeField] Transform playerModel;


    private void Update()
    {
        transform.LookAt(playerModel);
    }
}
