using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    public float horizontalSpeed = 2f; // Speed at which the sprite moves horizontally
    public float verticalSpeed = 0.5f; // Speed at which the sprite moves vertically (floating)
    public float floatingAmplitude = 0.5f; // Amplitude of the vertical floating movement
    public float leftLimit = -5f; // Customizable left boundary
    public float rightLimit = 5f; // Customizable right boundary
    public bool canFlip = true; // Option to enable or disable sprite flipping

    private Vector3 startPos; // Starting position of the sprite
    private bool movingRight = true; // Tracks the direction of movement

    void Start()
    {
        // Save the starting vertical position to base the floating movement on it
        startPos = transform.position;
    }

    void Update()
    {
        // Move horizontally, either right or left depending on the direction
        if (movingRight)
        {
            transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * horizontalSpeed * Time.deltaTime;
        }

        // Add floating vertical movement using a sine wave for smooth oscillation
        transform.position = new Vector3(transform.position.x, startPos.y + Mathf.Sin(Time.time * verticalSpeed) * floatingAmplitude, transform.position.z);

        // Check if the sprite reaches the custom right or left edges
        if (transform.position.x > rightLimit && movingRight)
        {
            // Flip direction to the left
            movingRight = false;
            FlipSprite();
        }
        else if (transform.position.x < leftLimit && !movingRight)
        {
            // Flip direction to the right
            movingRight = true;
            FlipSprite();
        }

        // Face the direction of movement
        if (canFlip)
        {
            if (movingRight && transform.localScale.x < 1)
            {
                FlipSprite();
            }
            else if (!movingRight && transform.localScale.x > 0)
            {
                FlipSprite();
            }
        }
    }

    // Function to flip the sprite on the X-axis
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the x-axis
        transform.localScale = scale;
    }
}
