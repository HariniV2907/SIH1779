using System.Collections;
using UnityEngine;

public class LHandColliderScript : MonoBehaviour
{
    public string targetTag = "Hand_L";

    public Transform sourceTransform; // Assign the source transform in the inspector
    public Transform targetTransform; // Assign the target transform in the inspector

    public GameObject[] objectsToDisable; // Assign objects to disable in the inspector

    public float transformationInterval = 0.1f; // Time interval for continuous transformation
    public float cooldownDuration = 2f; // Duration of cooldown between transformations

    private bool isCooldown = false; // Tracks if the cooldown period is active
    private Coroutine transformationCoroutine; // Reference to the running transformation coroutine

    // Materials for switching
    public Material newMaterial;        // Material to apply during snapping
    private Material originalMaterial;  // Original material to revert back to

    private MeshRenderer targetRenderer; // Reference to the renderer of the target

    // Tolerance for snapping
    public float positionTolerance = 0.1f; // How close the positions need to be for snapping
    public float rotationTolerance = 5f; // How close the rotations (in degrees) need to be for snapping

    private void Start()
    {
        // Get the renderer from the targetTransform
        if (targetTransform != null)
        {
            targetRenderer = targetTransform.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                // Store the original material of the target
                originalMaterial = targetRenderer.material;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCooldown && other.CompareTag(targetTag) && sourceTransform != null && targetTransform != null)
        {
            // Check if the target is correctly positioned and rotated
            if (IsCorrectlyPlaced())
            {
                // Start coroutine to continuously apply the transformation and handle material switching
                if (transformationCoroutine == null) // Ensure only one instance runs
                {
                    transformationCoroutine = StartCoroutine(ContinuousTransformation());
                }
            }
        }
    }

    private IEnumerator ContinuousTransformation()
    {
        isCooldown = true;

        // Apply the new material to the target
        if (targetRenderer != null && newMaterial != null)
        {
            targetRenderer.material = newMaterial;
        }

        // Loop for continuous snapping while cooldown is active
        while (isCooldown)
        {
            // Snap the transform values from sourceTransform to targetTransform
            targetTransform.position = sourceTransform.position;
            targetTransform.rotation = sourceTransform.rotation;

            yield return new WaitForSeconds(transformationInterval);
        }

        // At the end of snapping, disable specified objects
        DisableObjects();
        transformationCoroutine = null; // Reset the coroutine reference when done
    }

    private void DisableObjects()
    {
        // Disable each object in the array
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Start the cooldown and revert the material after disabling the objects
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        // Revert the target object's material to its original state
        if (targetRenderer != null && originalMaterial != null)
        {
            targetRenderer.material = originalMaterial;
        }

        // End the cooldown
        isCooldown = false;
    }

    // Method to check if the object is close enough in position and rotation
    private bool IsCorrectlyPlaced()
    {
        // Check position distance
        float positionDistance = Vector3.Distance(targetTransform.position, sourceTransform.position);
        if (positionDistance > positionTolerance)
        {
            return false; // Not close enough in position
        }

        // Check rotation difference (using Quaternion angle)
        float rotationDifference = Quaternion.Angle(targetTransform.rotation, sourceTransform.rotation);
        if (rotationDifference > rotationTolerance)
        {
            return false; // Not close enough in rotation
        }

        return true; // Both position and rotation are within tolerance
    }
}
