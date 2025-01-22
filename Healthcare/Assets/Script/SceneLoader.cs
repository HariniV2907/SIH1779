using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName) && Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Invalid scene name. Ensure the name matches exactly and is added to Build Settings.");
        }
    }
}
