using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeeSpawner : MonoBehaviour
{
    public GameObject beePrefab;
    private List<Vector3> spawnPoints;
    private List<Vector3> availableSpawnPoints;

    private List<BeeData> beeList = new List<BeeData>();
    
    

    void Start(){
        spawnPoints = new List<Vector3>{
            new Vector3(10, 0, 10),
            new Vector3(-5, 0, 8),
            new Vector3(15, 0, -5),
            new Vector3(-10, 0, -15),
            new Vector3(0, 0, -20),
            new Vector3(20, 0, 5),
            new Vector3(-15, 0, -10),
            new Vector3(5, 0, 15),
            new Vector3(-8, 0, 12),
            new Vector3(2, 0, -25)
        };
        ShuffleList(spawnPoints);
        availableSpawnPoints = new List<Vector3>(spawnPoints); // Copy shuffled list

        LoadBeeData("BeeCharacter");
        ShuffleBeeList(); // Shuffle the bee list to get unique bees each time
        SpawnBees();
    }

    void ShuffleList(List<Vector3> list){
        for (int i = 0; i < list.Count; i++){
            Vector3 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

    }

    void ShuffleBeeList(){
        for (int i = 0; i < beeList.Count; i++){
            BeeData temp = beeList[i];
            int randomIndex = Random.Range(i, beeList.Count);
            beeList[i] = beeList[randomIndex];
            beeList[randomIndex] = temp;
        }
    }

    void LoadBeeData(string fileName)
    {
        // Load CSV file from Resources/CSVs folder
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/" + fileName); // Ensure your file is inside Resources/CSVs/ directory

        if (csvFile != null)
        {
            string[] csvLines = csvFile.text.Split('\n');
            for (int i = 1; i < csvLines.Length; i++) // Skip header row
            {
                string[] fields = csvLines[i].Split(',');

                BeeData bee = new BeeData
                {
                    Name = fields[0],
                    Role = fields[1],
                    Alignment = fields[2],
                    Dialogue = fields[3],
                    Difficulty = fields[4]
                };
                beeList.Add(bee);
            }
        }
        else
        {
            Debug.LogError("CSV file not found in Resources folder: " + fileName);
        }
    }

    void SpawnBees()
    {
        int easyBeesCount = 0;
        int mediumBeesCount = 0;
        int hardBeesCount = 0;

        foreach (BeeData beeData in beeList)
        {
            // Only spawn bees if there are available spawn points
            if (availableSpawnPoints.Count == 0)
            {
                Debug.LogWarning("No more available spawn points!");
                break;
            }

            // Check difficulty and spawn accordingly
            if (beeData.Difficulty == "Easy" && easyBeesCount < 5) // Spawn 5 Easy bees
            {
                SpawnBee(beeData);
                easyBeesCount++;
            }
            else if (beeData.Difficulty == "Medium" && mediumBeesCount < 5) // Spawn 5 Medium bees
            {
                SpawnBee(beeData);
                mediumBeesCount++;
            }
            else if (beeData.Difficulty == "Hard" && hardBeesCount < 5) // Spawn 5 Hard bees
            {
                SpawnBee(beeData);
                hardBeesCount++;
            }
        }
    }

    void SpawnBee(BeeData beeData)
    {
        if (availableSpawnPoints.Count == 0) return; // Exit if no spawn points available

        // Select a random spawn point and remove it from the available list
        Vector3 spawnPosition = availableSpawnPoints[0];
        availableSpawnPoints.RemoveAt(0);

        GameObject newBee = Instantiate(beePrefab, spawnPosition, Quaternion.identity);
        Bee beeScript = newBee.GetComponent<Bee>();
        beeScript.Setup(beeData);

        
    }
}
