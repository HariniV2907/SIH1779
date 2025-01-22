using UnityEngine;

public class AnimController : MonoBehaviour
{
    // Components for Button Press functionality
    public Animator buttonAnimator; // Animator for the Big Red Button
    public string buttonAnimationTriggerName = "AnimPress"; // Trigger for the button animation

    // Components for Animation functionality
    public GameObject targetObject; // Reference to the prefab or GameObject
    public string animationTriggerName; // Trigger to activate the animation
    public float animationDuration = 30f; // Animation duration in seconds
    public float animationLength = 8f; // Length of a single animation loop in seconds

    private float timer; // Internal timer
    private bool isAnimationActive = false; // To track animation state
    private Animator objectAnimator; // Reference to the Animator component
    private Vector3 initialPosition; // To store the initial position
    private Quaternion initialRotation; // To store the initial rotation

    // Called by the button's interaction event
    public void OnButtonPress()
    {
        // Trigger the button animation
        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger(buttonAnimationTriggerName);
        }
        else
        {
            Debug.LogError("Animator is not assigned to the button!");
        }

        // Start the target object animation if not already running
        if (!isAnimationActive)
        {
            ActivateAnimation();
        }
    }

    // Function to activate the animation and start the timer
    private void ActivateAnimation()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        if (string.IsNullOrEmpty(animationTriggerName))
        {
            Debug.LogError("Animation trigger name is not assigned.");
            return;
        }

        objectAnimator = targetObject.GetComponent<Animator>();
        if (objectAnimator == null)
        {
            Debug.LogError("Animator component not found on the target object.");
            return;
        }

        // Store the initial position and rotation
        initialPosition = targetObject.transform.position;
        initialRotation = targetObject.transform.rotation;

        // Reset timer and animation state
        timer = 0f;
        isAnimationActive = true;

        // Activate the Animator in case it was disabled earlier
        objectAnimator.enabled = true;

        // Trigger the animation
        PlayAnimation();

        Debug.Log($"Animation triggered: {animationTriggerName}");
    }

    private void Update()
    {
        if (isAnimationActive)
        {
            timer += Time.deltaTime;

            // Stop animation if duration exceeds
            if (timer >= animationDuration)
            {
                StopAnimation();
            }
        }
    }

    // Function to play the animation
    private void PlayAnimation()
    {
        if (objectAnimator == null || !isAnimationActive) return;

        // Trigger the animation
        objectAnimator.SetTrigger(animationTriggerName);

        Debug.Log("Playing animation.");
    }

    // Function to stop the animation
    private void StopAnimation()
    {
        isAnimationActive = false;

        if (objectAnimator == null) return;

        // Deactivate the Animator
        objectAnimator.enabled = false;

        // Reset to the initial position and rotation
        targetObject.transform.position = initialPosition;
        targetObject.transform.rotation = initialRotation;

        Debug.Log($"Animation stopped. Object reset to initial position.");
    }
}
