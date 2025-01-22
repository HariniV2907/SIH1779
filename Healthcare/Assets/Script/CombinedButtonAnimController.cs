using UnityEngine;
using TMPro;

public class CombinedButtonAnimController : MonoBehaviour
{
    public Animator buttonAnimator;
    public TextMeshProUGUI countText;
    public int maxPressCount = 30;

    public GameObject targetObject;
    public string animationTriggerName;
    public string triggeringTag = "Player"; // Tag of the object that can trigger the interaction
    public float animationDuration = 30f;
    public float animationLength = 8f;

    private int pressCount = 0;
    private float timer;
    private bool isTimerRunning = false;
    private Animator objectAnimator;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool isTask1Complete = false; // Task completion flag

    // Trigger event when a collider with the specified tag enters this object's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag)) // Check if the collider's tag matches the triggering tag
        {
            if (pressCount < maxPressCount)
            {
                pressCount++;
                UpdateCountText();

                if (buttonAnimator != null)
                {
                    buttonAnimator.SetTrigger(gameObject.tag); // Use the button's tag as the trigger
                    Debug.Log($"Button animation triggered with tag: {gameObject.tag}");
                }
                else
                {
                    Debug.LogError("Animator is not assigned to the button!");
                }

                if (!isTimerRunning)
                {
                    ActivateAnimation();
                }

                if (pressCount == maxPressCount)
                {
                    isTask1Complete = true; // Task completed
                    Debug.Log("Task 1 is complete!");
                    DisableButton();
                    StopAnimation();
                    DisableTextBox(); // Disable the text box when task is completed
                }
            }
        }
    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = $"Compressions: {pressCount}/{maxPressCount}";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI for the count text is not assigned!");
        }
    }

    private void DisableTextBox()
    {
        if (countText != null)
        {
            countText.gameObject.SetActive(false); // Disable the text box GameObject
            Debug.Log("Count text box has been disabled.");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI for the count text is not assigned!");
        }
    }

    private void DisableButton()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // Disable the button collider to prevent further presses
        }
        else
        {
            Debug.LogError("Collider component not found on the button!");
        }
    }

    private void ActivateAnimation()
    {
        if (targetObject == null || string.IsNullOrEmpty(animationTriggerName))
        {
            Debug.LogError("Target object or animation trigger name is not assigned.");
            return;
        }

        objectAnimator = targetObject.GetComponent<Animator>();
        if (objectAnimator == null)
        {
            Debug.LogError("Animator component not found on the target object.");
            return;
        }

        initialPosition = targetObject.transform.position;
        initialRotation = targetObject.transform.rotation;

        timer = 0f;
        isTimerRunning = true;

        PlayAnimation();
        Debug.Log($"Animation triggered: {animationTriggerName}");
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= animationDuration || pressCount >= maxPressCount)
            {
                StopAnimation();
            }
        }
    }

    private void PlayAnimation()
    {
        if (objectAnimator == null || !isTimerRunning) return;
        objectAnimator.SetTrigger(animationTriggerName);
        Invoke(nameof(PlayAnimation), animationLength);
    }

    private void StopAnimation()
    {
        isTimerRunning = false;
        if (objectAnimator == null) return;

        CancelInvoke(nameof(PlayAnimation));
        objectAnimator.enabled = false;

        targetObject.transform.position = initialPosition;
        targetObject.transform.rotation = initialRotation;

        Debug.Log("Animation stopped. Object reset to initial position.");
    }
}
