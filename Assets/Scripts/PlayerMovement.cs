using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    InputAction movementKeys;


    [Header("Jump")]
    InputAction jumpKey;
    bool isGrounded;
    float airSraifeSpeed;
    Vector2 jumpVelocity; //Horizontal and vertical velocity of a jump


    //double jump?//triple jump/combo jump

    private void Start()
    {
        movementKeys = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        /////////
    }
}
