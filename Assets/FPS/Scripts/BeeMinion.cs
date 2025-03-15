using UnityEngine;

public class BeeMinion : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 5;
    private Transform player;
    private QueenBee queen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
                Destroy(gameObject); // Destroy on impact
            }
        }
    }

    public void SetQueen(QueenBee queenRef)
    {
        queen = queenRef;
    }

    void OnDestroy()
    {
        if (queen != null)
        {
            queen.OnBeeDestroyed(); // Notify Queen when destroyed
        }
    }
}