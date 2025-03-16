using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public int normalBeesAlive = 2;
    public int queenBeeAlive = 1;
    [SerializeField] private GameObject queenBeePrefab;  


    public void NormalBeeKilled()
    {
        normalBeesAlive -= 1;
        Debug.Log("Number of bees left: " + normalBeesAlive);
        if (normalBeesAlive == 0) {
            SpawnQueenBee();
        }
    }

    public void QueenBeeKilled() {

        // GameOver victory screen
    }
    private void SpawnQueenBee() {
        if (queenBeePrefab == null)
        {
            Debug.LogError("Cannot spawn Queen Bee: Prefab is null!");
            return;
        }

        Vector3 position = new Vector3(33.0f, 10.0f, 0.0f);
        GameObject newBee = Instantiate(queenBeePrefab, position, Quaternion.identity);

        if (newBee != null)
        {
            Debug.Log("Queen Spawned at " + newBee.transform.position);
            newBee.SetActive(true); // Ensure it’s active
        }
        else
        {
            Debug.LogError("Failed to instantiate Queen Bee!");
        }
    }

}
