using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; };
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // constraints on health levels for player

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
             if (!dead)
 {
     anim.SetTrigger("die");
     GetComponent<PlayerMovement>().enabled = false;
     dead = true;
 }
        }
    }

      private void Update()
  {
      if (Input.GetKeyDown(KeyCode.E))
          TakeDamage(1);
  }
}
