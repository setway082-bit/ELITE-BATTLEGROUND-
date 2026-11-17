using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2.2f, -4.2f);
    public float sensitivity = 3f;
    public float minPitch = -25f, maxPitch = 65f;
    float yaw, pitch = 12f;

    void LateUpdate()
    {
        if (!target) return;
        if (Application.isEditor)
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity * 4f;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity * 4f;
        }
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.4f);
    }

    public void AddLookInput(Vector2 delta)
    {
        yaw += delta.x * sensitivity;
        pitch = Mathf.Clamp(pitch - delta.y * sensitivity, minPitch, maxPitch);
    }
}
