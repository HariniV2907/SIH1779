using UnityEngine;

public class AnimController_Stop : MonoBehaviour
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
    private bool isTimerRunning = false; // To track timer state
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

        // If animation is running, stop it, otherwise start it
        if (isTimerRunning)
        {
            StopAnimation(); // Stop the animation if it's currently running
        }
        else
        {
            ActivateAnimation(); // Start the animation if it's not already running
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

        timer = 0f;
        isTimerRunning = true;

        // Start the animation loop
        PlayAnimation();

        Debug.Log($"Animation triggered: {animationTriggerName}");
    }

    private void Update()
    {
        if (isTimerRunning)
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
        if (objectAnimator == null || !isTimerRunning) return;

        // Trigger the animation
        objectAnimator.SetTrigger(animationTriggerName);

        // Schedule the next play based on animation length
        Invoke(nameof(PlayAnimation), animationLength);
    }

    // Function to stop the animation
    private void StopAnimation()
    {
        isTimerRunning = false;

        if (objectAnimator == null) return;

        // Cancel any pending invocations of PlayAnimation
        CancelInvoke(nameof(PlayAnimation));

        // Deactivate the Animator
        objectAnimator.enabled = false;

        // Reset to the initial position and rotation
        targetObject.transform.position = initialPosition;
        targetObject.transform.rotation = initialRotation;

        Debug.Log($"Animation stopped. Object reset to initial position.");
    }
}
