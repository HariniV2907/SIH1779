using UnityEngine;
using TMPro;

public class TouchCheckTMP : MonoBehaviour
{
    public string targetTag = "Target"; // Tag for the correct target
    public Transform correctPosition; // Correct position for the target
    public float positionTolerance = 0.1f; // Tolerance for position correctness

    public TextMeshProUGUI targetFeedbackText; // Text for the Target tag
    public TextMeshProUGUI i1FeedbackText; // Text for the i1 tag
    public TextMeshProUGUI i2FeedbackText; // Text for the i2 tag
    public TextMeshProUGUI i3FeedbackText; // Text for the i3 tag
    public TextMeshProUGUI i4FeedbackText; // Text for the i4 tag

    [TextArea] public string i1CustomText; // Custom text for i1
    [TextArea] public string i2CustomText; // Custom text for i2
    [TextArea] public string i3CustomText; // Custom text for i3
    [TextArea] public string i4CustomText; // Custom text for i4

    public AudioClip i1AudioClip;
    public AudioClip i2AudioClip;
    public AudioClip i3AudioClip;
    public AudioClip i4AudioClip;

    private AudioSource audioSource;

    private int targetCollisionCount = 0; // Tracks how many colliders are touching the target

    void Start()
    {
        // Ensure all feedback texts are hidden initially
        HideAllText();

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollisionEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollisionEnter(other);
    }

    private void OnCollisionExit(Collision collision)
    {
        HandleCollisionExit(collision.collider);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleCollisionExit(other);
    }

    private void HandleCollisionEnter(Collider otherCollider)
    {
        // Handle the target collider
        if (otherCollider.CompareTag(targetTag))
        {
            targetCollisionCount++;
            UpdateTargetFeedback();
        }
        // Handle i1-i4 tags with custom text
        else if (otherCollider.CompareTag("i1"))
        {
            ShowText(i1FeedbackText, i1CustomText);
            PlayAudio(i1AudioClip);
        }
        else if (otherCollider.CompareTag("i2"))
        {
            ShowText(i2FeedbackText, i2CustomText);
            PlayAudio(i2AudioClip);
        }
        else if (otherCollider.CompareTag("i3"))
        {
            ShowText(i3FeedbackText, i3CustomText);
            PlayAudio(i3AudioClip);
        }
        else if (otherCollider.CompareTag("i4"))
        {
            ShowText(i4FeedbackText, i4CustomText);
            PlayAudio(i4AudioClip);
        }
    }

    private void HandleCollisionExit(Collider otherCollider)
    {
        // Handle the target collider
        if (otherCollider.CompareTag(targetTag))
        {
            targetCollisionCount = Mathf.Max(0, targetCollisionCount - 1); // Decrease count but prevent it from going below zero
            UpdateTargetFeedback();
        }
        // Handle i1-i4 tags
        else if (otherCollider.CompareTag("i1"))
        {
            HideText(i1FeedbackText);
        }
        else if (otherCollider.CompareTag("i2"))
        {
            HideText(i2FeedbackText);
        }
        else if (otherCollider.CompareTag("i3"))
        {
            HideText(i3FeedbackText);
        }
        else if (otherCollider.CompareTag("i4"))
        {
            HideText(i4FeedbackText);
        }
    }

    private void UpdateTargetFeedback()
    {
        if (targetFeedbackText == null)
        {
            Debug.LogWarning("Target feedback text is not assigned.");
            return;
        }

        if (targetCollisionCount >= 2)
        {
            ShowText(targetFeedbackText, "Correct");
        }
        else if (targetCollisionCount == 1)
        {
            ShowText(targetFeedbackText, "Correct");
        }
        else
        {
            HideText(targetFeedbackText);
        }
    }

    private void ShowText(TextMeshProUGUI textComponent, string message)
    {
        if (textComponent != null)
        {
            textComponent.gameObject.SetActive(true);
            textComponent.text = message;
        }
    }

    private void HideText(TextMeshProUGUI textComponent)
    {
        if (textComponent != null)
        {
            textComponent.gameObject.SetActive(false);
        }
    }

    private void HideAllText()
    {
        HideText(targetFeedbackText);
        HideText(i1FeedbackText);
        HideText(i2FeedbackText);
        HideText(i3FeedbackText);
        HideText(i4FeedbackText);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
