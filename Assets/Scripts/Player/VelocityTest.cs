using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    //this script is to mess around with the velocity functions to get a better understanding of practical use
    public Vector3 Woag;
    public Rigidbody rb;
    public bool doTheThing;
    public ForceMode forceMode;
    public Vector3 outputVelocity;

    public Vector3 CustomGravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity += CustomGravity * Time.deltaTime;
        if (doTheThing)
        {
            doTheThing = false;
            Force();
        }
        outputVelocity = rb.linearVelocity;
    }

    void Force()
    {
        rb.AddForce(Woag,forceMode);
    }
}
