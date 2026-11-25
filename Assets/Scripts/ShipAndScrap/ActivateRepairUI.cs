using UnityEngine;

// Libby/Jordan Script \\
public class ActivateRepairUI : MonoBehaviour
{
    [SerializeField] GameObject shipRepairUI;


    private void Start()
    {
        shipRepairUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            shipRepairUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            shipRepairUI.SetActive(false);
        }
    }
}
