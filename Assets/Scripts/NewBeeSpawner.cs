using System.Linq;
using UnityEngine;

public class NewBeeSpawner : MonoBehaviour
{
    public GameObject spawnpoints;
    public GameObject bee;

    string[] beeNames = new string[]
{
    "Buzz Aldrin", "Bee-yoncé", "Bee Arthur", "Sting Crosby", "Buzz Lightyear", 
    "Bee-rnard", "Queen Buzzabeth", "Honey Boo Boo", "Pollen Parton", "Bee-nedict Cumberbee",
    "Beevis", "Wasabee", "Bee-thoven", "Bumble B.", "Bee-zus", 
    "McBuzz", "Hive Five", "Bee Diddy", "Beeatrix", "Bee Sharp",
    "Bee-atrix Potter", "Nectarine", "Bee-rnstein", "Winnie the Buzz", "Bee-zilla",
    "Bumble-ore", "Bee-tallica", "Buzzanova", "Bee-dazzle", "Bee-troot",
    "Buzzter Keaton", "Bee King", "Beetlejuice", "Bee-witched", "Bee-gonia",
    "Bee-rnie Sanders", "Bee Spears", "Bee Gee", "Bee-hive Mind", "Bee-licious",
    "Bee-thany", "Bee-moji", "Bee Happy", "Bee Gone", "Bee Cool",
    "Bee Kind", "Bee Yourself", "Beeautiful", "Queen Bee", "Bee-have",
    "Bee-wildered", "Bee-drill", "Bee-ginner", "Bee-merang", "Bee-yond",
    "Bee Ready", "Bee There", "Bee You", "Bee Wild", "Beezy",
    "Bee-low Zero", "Buzz LightBEEr", "Bee Mine", "Beeffalo", "BeeZilla",
    "Bee-witched", "Bee Smarter", "Bee-nana", "Bee-trayal", "Bee Keeper",
    "Bee-sy Bee", "Bee-rilliant", "Bee Coolio", "Bee-friend", "Bee-yond Limits",
    "Bee-loved", "Bee-trice", "Bee-thlehem", "Bee-tterfly", "Bee-troot Juice",
    "Bee-wonder", "BeeZ", "Bee-squatch", "Bee Sonic", "Bee-bop",
    "Bee-dazzling", "Bee-dy Eye", "Bee-ther", "Bee-storm", "Bee-Knighted",
    "Bee Happy Now", "Bee Wave", "BeeZooka", "Bee-ware", "Bee-tastic",
    "Bee-yond Borders", "Bee Chill", "Bee-bob", "Bee-think", "Bee Nice",
    "Bee-loved One", "Bee-mused", "Bee-rainstorm", "Bee Jamin", "Bee-noculars"
};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        System.Random random = new System.Random();
        int n = spawnpoints.transform.childCount;

        Transform[] allChildren = spawnpoints.GetComponentsInChildren<Transform>();

        allChildren = allChildren
            .OrderBy(x => Random.value) // Shuffle
            .Take(n/2) // Take first n/2
            .ToArray();

        foreach (Transform transform in allChildren) {
            GameObject myObj = Instantiate(bee, transform.position, transform.rotation);
            if (random.Next(0, 2) == 0) {
                myObj.GetComponent<Bee>().Alignment = "good";
            }
            else {
                myObj.GetComponent<Bee>().Alignment = "bad";
            }

            myObj.GetComponent<Bee>().Name = beeNames[random.Next(0, beeNames.Length)];
        }
    }
}
