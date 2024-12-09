using UnityEngine;

public class Player_status : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public Healthbar healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    public void Heal(int amount)
    {
        int newHealth = currentHealth + amount;
        currentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        healthbar.SetHealth(currentHealth);
    }
}
