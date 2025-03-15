using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 10;

    private void Start()
    {
        // Destroy projectile after a certain time
        // Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (gameObject != null)
        {
            // Move the projectile forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision logic here
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit! Damage: " + damage);
            // Apply damage to player (add player health script logic here)

            // have a player global health

            // have a if for a hit bee
        }

        // Destroy the projectile on impact
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}