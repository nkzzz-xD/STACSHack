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
        if (normalBeesAlive <= 0) {
            SpawnQueenBee();
        }
    }
    private void SpawnQueenBee()
    {
        if (queenBeePrefab == null) return;

        // Random position within spawn radius
        //Vector3 spawnPos = (10f,5f,10f);
        Vector3 position = new Vector3(1.0f, 2.0f, 3.0f);

        GameObject newBee = Instantiate(queenBeePrefab, position, Quaternion.identity);
    }

}
