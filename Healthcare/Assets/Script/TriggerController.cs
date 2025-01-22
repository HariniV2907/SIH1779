using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator; // Reference to the Animator component

    [Header("Animation Triggers")]
    public string[] animationTriggers; // Public array to hold animation trigger names

    /// <summary>
    /// Activates a trigger in the Animator based on the specified index.
    /// </summary>
    /// <param name="triggerIndex">Index of the trigger in the animationTriggers array</param>
    public void ActivateTrigger(int triggerIndex)
    {
        // Validate the input index
        if (animationTriggers == null || animationTriggers.Length == 0)
        {
            Debug.LogError("Animation triggers array is empty or null!");
            return;
        }

        if (triggerIndex < 0 || triggerIndex >= animationTriggers.Length)
        {
            Debug.LogError($"Invalid trigger index: {triggerIndex}. It must be between 0 and {animationTriggers.Length - 1}.");
            return;
        }

        // Validate the Animator
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
            return;
        }

        // Activate the specified trigger
        string triggerName = animationTriggers[triggerIndex];
        animator.SetTrigger(triggerName);
        Debug.Log($"Trigger activated: {triggerName}");
    }
}
