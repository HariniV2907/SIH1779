using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    // Reference to the object to enable and disable
    public GameObject targetObject;

    // Time in seconds the object should remain enabled
    public float duration = 2f;

    // Start is called before the first frame update
    public void EnableAndDisable()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Enable the object
            Invoke(nameof(DisableObject), duration); // Schedule disabling the object
        }
        else
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }

    // Method to disable the object
    private void DisableObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
