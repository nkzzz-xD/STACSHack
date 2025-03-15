using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;    // Maximum health value
    [SerializeField] private int currentHealth;      // Current health value
    
    public bool IsDead { get; private set; }         // Property to check if player is dead

    // Event to notify other systems when health changes (optional, for UI updates)
    public System.Action<int, int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;                   // Initialize health
        IsDead = false;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);  // Update UI if hooked up
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;                          // Don't take damage if already dead

        currentHealth = Mathf.Max(0, currentHealth - damage);  // Reduce health, min 0
        OnHealthChanged?.Invoke(currentHealth, maxHealth);     // Notify listeners
        
        Debug.Log($"Player took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;                          // Can't heal if dead

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);  // Increase health, max at maxHealth
        OnHealthChanged?.Invoke(currentHealth, maxHealth);            // Notify listeners
        Debug.Log($"Player healed for {amount}. Health: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        IsDead = true;
        // Add death logic here (e.g., play animation, disable controls, show game over)
        Debug.Log("Player has died!");
        gameObject.SetActive(false); 
    }

    // Getter methods for UI or other systems
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => (float)currentHealth / maxHealth;
}