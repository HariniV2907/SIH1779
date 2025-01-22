using System.Collections;
using UnityEngine;
using TMPro;

public class SequentialElementController : MonoBehaviour
{
    [System.Serializable]
    public class Element
    {
        public TextMeshProUGUI textMeshPro;    // Reference to TextMeshPro
        public AudioSource audioSource;       // Reference to AudioSource
        public GameObject prefab;             // Reference to Prefab
        public GameObject animatedPrefab;     // Reference to Animated Prefab
        public string animationTrigger;       // Trigger for the Animator
        public UnityEngine.UI.RawImage rawImage; // Reference to RawImage
    }

    public Element[] elements; // Array of elements
    private int currentElementIndex = 0;     // Current active element index
    private int prefabActionCounter = 0;     // Counter for prefab actions

    void Start()
    {
        // Disable all elements and reset animations at the start
        foreach (var element in elements)
        {
            if (element.textMeshPro != null) element.textMeshPro.gameObject.SetActive(false);
            if (element.audioSource != null) element.audioSource.gameObject.SetActive(false);
            if (element.prefab != null) element.prefab.SetActive(false);
            if (element.rawImage != null) element.rawImage.gameObject.SetActive(false); // Disable RawImage

            // Reset animations for animated prefabs
            if (element.animatedPrefab != null)
            {
                Animator animator = element.animatedPrefab.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.ResetTrigger(element.animationTrigger); // Reset the trigger
                }
            }
        }

        // Enable the first element
        if (elements.Length > 0)
        {
            EnableElement(0);
        }
    }

    private void EnableElement(int index)
    {
        currentElementIndex = index;

        // Enable TextMeshPro and AudioSource of the current element
        if (elements[index].textMeshPro != null) elements[index].textMeshPro.gameObject.SetActive(true);
        if (elements[index].audioSource != null)
        {
            elements[index].audioSource.gameObject.SetActive(true);
            elements[index].audioSource.Play();
            StartCoroutine(HandleAudioCompletion(index));
        }

        // Enable the prefab (if applicable)
        if (elements[index].prefab != null)
        {
            elements[index].prefab.SetActive(true);
            prefabActionCounter = 0; // Reset prefab action counter
        }

        // Play the animation of the current element
        if (elements[index].animatedPrefab != null)
        {
            Animator animator = elements[index].animatedPrefab.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(elements[index].animationTrigger);
            }
        }
    }

    private IEnumerator HandleAudioCompletion(int index)
    {
        // Wait for the audio to finish playing
        yield return new WaitWhile(() => elements[index].audioSource != null && elements[index].audioSource.isPlaying);

        if (index == 1)
        {
            // For the second element, do nothing after audio (wait for prefab actions)
        }
        else if (index == 2)
        {
            // For the third element, wait for two prefab calls before disabling (handled in PrefabActionTriggered)
        }
        else if (index == 3)
        {
            // For the fourth element, disable the element entirely after audio completes
            DisableElement(index);
        }
        else if (index == 4)
        {
            // For the fifth element, leave it active for further behavior if required
        }
        else
        {
            // For other elements, disable the current element after audio finishes
            DisableElement(index);
        }
    }

    private void DisableElement(int index)
    {
        // Disable all components of the current element
        if (elements[index].textMeshPro != null) elements[index].textMeshPro.gameObject.SetActive(false);
        if (elements[index].audioSource != null) elements[index].audioSource.gameObject.SetActive(false);
        if (elements[index].prefab != null) elements[index].prefab.SetActive(false);

        // Keep the RawImage disabled unless explicitly activated for the third element
        if (index != 2 && elements[index].rawImage != null)
        {
            elements[index].rawImage.gameObject.SetActive(false);
        }

        // Enable the next element if available
        if (index + 1 < elements.Length)
        {
            EnableElement(index + 1);
        }
    }

    public void PrefabActionTriggered()
    {
        if (currentElementIndex == 1) // Special case for the second element
        {
            // Immediately disable element 2 after one call
            DisableElement(currentElementIndex);
        }
        else if (currentElementIndex == 2) // Special case for the third element
        {
            prefabActionCounter++;

            // Disable the third element after exactly two prefab function calls
            if (prefabActionCounter >= 2)
            {
                if (elements[currentElementIndex].textMeshPro != null)
                    elements[currentElementIndex].textMeshPro.gameObject.SetActive(false);

                DisableElement(currentElementIndex);
            }
        }
    }

    /// <summary>
    /// Function to explicitly enable the RawImage of the third element.
    /// </summary>
    public void EnableRawImageForElement3()
    {
        if (elements.Length > 2 && elements[2].rawImage != null)
        {
            elements[2].rawImage.gameObject.SetActive(true);
        }
    }
}
