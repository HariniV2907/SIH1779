using UnityEngine;

public class Anim : MonoBehaviour
{
    public GameObject targetObject; // Reference to the prefab or GameObject
    public string triggerName; // Trigger to activate the animation
    public float duration = 30f; // Animation duration in seconds
    public float animationLength = 8f; // Length of the animation in seconds

    private float timer; // Internal timer
    private bool isTimerRunning = false; // To track timer state
    private Animator animator; // Reference to the Animator component
    private Vector3 initialPosition; // To store the initial position
    private Quaternion initialRotation; // To store the initial rotation

    // Function to activate the animation and start the timer
    public void ActivateAnimation()
    {
        if (isTimerRunning)
        {
            Debug.Log("Animation is already running.");
            return;
        }

        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        if (string.IsNullOrEmpty(triggerName))
        {
            Debug.LogError("Trigger name is not assigned.");
            return;
        }

        animator = targetObject.GetComponent<Animator>();
        if (animator == null)
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

        Debug.Log($"Animation triggered: {triggerName}");
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                StopAnimation();
            }
        }
    }

    // Function to play the animation
    private void PlayAnimation()
    {
        if (animator == null) return;

        // Trigger the animation
        animator.SetTrigger(triggerName);

        // Schedule the next play based on animation length
        Invoke(nameof(PlayAnimation), animationLength);
    }

    // Function to stop the animation
    private void StopAnimation()
    {
        isTimerRunning = false;

        if (animator == null) return;

        // Cancel any pending invocations of PlayAnimation
        CancelInvoke(nameof(PlayAnimation));

        // Deactivate the Animator
        animator.enabled = false;

        // Reset to the initial position and rotation
        targetObject.transform.position = initialPosition;
        targetObject.transform.rotation = initialRotation;

        Debug.Log($"Animation stopped after {duration} seconds, and object reset to initial position.");
    }
}
