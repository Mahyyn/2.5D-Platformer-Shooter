using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxEffect : MonoBehaviour
{

    // Store the starting position of the background
    private float startPosition;
    // Store the reference to the main camera
    private GameObject cam;
    // A value to control the parallax effect strength
    [SerializeField] private float parallaxEffect;
    // The length of the background sprite
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        // Get the length of the background sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        // Find the main camera in the scene and store its reference
        cam = GameObject.Find("CM vcam1");
        // Store the starting position of the background
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the temporary position of the background
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        // Calculate the distance the background should move based on the camera position and the parallax effect strength
        float distance = (cam.transform.position.x * parallaxEffect);
        // Set the position of the background to the calculated values
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        // If the temporary position of the background is greater than the starting position + length, update the starting position
        if (temp > startPosition + length)
        {
            startPosition += length;
        }
        // If the temporary position of the background is less than the starting position - length, update the starting position
        else if (temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
