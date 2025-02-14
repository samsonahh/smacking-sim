using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Sword sword;
    [SerializeField] float aimDistanceFromCamera = 20f;

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        Vector3 aimPosition = GetCurrentAimPosition();
        sword.transform.rotation = Quaternion.LookRotation(aimPosition);
    }

    private Vector3 GetCurrentAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * aimDistanceFromCamera;
    }
}
