using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // time between attacks
    [SerializeField] private Transform firePoint; // where fireballs are fired from
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        //pools fireballs so creates multiple fireballs immediately, then deactivates fireballs when it hits, and waits for it to be reused
    }

    
}

