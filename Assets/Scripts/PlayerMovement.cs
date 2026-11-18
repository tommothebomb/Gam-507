using System.Runtime.ExceptionServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle,
        Moveing,
        Jumping,
        Falling,
    }


    [Header("Debug")]
    [SerializeField] float Velocity;
    [SerializeField] Vector3 moveKeys;

    [Header("Movement")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    Vector3 outputDir;
    Vector3 currentDir;
    float angle;
    Rigidbody rb;
    [SerializeField] Transform cameraDir;
    InputAction movementKeys;

    [SerializeField] bool isSliding;


    [Header("Visuals")]
    [SerializeField] Transform Model;
    [SerializeField] Animator animatorController;
    //[SerializeField] Animator animator; //not implented yet

    [Header("Jump")]
    [SerializeField] bool isGrounded;
    InputAction jumpKey;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] float additionalAirJump;
    float remaingAirJumps;

    [Header("Grab")]
    [SerializeField] bool IsGrab;
    bool usingHinge;
    InputAction grabKey;
    [SerializeField] Vector3 grabPoint; //the postion the player grabbed
    [SerializeField] Transform hands;
    [SerializeField] LayerMask interactableLayerMask;
    [SerializeField] float grabPullSpeed;
    [SerializeField] Vector2 grabEffecicency; //X is push speed Y is pull speed ()
    [SerializeField] GameObject GrabbedObject;

    [SerializeField] RigidbodyConstraints originalConstraints;
    [SerializeField] RigidbodyConstraints defaultConstraints;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        movementKeys = InputSystem.actions.FindAction("Move");
        jumpKey = InputSystem.actions.FindAction("Jump");
        grabKey = InputSystem.actions.FindAction("Interact");

        grabKey.performed += ctx => StartGrab();
        jumpKey.performed += ctx => Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = GroundedCheck();
        ////////
        if (IsGrab)
        {
            GrabMove();
        }
        else
        {
            Move();
        }


        if (currentDir.normalized.magnitude != 0)
        {
            Model.rotation = Quaternion.LookRotation(currentDir);
        }
    }

    void Move()
    {
        //W S = forward backwards  / Z
        //A D = forward Left Right / X
        //Movedir = new Vector3(Movedir.y, 0, Movedir.x);
        Vector3 Movedir = movementKeys.ReadValue<Vector2>();
        moveKeys = Movedir;
        Movedir = new Vector3(Movedir.x, 0, Movedir.y).normalized;


        //the direction the camera is looking, this is your forward direction
        //Vector3 direction = new Vector3(cameraDir.forward.x, 0, cameraDir.forward.z);
        outputDir = new Vector2(cameraDir.forward.x, cameraDir.forward.z).normalized;
         //direction vector2 the player is looking

        angle = (Vector2.SignedAngle(outputDir,Vector2.up)); //anlge direction player is looking from basic forwards

        Vector3 variable = Quaternion.AngleAxis(angle, Vector3.up) * Movedir;

        if (isGrounded)
        {
            currentDir = Vector3.Lerp(currentDir, variable, 0.8f);
        }
        else
        {
            currentDir = Vector3.Lerp(currentDir, variable, 0.05f);
        }
            //get the diffance of currentdir and variable to then rotate current dir to the variable


        rb.linearVelocity += currentDir * acceleration;

        //speed clamp airborn
        if  (rb.linearVelocity.magnitude > maxSpeed && !isGrounded)
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, rb.linearVelocity.y, SpeedClamp.z);
        }
        else if (rb.linearVelocity.magnitude > maxSpeed) //speed clamp grounded
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, SpeedClamp.y, SpeedClamp.z);
        }


        //VALUE DEBUG
        //VelocityArrow.rotation = Quaternion.LookRotation(currentDir); //direction of travel
        Velocity = rb.linearVelocity.magnitude; //total speed

    }

    void GrabMove()
    {
        #region ObjectMovement
        //using the hands as the movement direction
        //speed is / by objects mass and multipled by the diffrent of player mass to object mass so the force is applied equaly
        Rigidbody GrabRb = GrabbedObject.GetComponent<Rigidbody>(); 

        Vector3 Movedir = movementKeys.ReadValue<Vector2>();
        moveKeys = Movedir;
        Movedir = new Vector3(0, 0, Movedir.y).normalized;

        outputDir = new Vector2(hands.forward.x, hands.forward.z).normalized;
        angle = (Vector2.SignedAngle(outputDir, Vector2.up)); //anlge direction player is looking from basic forwards

        Vector3 FinalVelocity = Quaternion.AngleAxis(angle, Vector3.up) * Movedir;

        currentDir = Vector3.Lerp(currentDir, FinalVelocity, 1f);
        //get the diffance of currentdir and variable to then rotate current dir to the variable

        //we need to add the weight of the grabbed object aswell into out calculations
        // GrabRb.linearVelocity = rb.linearVelocity;
        
        GrabRb.linearVelocity = (currentDir * grabPullSpeed);

        if (usingHinge)
        {
            this.transform.position = hands.position + (-hands.transform.forward / 1.75f);
        }

        /*else IM OLD - CODE
        {
            this.transform.position = hands.GetChild(0).transform.position;
            HingeJoint Hjoint = GrabbedObject.GetComponent<HingeJoint>();
            Hjoint.motor = new JointMotor {targetVelocity = maxSpeed * Movedir.z,force = currentDir.magnitude * maxSpeed};
        }*/

       Debug.DrawRay(hands.position, hands.forward, Color.blue);

        if (GrabRb.linearVelocity.magnitude > grabPullSpeed)
        {
            Vector3 SpeedClamp = GrabRb.linearVelocity.normalized * (grabPullSpeed);
            GrabRb.linearVelocity = new Vector3(SpeedClamp.x, SpeedClamp.y, SpeedClamp.z);
        }

        /*
        if (rb.linearVelocity.magnitude > maxSpeed && !isGrounded)
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, rb.linearVelocity.y, SpeedClamp.z);
        }
        else if (rb.linearVelocity.magnitude > maxSpeed) //speed clamp grounded
        {
            Vector3 SpeedClamp = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(SpeedClamp.x, SpeedClamp.y, SpeedClamp.z);
        }*/


        //VALUE DEBUG
        Velocity = GrabRb.linearVelocity.magnitude; //total speeds 
        #endregion

        #region Falling Detection
        //this region is for is the ojbect or player is unsupported they shall break connection and the object should return to nature (basicaly isgrounded but for cube)




        #endregion

    }

    void Jump()
    {
        if ((!isGrounded && remaingAirJumps == 0) || IsGrab ) { return; }
        Debug.Log("Jump");
        remaingAirJumps -= 1;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(new Vector3(currentDir.x , 0.5f , currentDir.z) * jumpForce);
    }

    #region Grab
    void StartGrab()
    {
        if (!isGrounded) { return; }
        if (IsGrab) { EndGrab();  return; }
        Debug.Log("grab");

        Ray grabRay = new Ray() { origin = transform.position, direction = Model.transform.forward };
        RaycastHit hit;
        Physics.Raycast(grabRay,out hit, 3f, interactableLayerMask);
        Debug.DrawRay(grabRay.origin, grabRay.direction * 3, Color.red,3f);


        if (hit.collider != null && hit.collider.gameObject.CompareTag("Interactable"))
        {
            IsGrab = true;

            hands.position = hit.point;
           //hands.parent = null;

            Debug.DrawRay(hit.point, hit.normal,Color.green,2f);
            hands.LookAt(-hit.normal + hit.point);
            Debug.Log(hit.normal);

            rb.linearVelocity = Vector3.zero;

            hands.transform.parent = hit.transform;
            GrabbedObject = hit.transform.gameObject;



            originalConstraints = GrabbedObject.GetComponent<Rigidbody>().constraints;


            currentDir = Vector3.zero;

           // fixed joint method - worked mostly - couldnt do hinges 

           
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            if (!GrabbedObject.GetComponent<ConfigurableJoint>()) {
                this.transform.position = hands.position + (-hands.transform.forward / 1.75f);
                var joint = GrabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = rb;
                //dGrabbedObject.GetComponent<Rigidbody>().constraints = defaultConstraints;
             }
            else
            {
                usingHinge = true;
            }

            /*transform.position = hands.GetChild(0).position;
            var joint = GrabbedObject.AddComponent<FixedJoint>();
            joint.connectedBody = rb;*/    //rb.isKinematic = true;
            /* parenting method - didnt decelerate properly and didnt include player collision
            rb.isKinematic = true;
            this.transform.parent = GrabbedObject.transform;
            */
        }




    }

    //when relasing the grab button or nolonger being able to grab said object
    void EndGrab()
    {
        if (usingHinge)
        {
            usingHinge = false;
        }
        else
        {
            Destroy(GrabbedObject.GetComponent<FixedJoint>());
        }
        GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;


        IsGrab = false;
        GrabbedObject = null;

        GrabbedObject.GetComponent<Rigidbody>().constraints = originalConstraints;

        hands.transform.parent = transform;
    }


    #endregion


    #region Utility
    bool GroundedCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector2.down,out hit, 1.15f, GroundMask);
        Debug.DrawRay(transform.position, Vector2.down * 1.15f, Color.green);


        if (hit.collider != null)
        {
            remaingAirJumps = additionalAirJump;
            //is grounded
            //check ground normal for slide = on slide// add a downward force to the player
            //Debug.Log(Vector3.Dot(Vector3.up, hit.normal));
            if (Vector3.Dot(Vector3.up, hit.normal) < 0.9f)
            {
                Debug.Log("IM SLIDING YO");
                isSliding = true;
            }
            else if (isSliding)
            {
                isSliding = false;
            }
                //jump end
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
