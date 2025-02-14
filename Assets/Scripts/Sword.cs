using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("HitCollider")) return;

        Dummy dummy = other.GetComponentInParent<Dummy>();
        if(dummy == null) return;

        dummy.Hit();
    }
}
