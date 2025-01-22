using UnityEngine;

public class EnableGameObjectOnCompletion : MonoBehaviour
{
    // Boolean flags to be set by other scripts
    public bool isTask1Complete = false;
    public bool isTask2Complete = false;
    public bool isTask3Complete = false;
    public bool isTask4Complete = false;

    // GameObject to enable
    public GameObject targetObject;

    // Flag to prevent re-triggering the enable logic
    private bool isObjectEnabled = false;

    void Update()
    {
        // Check if all tasks are complete
        if (!isObjectEnabled && AllTasksCompleted())
        {
            EnableTargetObject();
        }
    }

    // Check if all boolean flags are true
    private bool AllTasksCompleted()
    {
        return isTask1Complete && isTask2Complete && isTask3Complete && isTask4Complete;
    }

    // Enable the target GameObject
    private void EnableTargetObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            isObjectEnabled = true; // Prevent re-triggering
            Debug.Log("Target object enabled!");
        }
        else
        {
            Debug.LogError("Target object is not assigned in the inspector!");
        }
    }
}
