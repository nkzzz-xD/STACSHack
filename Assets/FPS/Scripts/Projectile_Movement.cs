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
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}