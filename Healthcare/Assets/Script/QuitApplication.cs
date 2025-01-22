using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Function to quit the application
    public void QuitApp()
    {
        // Logs a message to the console for debugging purposes
        Debug.Log("Application Quit");

        // Quits the application
        Application.Quit();

#if UNITY_EDITOR
        // Stops the play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}