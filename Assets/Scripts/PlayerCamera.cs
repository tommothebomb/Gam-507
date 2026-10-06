using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform CameraPivotPoint;


    [Header("Acceleraation")]
    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float directionChangeSpeed;

    [Header("Centering")]
    [SerializeField] Transform lookDirection;
    [SerializeField] float delay;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        //get the camera delta
        //rotate the x and y of the camera based of the 
    }
}
