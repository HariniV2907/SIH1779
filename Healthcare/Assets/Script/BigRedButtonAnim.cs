using UnityEngine;

public class BigRedButtonAnim : MonoBehaviour
{
    // Reference to the prefab (GameObject with the Animator)
    public GameObject targetObject;

    // Name of the trigger to activate the animation
    public string triggerName;

    // Called when the button is pressed
    public void OnButtonPress()
    {
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

        // Get the Animator component from the target object
        Animator animator = targetObject.GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator component found on the target object.");
            return;
        }

        // Trigger the animation
        animator.SetTrigger(triggerName);

        Debug.Log($"Animation with trigger '{triggerName}' activated on the target object.");
    }
}
