using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // constraints on health levels for player

        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            //player dead
        }
    }
}
