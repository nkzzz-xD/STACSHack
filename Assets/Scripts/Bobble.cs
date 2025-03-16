using UnityEngine;

public class Bobble : MonoBehaviour
{
    public float amplitude = 0.25f;  // How high and low the object floats
    public float speed = 30f;        // How fast the object floats

    private Vector3 startPosition;

    private void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin((Time.time + startPosition.x + startPosition.z) * speed) * amplitude;

        // Set the object's position with the updated Y value
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
