using UnityEngine;
using TMPro;  // Import for TextMeshPro
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int normalBeesAlive = 2;
    public int queenBeeAlive = 1;
    [SerializeField] private GameObject queenBeePrefab;  
    public Image blackScreen;
    public float fadeSpeed = 1.5f;  // Speed of fade effect
    private bool fadeToBlack = false;
    private bool blackoutComplete = false;  // Track if the blackout is complete
    public TextMeshProUGUI canvasText; // Assign in Inspector

    public void NormalBeeKilled()
    {
        normalBeesAlive -= 1;
        Debug.Log("Number of bees left: " + normalBeesAlive);
        if (normalBeesAlive == 0) 
        {
            SpawnQueenBee();
        }
    }

    void Update()
    {
        if (fadeToBlack)
        {
            // Gradually increase alpha to 1 (blackout effect)
            Color newColor = blackScreen.color;
            newColor.a = Mathf.Lerp(newColor.a, 1, fadeSpeed * Time.deltaTime);
            blackScreen.color = newColor;

            // Stop fading once the screen is fully black
            if (newColor.a >= 0.99f)
            {
                fadeToBlack = false;
                blackoutComplete = true;  // Set blackout complete
            }

            if (canvasText != null)
            {
                // Fade in text by adjusting the alpha value of the text color
                Color textColor = canvasText.color;
                textColor.a = Mathf.Lerp(textColor.a, 1, fadeSpeed * Time.deltaTime); // Fade in text alpha

                // Ensure text is white and fully visible
                if (textColor.a > 0.99f)
                {
                    textColor.r = 1f;  // Red component (white)
                    textColor.g = 1f;  // Green component (white)
                    textColor.b = 1f;  // Blue component (white)
                }

                canvasText.color = textColor;  // Apply new color with updated alpha
                canvasText.fontSize = 100;  // Adjust font size
            }
        }

        // If the blackout is complete, stop everything (pause the game)
        if (blackoutComplete)
        {
            Time.timeScale = 0f;  // Pause the game
            // Optionally, disable any other game objects or do other tasks here
        }
    }

    // Call this method when you want to trigger the blackout (make the screen black)
    public void TriggerBlackout()
    {
        fadeToBlack = true;
        blackoutComplete = false;  // Reset blackout completion status
    }

    public void QueenBeeKilled() 
    {
        Debug.Log("Player has killed the Queen Bee");
        TriggerBlackout();  // Trigger blackout after killing the queen bee
    }

    private void SpawnQueenBee() 
    {
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