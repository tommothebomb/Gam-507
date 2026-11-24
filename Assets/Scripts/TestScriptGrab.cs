using UnityEngine;

public class TestScriptGrab : MonoBehaviour
{
    Rigidbody rb;
    public GameObject GrabbedObject;
    public LayerMask mask;
    public float Speed;
    //get the normal of the grabbed object


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, GrabbedObject.transform.position - this.transform.position, out hit, 3, mask);
        Debug.DrawRay(this.transform.position, GrabbedObject.transform.position - this.transform.position, Color.red, 0.4f);
        if(hit.point != null)
        {
            Vector3 newNorm = new Vector3(hit.normal.x, 0, hit.normal.z);
            Debug.DrawRay(hit.point, -newNorm, Color.green, 0.4f);
            GrabbedObject.GetComponent<Rigidbody>().linearVelocity = -newNorm * Speed;
        }
    }
}
