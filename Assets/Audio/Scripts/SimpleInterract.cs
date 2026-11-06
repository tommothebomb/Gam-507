using UnityEngine;

public class SimpleInterract : MonoBehaviour
{
    InterractionType interType;
    public AK.Wwise.Event newEvent;
    bool interractionToggle;
    public enum InterractionType { 
        CollisionEnter,
        TriggerEnter,
        CollisionExit,
        TriggerExit,
    }
    public void OnInterraction(InterractionType interraction)
    {
        switch (interraction)
        {
            case InterractionType.CollisionEnter:
                interType = InterractionType.CollisionEnter; break;
            case InterractionType.TriggerEnter:
                interType= InterractionType.TriggerEnter; break;
            case InterractionType.CollisionExit:
                interType = InterractionType.CollisionExit; break;
            case InterractionType.TriggerExit: 
                interType = InterractionType.TriggerExit; break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interType == InterractionType.CollisionEnter && collision.gameObject.CompareTag("Player"))
        {
            AkUnitySoundEngine.PostEvent(newEvent.Name, this.gameObject);
            interractionToggle = !interractionToggle;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (interType == InterractionType.CollisionExit && collision.gameObject.CompareTag("Player"))
        {
            AkUnitySoundEngine.PostEvent(newEvent.Name, this.gameObject);
            interractionToggle = !interractionToggle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (interType == InterractionType.TriggerEnter && other.gameObject.CompareTag("Player"))
        {
            AkUnitySoundEngine.PostEvent(newEvent.Name, this.gameObject);
            interractionToggle = !interractionToggle;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (interType == InterractionType.TriggerExit && other.gameObject.CompareTag("Player"))
        {
            AkUnitySoundEngine.PostEvent(newEvent.Name, this.gameObject);
            interractionToggle = !interractionToggle;
        }
        
    }


}
