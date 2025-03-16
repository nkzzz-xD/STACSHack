using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthBar;

    void Awake()
    {
        // Ensure we update the slider as soon as possible
        if (playerHealth != null && healthBar != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthBar;
            // Set initial value in Awake to catch it before Start
            UpdateHealthBar(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(int current, int max)
    {
        if (healthBar != null)
        {
            float percentage = (float)current / max;
            healthBar.value = percentage;
            Debug.Log($"Health UI Updated: {current}/{max} = {percentage}");
        }
        else
        {
            Debug.LogWarning("Health bar slider is not assigned in the Inspector!");
        }
    }
}