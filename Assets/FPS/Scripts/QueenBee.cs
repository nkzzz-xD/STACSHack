using UnityEngine;
using TMPro;  // Import for TextMeshPro
using UnityEngine.UI;  // For Image UI manipulation

public class QueenBee : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;          // Queen has more health than player
    [SerializeField] private float moveSpeed = 2f;         // Slow movement
    [SerializeField] private GameObject beePrefab;         // Prefab for spawned bees
    [SerializeField] private float spawnRadius = 3f;       // Radius around Queen to spawn bees
    [SerializeField] private float spawnInterval = 5f;     // Time between spawns
    [SerializeField] private int maxSpawnedBees = 5;       // Limit on spawned bees

    public Image blackScreen;  // Reference to the black screen (we will change it to white)
    public TextMeshProUGUI canvasText;  // Reference to the text you want to show
    public float fadeSpeed = 1.5f;  // Speed of fading effect for the background
    public bool IsDead { get; private set; }  // Property to check if Queen is dead

    private int currentHealth;
    private float spawnTimer;
    private int spawnedBeeCount;

    void Start()
    {
        currentHealth = maxHealth;
        spawnTimer = spawnInterval;
        spawnedBeeCount = 0;

        // Ensure blackScreen is invisible initially
        if (blackScreen != null)
        {
            Color initialColor = blackScreen.color;
            initialColor.a = 0; // Invisible at the start
            blackScreen.color = initialColor;
        }

        // Ensure canvasText is invisible initially
        if (canvasText != null)
        {
            Color textColor = canvasText.color;
            textColor.a = 0; // Invisible at the start
            canvasText.color = textColor;
        }
    }

    void Update()
    {
        if (IsDead) return; // If Queen is dead, stop any updates

        // Spawn bees periodically
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f && spawnedBeeCount < maxSpawnedBees)
        {
            SpawnBee();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnBee()
    {
        if (beePrefab == null) return;

        // Random position within spawn radius
        Vector3 spawnPos = transform.position + (Random.insideUnitSphere * spawnRadius);
        spawnPos.y = transform.position.y; // Keep it at Queen's height (2D-ish plane)

        GameObject newBee = Instantiate(beePrefab, spawnPos, Quaternion.identity);
        spawnedBeeCount++;

        // Optional: Pass Queen reference to spawned bee for cleanup
        BeeMinion beeScript = newBee.GetComponent<BeeMinion>();
        if (beeScript != null) beeScript.SetQueen(this);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        Debug.Log($"Queen Bee took {damage} damage. Health: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        IsDead = true; // Mark Queen as dead

        // Trigger the whiteout effect
        TriggerWhiteout();

        // Disable Queen Bee gameplay (stop moving, attacking, etc.)
        gameObject.SetActive(false); // You can disable the Queen or just stop its actions

        Debug.Log("Queen Bee defeated!");
    }

    private void TriggerWhiteout()
    {
        if (blackScreen != null)
        {
            // Gradually turn the background white (from black or transparent)
            StartCoroutine(FadeToWhite());
        }

        if (canvasText != null)
        {
            // Fade in the text and change to black color
            canvasText.text = "QUEEN BEE DEFEATED!";
            StartCoroutine(FadeInText());
        }
    }

    private System.Collections.IEnumerator FadeToWhite()
    {
        // Gradually change the alpha to 1 (for white background)
        float startAlpha = blackScreen.color.a;
        while (blackScreen.color.a < 1f)
        {
            Color newColor = blackScreen.color;
            newColor.a = Mathf.Lerp(newColor.a, 1, fadeSpeed * Time.deltaTime);
            blackScreen.color = newColor;
            yield return null;
        }
    }

    private System.Collections.IEnumerator FadeInText()
    {
        // Gradually fade in the text
        float targetAlpha = 1f;
        Color textColor = canvasText.color;
        while (textColor.a < targetAlpha)
        {
            textColor.a = Mathf.Lerp(textColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            canvasText.color = textColor;
            yield return null;
        }

        // Ensure text color is black (in case of any issues with fading)
        canvasText.color = Color.black;
    }

    // Called by spawned bees when they die to update count
    public void OnBeeDestroyed()
    {
        spawnedBeeCount = Mathf.Max(0, spawnedBeeCount - 1);
    }
}