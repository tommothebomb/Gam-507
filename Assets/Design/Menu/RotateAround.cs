using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float rotateX;
    [SerializeField] float rotateY;
    [SerializeField] float rotateZ;

    void Update()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * speed * Time.deltaTime);    
    }
}
