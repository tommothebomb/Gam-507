using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform CameraPivotPoint;

    [Header("Debug")]
    [SerializeField] Vector2 mouseMovement;
    [SerializeField] Vector2 debugMouse;

    [Header("Core")]
    InputAction mouseDelta;
    [SerializeField] GameObject playerCamera;
    [SerializeField] float yClampMin;
    [SerializeField] float yClampMax;

    [SerializeField] Vector2 Velocity;

    [Header("Acceleraation")]
    [SerializeField] float maxSpeed;
    [SerializeField] float Acceleration;
    [SerializeField] float Deceleration;

    //add camera distance raycast
    [Header("Distance")]
    [Serialize] float maxDist;

    // Update is called once per frame
    private void Awake()
    {
        mouseDelta = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //get the camera delta
        //rotate the x and y of the camera based of the 
        
        mouseMovement = mouseDelta.ReadValue<Vector2>();
        mouseMovement = new Vector2(mouseMovement.y, mouseMovement.x);
        //Deceleration
        /*
        if (Mathf.Abs(mouseMovement.y) <= 0.25)
        {
            //decelerate velocity
            Velocity.y *= 0;
        }
        if (Mathf.Abs(mouseMovement.x) <= 0.25)
        {
            Velocity.x *= 0;
        }*/

        Velocity = mouseMovement * Acceleration;
        //clamp the velocity
        Velocity = new Vector2(Mathf.Clamp(Velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(Velocity.y, -maxSpeed, maxSpeed));

        //Velocity = Time.deltaTime;

        Quaternion XLocalAxis = quaternion.Euler(Velocity.x * Time.deltaTime, 0, 0,0); //local X axis rotation
        quaternion YGlobalaxis = quaternion.Euler(0, Velocity.y * Time.deltaTime, 0,0);
        Quaternion OutputRotation = YGlobalaxis * CameraPivotPoint.rotation * XLocalAxis;

        CameraPivotPoint.rotation = OutputRotation;


        //clamped v
        //CameraPivotPoint.eulerAngles = new Vector3(Mathf.Clamp(OutputRotation.eulerAngles.x, yClampMin, yClampMax), OutputRotation.eulerAngles.y, OutputRotation.eulerAngles.z);
  
    }
}
