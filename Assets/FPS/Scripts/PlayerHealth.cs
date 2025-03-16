using UnityEngine;
using TMPro;  // Import for TextMeshPro
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;    // Maximum health value
    [SerializeField] private int currentHealth;      // Current health value
    public Image blackScreen;
    public float fadeSpeed = 1.5f;  // Speed of fade effect
    private bool fadeToBlack = false;
    private bool blackoutComplete = false;  // Track if the blackout is complete
    public TextMeshProUGUI canvasText; // Assign in Inspector
    public TextMeshProUGUI winningText;
    public bool IsDead { get; private set; }         // Property to check if player is dead

    // Event to notify other systems when health changes (optional, for UI updates)
    public System.Action<int, int> OnHealthChanged;

    void Start()
    {
        // Ensure the panel is invisible at the start
        if (blackScreen != null)
        {
            Color initialColor = blackScreen.color;
            initialColor.a = 0;  // Set initial alpha to 0 (invisible)
            blackScreen.color = initialColor;
        }

        // Hide the text initially
        if (canvasText != null)
        {
            Color textColor = canvasText.color;
            textColor.a = 0;  // Make text invisible
            canvasText.color = textColor;

            // Center the text alignment (optional via script)
            canvasText.alignment = TextAlignmentOptions.Center;
        }

        // Hide the text initially
        if (winningText != null)
        {
            Color textColor = winningText.color;
            textColor.a = 0;  // Make text invisible
            winningText.color = textColor;

            // Center the text alignment (optional via script)
            winningText.alignment = TextAlignmentOptions.Center;
        }

        currentHealth = maxHealth;                   // Initialize health
        IsDead = false;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);  // Update UI if hooked up
    }

    void Update()
    {
        if (fadeToBlack)
        {
            // Gradually increase alpha to 1 (blackout effect)
            Color newColor = blackScreen.color;
            newColor.a = Mathf.Lerp(newColor.a, 1, fadeSpeed * Time.deltaTime);
            blackScreen.color = newColor;

            // Stop fading once the screen is fully black
            if (newColor.a >= 0.99f)
            {
                fadeToBlack = false;
                blackoutComplete = true;  // Set the blackout as complete
            }

            if (canvasText != null)
            {
                // Fade in text by adjusting the alpha value of the text color
                Color textColor = canvasText.color;
                textColor.a = Mathf.Lerp(textColor.a, 1, fadeSpeed * Time.deltaTime); // Fade in text alpha

                // Ensure text is white and fully visible
                if (textColor.a > 0.99f)
                {
                    textColor.r = 1f;  // Red component (white)
                    textColor.g = 1f;  // Green component (white)
                    textColor.b = 1f;  // Blue component (white)
                }

                canvasText.color = textColor;  // Apply new color with updated alpha
                canvasText.fontSize = 100;  // Adjust font size
            }
        }

        // If the blackout is complete, stop everything (pause the game)
        if (blackoutComplete)
        {
            Time.timeScale = 0f;  // Pause the game
            // Optionally, disable any other game objects or do other tasks here
        }
    }

    // Call this method when you want to trigger the blackout (make the screen black)
    public void TriggerBlackout()
    {
        fadeToBlack = true;
        blackoutComplete = false;  // Reset blackout completion status
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
        IsDead = true;  // Set IsDead to true
        // Do whatever logic you need for death (e.g., play death animation, disable player, etc.)
        Debug.Log("Player has died!");

        // Trigger the blackout effect
        TriggerBlackout();  // Trigger the blackout screen
    }

    // Getter methods for UI or other systems
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => (float)currentHealth / maxHealth;
}