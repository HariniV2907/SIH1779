using System.Collections;
using UnityEngine;
using TMPro;

public class RedColliderScript_Score : MonoBehaviour
{
    public string targetTag = "AED_Red";
    public Transform sourceTransform;
    public Transform targetTransform;
    public GameObject[] objectsToDisable;

    public float transformationInterval = 0.1f;
    public float cooldownDuration = 2f;

    private bool isCooldown = false;
    private Coroutine transformationCoroutine;
    public Material newMaterial;
    private Material originalMaterial;
    private MeshRenderer targetRenderer;

    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    public TextMeshProUGUI pointsText;
    public bool isTask3Complete = false; // Task completion flag

    public AudioSource snapAudio; // Add AudioSource for snap sound

    private void Start()
    {
        UpdatePointsDisplay("");
        if (targetTransform != null)
        {
            targetRenderer = targetTransform.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                originalMaterial = targetRenderer.material;
            }
        }

        if (snapAudio == null)
        {
            Debug.LogError("Snap AudioSource is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCooldown && other.CompareTag(targetTag) && sourceTransform != null && targetTransform != null)
        {
            if (IsCorrectlyPlaced())
            {
                if (!isTask3Complete)
                {
                    DisplayMessage("Well Done!");
                    isTask3Complete = true; // Task completed
                    Debug.Log("Task 3 is complete!");

                    // Play snapping sound
                    if (snapAudio != null)
                    {
                        snapAudio.Play();
                    }
                }

                if (transformationCoroutine == null)
                {
                    transformationCoroutine = StartCoroutine(ContinuousTransformation());
                }
            }
        }
    }

    private IEnumerator ContinuousTransformation()
    {
        isCooldown = true;
        if (targetRenderer != null && newMaterial != null)
        {
            targetRenderer.material = newMaterial;
        }

        while (isCooldown)
        {
            targetTransform.position = sourceTransform.position;
            targetTransform.rotation = sourceTransform.rotation;
            yield return new WaitForSeconds(transformationInterval);
        }

        DisableObjects();
        transformationCoroutine = null;
    }

    private void DisableObjects()
    {
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        if (targetRenderer != null && originalMaterial != null)
        {
            targetRenderer.material = originalMaterial;
        }
        isCooldown = false;
    }

    private bool IsCorrectlyPlaced()
    {
        float positionDistance = Vector3.Distance(targetTransform.position, sourceTransform.position);
        float rotationDifference = Quaternion.Angle(targetTransform.rotation, sourceTransform.rotation);
        return positionDistance <= positionTolerance && rotationDifference <= rotationTolerance;
    }

    private void DisplayMessage(string message)
    {
        UpdatePointsDisplay(message);
        if (pointsText != null)
        {
            StartCoroutine(DisableTextAfterDelay(5f)); // Start coroutine to disable text after 5 seconds
        }
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (pointsText != null)
        {
            pointsText.gameObject.SetActive(false); // Disable the text after delay
        }
    }

    private void UpdatePointsDisplay(string message)
    {
        if (pointsText != null)
        {
            pointsText.text = message;
        }
        else
        {
            Debug.LogError("Points Text is not assigned!");
        }
    }
}