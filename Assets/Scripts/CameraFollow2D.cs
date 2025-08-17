using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float offsetX = 5f;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
