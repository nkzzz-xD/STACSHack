using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign Projectile_Blaster prefab in Inspector
    public Transform firePoint; // The point from which projectiles are fired
    public float attackRange = 10f;
    public float fireRate = 1f; // How often the enemy fires

    private Transform player;
    private float nextFireTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (Time.time >= nextFireTime)
            {
                Attack();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void Attack()
    {
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        //directionToPlayer.y = 0; // This ensures the firePoint only rotates horizontally

    // Rotate the firePoint to face the player
        firePoint.rotation = Quaternion.LookRotation(directionToPlayer);

    // Instantiate the projectile at the firePoint's position and rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    // Optional: Visualize the attack range in Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}