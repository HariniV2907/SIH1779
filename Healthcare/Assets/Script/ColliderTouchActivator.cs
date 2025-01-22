using UnityEngine;

public class ColliderTouchActivator : MonoBehaviour
{
    public Collider collider1; // Reference to the first collider
    public Collider collider2; // Reference to the second collider
    public MonoBehaviour scriptToActivate; // Reference to the script to activate

    private bool isCollider1Touched = false; // Tracks if collider1 is touched
    private bool isCollider2Touched = false; // Tracks if collider2 is touched

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
        // Check for collision with collider1
        if (other == collider1)
        {
            isCollider1Touched = true;
            Debug.Log("Collider 1 touched.");
        }

        // Check for collision with collider2
        if (other == collider2)
        {
            isCollider2Touched = true;
            Debug.Log("Collider 2 touched.");
        }

        // Activate the script if both colliders are touched
        if (isCollider1Touched && isCollider2Touched)
        {
            Debug.Log("Both colliders touched.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the touch state when colliders are no longer touched
        if (other == collider1)
        {
            isCollider1Touched = false;
            Debug.Log("Collider 1 no longer touched.");
        }

        if (other == collider2)
        {
            isCollider2Touched = false;
            Debug.Log("Collider 2 no longer touched.");
        }
    }

    // Function to call from OnSelect
    public void OnSelect()
    {
        if (isCollider1Touched && isCollider2Touched)
        {
            ActivateScript();
        }
        else
        {
            Debug.Log("Both colliders are not touched yet. OnSelect did nothing.");
        }
    }

    private void ActivateScript()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = true;
            Debug.Log("Script activated via OnSelect!");
        }
    }
}
