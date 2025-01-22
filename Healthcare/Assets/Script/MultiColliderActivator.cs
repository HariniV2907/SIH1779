using UnityEngine;

public class MultiColliderActivator : MonoBehaviour
{
    public Collider collider1; // Reference to the first collider (main)
    public Collider collider2; // Reference to the second collider
    public Collider collider3; // Reference to the third collider
    public MonoBehaviour scriptToActivate; // Reference to the script to activate

    private bool isScriptActivated = false; // Tracks if the script has been activated
    private bool isCollider2Touched = false; // Tracks if collider1 is touching collider2
    private bool isCollider3Touched = false; // Tracks if collider1 is touching collider3

    private void Start()
    {
        // Ensure the target script is initially disabled
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = false;
        }
        else
        {
            Debug.LogError("Script to activate is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if collider1 is touching collider2
        if (other == collider2)
        {
            isCollider2Touched = true;
            Debug.Log("Collider 1 touched Collider 2.");
        }

        // Check if collider1 is touching collider3
        if (other == collider3)
        {
            isCollider3Touched = true;
            Debug.Log("Collider 1 touched Collider 3.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset touch state for collider2
        if (other == collider2)
        {
            isCollider2Touched = false;
            Debug.Log("Collider 1 no longer touching Collider 2.");
        }

        // Reset touch state for collider3
        if (other == collider3)
        {
            isCollider3Touched = false;
            Debug.Log("Collider 1 no longer touching Collider 3.");
        }
    }

    // Function to check and activate the script
    public void CheckAndActivateScript()
    {
        if (!isScriptActivated && (isCollider2Touched || isCollider3Touched))
        {
            ActivateScript();
        }
        else
        {
            Debug.Log("Conditions not met or script already activated.");
        }
    }

    private void ActivateScript()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = true;
            isScriptActivated = true; // Prevent reactivation
            Debug.Log("Script activated as Collider 1 touched Collider 2 or Collider 3.");
        }
    }
}
