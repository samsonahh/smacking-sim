using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    [SerializeField] float sensitivity;
    [SerializeField] float minY = -90;
    [SerializeField] float maxY = 90;

    float xRot;
    float yRot;

    private void Update()
    {
        transform.position = followTransform.position;

        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensitivity;
        xRot -= mouseY * sensitivity;

        xRot = Mathf.Clamp(xRot, minY, maxY);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
