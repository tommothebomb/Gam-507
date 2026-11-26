using UnityEngine;

// Libby/Jordan Script \\
public class BiilboardUI : MonoBehaviour
{
    Camera mainCam;
    Transform camTransform;
    Quaternion ogRotation;


    private void Awake()
    {
        mainCam = Camera.main;
        gameObject.GetComponent<Canvas>().worldCamera = mainCam;
        camTransform = mainCam.transform;
    }

    private void Start()
    {
        ogRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = camTransform.rotation * ogRotation;
    }
}
