using UnityEngine;

public class HealPack : MonoBehaviour
{
    public int heal = 100;              // Amount to heal
    public float refreshTime = 20f;     // Time until refresh

    private bool available = true;      // Fixed typo: "boolean" -> "bool", "availble" -> "available"
    private float refreshTimer;         // Timer for refresh
    private Renderer renderer;          // To hide/show the object
    private Collider collider;          // To disable/enable collision

    void Start()
    {
        // Cache components for efficiency
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        
        if (renderer == null || collider == null)
        {
            Debug.LogError("HealPack needs a Renderer and Collider!");
        }
    }

    void Update()
    {
        if (!available)
        {
            refreshTimer -= Time.deltaTime;
            if (refreshTimer <= 0f)
            {
                RefreshHealPack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (available && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(heal);
                StartRefreshCooldown();
            }
        }
    }

    private void StartRefreshCooldown()
    {
        available = false;
        refreshTimer = refreshTime;
        
        // Visually disable the heal pack
        if (renderer != null) renderer.enabled = false;
        if (collider != null) collider.enabled = false;
    }

    private void RefreshHealPack()
    {
        available = true;
        
        // Re-enable the heal pack
        if (renderer != null) renderer.enabled = true;
        if (collider != null) collider.enabled = true;
        
        Debug.Log("Heal pack refreshed!");
    }
}