using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class Canvas : MonoBehaviour
{
    // Array to hold the TextMeshProUGUI objects
    public TextMeshProUGUI[] textMeshes;

    // Variable to track the current active text index
    private int currentTextIndex = 0;

    void Start()
    {
        // Disable all TextMeshPro objects except the first one
        for (int i = 0; i < textMeshes.Length; i++)
        {
            textMeshes[i].gameObject.SetActive(i == 0); // Only the first TextMeshPro is active
        }
    }

    // Call this function to move to the next TextMeshPro object
    public void ShowNextTextMesh()
    {
        if (currentTextIndex < textMeshes.Length - 1)
        {
            // Disable the current TextMeshPro object
            textMeshes[currentTextIndex].gameObject.SetActive(false);

            // Move to the next TextMeshPro object
            currentTextIndex++;

            // Enable the next TextMeshPro object
            textMeshes[currentTextIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("All TextMeshPro objects have been displayed.");
        }
    }
}
