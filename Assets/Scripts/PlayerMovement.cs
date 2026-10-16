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
    [SerializeField] Vector3 outputDir;
    [SerializeField] Vector3 currentDir;
    [SerializeField] float currentangle;
    [SerializeField] float angle;

    [Header("Core")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float turningArc;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform cameraDir;
    InputAction movementKeys;

    [Header("asstetics")]
    [SerializeField] Transform Model;
    [SerializeField] Animator animator; //not implented yet

    public enum PlayerStates
    {
        Idle,
        Moveing,
        Jumping,
        Falling,
    }



    [Header("Jump")]
    InputAction jumpKey;
    [SerializeField] bool isGrounded;
    float airSraifeSpeed;

    [SerializeField] LayerMask GroundMask;

    Vector2 jumpVelocity; //Horizontal and vertical velocity of a jump
    [SerializeField] float additionalAirJump;
    float remaingAirJumps;
    //double jump?//triple jump/combo jump

    private void Start()
    {
        movementKeys = InputSystem.actions.FindAction("Move");
        jumpKey = InputSystem.actions.FindAction("Jump");

        jumpKey.performed += ctx => Jump();
    }

    private void FixedUpdate()
    {
        /////////
        Move();
    }
    private void Update()
    {
        //grounded Check
        isGrounded = GroundedCheck();
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
        //Vector3 direction = new Vector3(cameraDir.forward.x, 0, cameraDir.forward.z);
        Vector2 direction = new Vector2(cameraDir.forward.x, cameraDir.forward.z).normalized;
        outputDir = direction; //direction vector2 the player is looking

        angle = (Vector2.SignedAngle(direction,Vector2.up)); //anlge direction player is looking from basic forwards

        Vector3 variable = Quaternion.AngleAxis(angle, Vector3.up) * Movedir;
        currentDir = Vector3.Lerp(currentDir, variable, 0.3f);
        //get the diffance of currentdir and variable to then rotate current dir to the variable


        rb.linearVelocity += currentDir * acceleration;

        //speed clamp
        if  (rb.linearVelocity.magnitude > maxSpeed && isGrounded)
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, rb.linearVelocity.y, SpeedClamp.z);
        }


        //VALUE DEBUG
        //VelocityArrow.rotation = Quaternion.LookRotation(currentDir); //direction of travel
        Model.rotation = Quaternion.LookRotation(currentDir); //direction of intent 
        Velocity = rb.linearVelocity.magnitude; //total speed

    }


    void Jump()
    {
        if (!isGrounded && remaingAirJumps == 0 ) { return; }
        Debug.Log("Jump");
        remaingAirJumps -= 1;
        rb.AddForce(new Vector3(currentDir.x , 1 , currentDir.z) * 400);
    }
    #region Utility
    bool GroundedCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector2.down,out hit, 0.55f, GroundMask);

        if (hit.collider != null)
        {
            remaingAirJumps = additionalAirJump;
            //is grounded
            //check ground normal for slide = on slide// add a downward force to the player
            if (Vector3.Dot(Vector3.up, hit.normal) > 0.3f)
            {
                Debug.Log("IM SLIDING YO");
            }

            return true;
        }
        else 
        {
            return false;
        }

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
