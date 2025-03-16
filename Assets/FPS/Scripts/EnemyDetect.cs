using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float speed = 3.0f;

    public float offset = 0f;

    private bool playerInRange;

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            playerInRange = distanceToPlayer <= detectionRange;

            if (playerInRange)
            {
                FollowPlayer();
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        float verticalSpeedMultiplier = 2.0f;  // Adjust this value as needed for faster vertical movement
        direction.y *= verticalSpeedMultiplier;
        
        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation  * Quaternion.Euler(0, offset, 0), Time.deltaTime * 5f);
    }

    // Optional: Visualize the detection range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}