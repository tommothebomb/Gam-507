using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    //the gameobject being affected
    public Transform CameraPivotPoint;
    public Transform cameraObj;

    [Header("CameraDistance")]
    [Tooltip("the current distance the camera is from the player")]
    [SerializeField] float cameraTrueDistance;
    [Tooltip("the distance we want the camera to be from the player")]
    [SerializeField] float cameraGoalDistance;

    [SerializeField] float cameraZoomSpeed; //how quickly the mouse wheel zooms in and out
    [SerializeField] float camMaxDistance; //how far the player can zoom out
    [SerializeField] float camMinDistance; //how close the player can zoom in
    [SerializeField] LayerMask camLayerMask; //what type of game object the camera will consider a wall and zoom infront of
    [SerializeField] float cameraAutoZoomSpeed; //how quickly it adjusts to the goal position

    [Header("Core")]
    [SerializeField] Vector2 mouseMovement; 
    InputAction mouseDelta; //Movemenet of the mouse
    InputAction mouseScroll; //Movement of the scroll wheel 
    [SerializeField] float yClampMin; //Clamping the Y value
    [SerializeField] float yClampMax; 

    [SerializeField] Vector2 Velocity; //the direction the camera is moving

    [Header("Sensativity")]
    [SerializeField] float sensitivity;
    [SerializeField] float Deceleration;

    // Update is called once per frame
    private void Awake()
    {
        //Grab the actions
        mouseDelta = InputSystem.actions.FindAction("Look");
        mouseScroll = InputSystem.actions.FindAction("Zoom");
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //Camera Zoom/DistanceFromPlayer
        cameraGoalDistance += mouseScroll.ReadValue<float>() * cameraZoomSpeed;
        cameraGoalDistance = Mathf.Clamp(cameraGoalDistance, camMaxDistance, camMinDistance);

        //define the ray values
        RaycastHit hit;
        Ray ray = new Ray(CameraPivotPoint.position, CameraPivotPoint.transform.forward * cameraGoalDistance);

        Physics.Raycast(ray,out hit,Mathf.Abs(cameraGoalDistance), camLayerMask,QueryTriggerInteraction.Ignore);
        if (hit.transform != null) //if the camera hits a wall smoothly lerp it to be iinfront of the wall 
        {
            //we use a walldistance instead of just setting the goal distance so that once the camera isnt hitting a wall anymore it returns to its original distance
            float wallDistance = -Vector3.Distance(hit.point, CameraPivotPoint.position);
            //lerp the current postion or true position to the goal distance
            cameraTrueDistance = Mathf.Lerp(cameraTrueDistance, wallDistance, cameraAutoZoomSpeed);
            cameraObj.transform.localPosition = new Vector3(0, 0, cameraTrueDistance);
        }
        else
        {
            cameraTrueDistance = Mathf.Lerp(cameraTrueDistance, cameraGoalDistance, cameraAutoZoomSpeed);
            cameraObj.transform.localPosition = new Vector3(0, 0, cameraTrueDistance);
        }

       


        //get the input Values
        mouseMovement = mouseDelta.ReadValue<Vector2>();
        //swap the X and Y
        mouseMovement = new Vector2(-mouseMovement.y, mouseMovement.x);

        /*
        if (Mathf.Abs(mouseMovement.y) <= 0.25)
        {
            //decelerate velocity
            Velocity.y *= 0;
        }
        if (Mathf.Abs(mouseMovement.x) <= 0.25)
        {
            Velocity.x *= 0;
        }*/ //Old code for camera Velocity

        //applie the sensativity
        Velocity = mouseMovement * sensitivity;
        //clamp the speed (might remove this (yeh this isnt needed anymore could come back if we want))
        //Velocity = new Vector2(Mathf.Clamp(Velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(Velocity.y, -maxSpeed, maxSpeed));

        //Apply the axis transforms
        Quaternion XLocalAxis = quaternion.Euler(Velocity.x * Time.deltaTime, 0, 0,0); //local X axis rotation
        quaternion YGlobalaxis = quaternion.Euler(0, Velocity.y * Time.deltaTime, 0,0); //Global Y axis

        // Y axis acts as the base axis then we add the current rotation to it, and then the x axis modifcation so that is applied localy
        Quaternion OutputRotation = YGlobalaxis * CameraPivotPoint.rotation * XLocalAxis;
        //clamp the X axis,  (why is the equasion so weird, beacuse unity wraps angles so -30 becomes 330, this equasions then reverts that so the clamp can work normaly)
        Vector3 clampedOutput = new Vector3(Mathf.Clamp(((OutputRotation.eulerAngles.x + 540 )%360) - 180, yClampMin, yClampMax), OutputRotation.eulerAngles.y, 0);
        //apply the transforms
        CameraPivotPoint.eulerAngles = clampedOutput;

  
    }
}
