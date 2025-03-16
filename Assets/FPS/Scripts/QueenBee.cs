using UnityEngine;

public class QueenBee : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;          // Queen has more health than player
    [SerializeField] private float moveSpeed = 2f;        // Slow movement
    [SerializeField] private GameObject beePrefab;        // Prefab for spawned bees
    [SerializeField] private float spawnRadius = 3f;      // Radius around Queen to spawn bees
    [SerializeField] private float spawnInterval = 5f;    // Time between spawns
    [SerializeField] private int maxSpawnedBees = 5;      // Limit on spawned bees

    private int currentHealth;
    private float spawnTimer;
    private int spawnedBeeCount;

    void Start()
    {
        currentHealth = maxHealth;
        spawnTimer = spawnInterval;
        spawnedBeeCount = 0;
    }

    void Update()
    {
        // Spawn bees periodically
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f && spawnedBeeCount < maxSpawnedBees)
        {
            SpawnBee();
            spawnTimer = spawnInterval;
        }
    }




    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        Debug.Log($"Queen Bee took {damage} damage. Health: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Queen Bee defeated!");
        Destroy(gameObject);
    }

    // Called by spawned bees when they die to update count
    public void OnBeeDestroyed()
    {
        spawnedBeeCount = Mathf.Max(0, spawnedBeeCount - 1);
    }
}