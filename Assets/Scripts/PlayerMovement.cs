using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] float Velocity;
    [SerializeField] Transform DirectionArrow;
    [SerializeField] Transform VelocityArrow;
    [SerializeField] Vector3 moveKeys;

    [Header("Core")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float turningArc;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform cameraDir;
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
        Move();
    }

    void Move()
    {
        //W S = forward backwards  / Z
        //A D = forward Left Right / X
        //Movedir = new Vector3(Movedir.y, 0, Movedir.x);
        Vector3 Movedir = movementKeys.ReadValue<Vector2>();
        moveKeys = Movedir;
        Movedir = new Vector3(Movedir.x, 0, Movedir.y);
        //the direction the camera is looking, this is your forward direction
        Vector3 direction = new Vector3(cameraDir.forward.x, 0, cameraDir.forward.z);



        rb.linearVelocity += direction * acceleration;

        //speed clamp
        if  (rb.linearVelocity.magnitude > maxSpeed)
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, rb.linearVelocity.y, SpeedClamp.z);
        }


        //VALUE DEBUG
        VelocityArrow.rotation = Quaternion.LookRotation(rb.linearVelocity); //direction of travel
        DirectionArrow.rotation = Quaternion.LookRotation(direction); //direction of intent 
        Velocity = rb.linearVelocity.magnitude; //total speed

    }

    #region Utility
    bool GroundedCheck()
    {



        return true;
    }

    #endregion

    //2 directions
    //one is the current diection of travel
    //two is the ideal direction of travel
    //you then lerp the current to the ideal and this gives us the turning arc
    //variables to add to this
    //  arc speed / turning speed
            //air von




    //script outline for myself 

    //movement should add force in a direction the camera is looking only a small amount so their is a turning arc
    //pivot point.rotation = direction
    //jumping adds vertical force
    //good air controll
    //face the direction of travel + turming so if turning lean further to the direction of turning

    //Animation states
    //idle
    //moving forwards
    //turning direction
    //jumping
    //acending
    //decending
    //vertical
    //horizontal
    //landing
    //pulling

    //pulling objects
    //grab the pulling object at a point
    //use that point as the ancore
    //if the object is hinged mvoe it accoring to the hinge
    //if the objects free just slow the players movement by the objects weight (speed = weight of object combined with the pulling force of the player)

}
