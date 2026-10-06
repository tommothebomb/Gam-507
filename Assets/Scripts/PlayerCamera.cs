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
        mouseMovement *= Acceleration * Time.deltaTime;
        //Deceleration
        if (mouseMovement == Vector2.zero)
        {
            //decelerate velocity
            Velocity *= Deceleration * Time.deltaTime;
        }

        Velocity += mouseMovement;
        //clamp the velocity
        Velocity = new Vector2(Mathf.Clamp(Velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(Velocity.y, -maxSpeed, maxSpeed));

        //Velocity = Time.deltaTime;

        Quaternion XLocalAxis = quaternion.Euler(Velocity.x, 0, 0,0); //local X axis rotation
        quaternion YGlobalaxis = quaternion.Euler(0,Velocity.y, 0,0);
        Quaternion OutputRotation = YGlobalaxis * CameraPivotPoint.rotation * XLocalAxis;

        CameraPivotPoint.rotation = OutputRotation;


        //clamped v
        //CameraPivotPoint.eulerAngles = new Vector3(Mathf.Clamp(OutputRotation.eulerAngles.x, yClampMin, yClampMax), OutputRotation.eulerAngles.y, OutputRotation.eulerAngles.z);
  
    }
}
