using System.Collections;
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
    bool _isGrounded;
    bool isGrounded
    {
        get { return _isGrounded; } set { _isGrounded = value; animatorController.SetBool("IsGrounded", value); }
    }
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


    [Header("Attack")]
    InputAction attackKey;
    //if attack while attacking or slightlyBefore

    [Header("Audio")]
    bool playedLand = true;
    float fallTimer = 0f;
    float fallTimer2 = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        movementKeys = InputSystem.actions.FindAction("Move");
        jumpKey = InputSystem.actions.FindAction("Jump");
        grabKey = InputSystem.actions.FindAction("Interact");
        attackKey = InputSystem.actions.FindAction("Attack");

        grabKey.performed += ctx => StartGrab();
        jumpKey.performed += ctx => Jump();
        attackKey.performed += ctx => Attack();

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
        //Debug.Log(rb.angularVelocity);

        if (!isGrounded){
            //StartCoroutine(CheckFall());
            playedLand = false;
            if (rb.linearVelocity.y < -0.1f)
            {
                fallTimer += Time.deltaTime;
            }
            else
            {
                AkUnitySoundEngine.PostEvent("Stop_Falling", this.gameObject);
                fallTimer = 0f;
                fallTimer2 = 0f;
            }

            if (fallTimer > 1)
            {
                fallTimer2 += Time.deltaTime;
                {
                    AkUnitySoundEngine.PostEvent("Play_Falling", this.gameObject);
                    float pitch = (Mathf.Min(fallTimer2, 2)) * 3f;
                    AkUnitySoundEngine.SetRTPCValue("Falling", pitch, gameObject);
                    Debug.Log(pitch);
                }
            }
        }
        else
        {
            AkUnitySoundEngine.SetRTPCValue("Falling", 0, this.gameObject);
            fallTimer = 0f;
            fallTimer2 = 0f;
            if (!playedLand)
            {
                AkUnitySoundEngine.PostEvent("Play_Land", this.gameObject);
                AkUnitySoundEngine.PostEvent("Stop_Falling", this.gameObject);
                playedLand = true;
            }
        }
    }

    void Move()
    {
        #region
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
        #endregion

        #region Visuals
        Velocity = rb.linearVelocity.magnitude; //total speed

        if (currentDir.normalized.magnitude != 0)
        {
            Model.rotation = Quaternion.LookRotation(currentDir);
        }
        //Animation
        animatorController.SetFloat("Velocity", rb.linearVelocity.magnitude);

        #endregion
    }

    void Jump()
    {
        if ((!isGrounded && remaingAirJumps == 0) || IsGrab ) { return; }
        AkUnitySoundEngine.PostEvent("Stop_Falling", this.gameObject);
        AkUnitySoundEngine.SetRTPCValue("Falling", 0, this.gameObject);
        remaingAirJumps -= 1;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(new Vector3(currentDir.x , 0.5f , currentDir.z) * jumpForce);
        AkUnitySoundEngine.PostEvent("Play_Jump", this.gameObject);

        //Animation
        animatorController.SetTrigger("Jump");
    }

    #region Grab
    void GrabMove()
    {
        if (!isGrounded) { EndGrab();return; }
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
            Model.rotation = Quaternion.LookRotation(hands.transform.forward);
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
        //physics raycast down if no ground on the object then drop the iteam and do some more check for how it will fall
        if(!usingHinge)
        {
            Collider grabbedCollider= GrabbedObject.GetComponent<Collider>();
            Vector3 bounds = new Vector3(grabbedCollider.bounds.size.x/3, 0.2f, grabbedCollider.bounds.size.z/3);
            RaycastHit hit;
            //Debug.DrawRay(GrabbedObject.transform.position, Vector3.down * ((GrabbedObject.GetComponent<Collider>().bounds.size.y / 2) + 0.05f), Color.green);
            //Physics.Raycast(GrabbedObject.transform.position, Vector3.down, out hit, (GrabbedObject.GetComponent<Collider>().bounds.size.y/2) + 0.05f, GroundMask);
            Physics.BoxCast(new Vector3(GrabbedObject.transform.position.x, GrabbedObject.transform.position.y-grabbedCollider.bounds.size.y/2, GrabbedObject.transform.position.z), bounds, Vector3.down,out hit);

            

            Debug.Log(hit.collider);

            /*
            if (hit.collider == null)
            {
                //DROP Grab
                Debug.Log("object not supported");
            }
            else
            { Debug.Log(hit.point); }*/
        }


        #endregion
        //Animation
        animatorController.SetFloat("Velocity", Movedir.z);
    }

    void StartGrab()
    {
        if (IsGrab) { EndGrab(); return; }
        if (!isGrounded) { return; }
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


            
            Model.rotation = Quaternion.LookRotation(hands.transform.forward);
            

            animatorController.SetTrigger("Grab");
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


        //GrabbedObject.GetComponent<Rigidbody>().constraints = originalConstraints;
        GrabbedObject.GetComponent<Rigidbody>().useGravity = true;

        IsGrab = false;
        GrabbedObject = null;

        hands.transform.parent = transform;

        animatorController.SetTrigger("EndGrab");
        animatorController.ResetTrigger("Attack");
    }

    IEnumerator CheckFall()
    {
        float fallHeight = transform.position.y;
        yield return new WaitForSeconds(0.2f);
        if (transform.position.y < fallHeight && !GroundedCheck()) StartCoroutine(fallingSound());
    }
    IEnumerator fallingSound()
    {
        AkUnitySoundEngine.PostEvent("Play_Falling", this.gameObject);
        if (GroundedCheck())
        yield return null;
    }
    #endregion

    #region Attack

    void Attack()
    {
        //just play the animation
        animatorController.SetTrigger("Attack");
    }


    /*
    //called via keyframes when the player attacks
    //do a spherecast at each connection points
    public void AttackPoint()
    {
        Debug.Log("AHHHH");
    }
    */

   
    #endregion

    #region Utility
    bool GroundedCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector2.down,out hit, 1.15f, GroundMask,QueryTriggerInteraction.Ignore);
        Debug.DrawRay(transform.position, Vector2.down * 1.15f, Color.green);


        if (hit.collider != null)
        {
            if (hit.collider.tag == "Carpet") AkUnitySoundEngine.SetSwitch("FloorMaterial", "Soft", Model.gameObject);
            else AkUnitySoundEngine.SetSwitch("FloorMaterial", "Hard", Model.gameObject);
            remaingAirJumps = additionalAirJump;
            //is grounded
            //check ground normal for slide = on slide// add a downward force to the player
            //Debug.Log(Vector3.Dot(Vector3.up, hit.normal));
            if (Vector3.Dot(Vector3.up, hit.normal) < 0.9f)
            {
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

}
