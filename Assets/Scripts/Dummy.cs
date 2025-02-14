using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator animator;

    [SerializeField] float hitCooldown = 0.5f;
    float hitCooldownTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hitCooldownTimer < hitCooldown) hitCooldownTimer += Time.deltaTime;
    }

    public void Hit()
    {
        if (hitCooldownTimer < hitCooldown) return;

        hitCooldownTimer = 0f;
        animator.SetTrigger("Hit");
    }
}
