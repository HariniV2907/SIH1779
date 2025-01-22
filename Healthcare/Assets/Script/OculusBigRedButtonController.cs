using UnityEngine;
using TMPro;

public class OculusBigRedButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Animator for the Big Red Button
    public TextMeshProUGUI countText; // TextMeshPro for displaying the count
    public string animationTriggerName = "Press"; // Trigger for the button animation
    public int maxPressCount = 30; // Maximum number of presses allowed

    private int pressCount = 0; // Current press count

    // Called by the button's interaction event
    public void OnButtonPress()
    {
        if (pressCount < maxPressCount)
        {
            pressCount++;

            // Trigger the button animation
            if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger(animationTriggerName);
            }
            else
            {
                Debug.LogError("Animator is not assigned to the button!");
            }

            // Update the count text
            UpdateCountText();

            // If max presses reached, disable the button
            if (pressCount == maxPressCount)
            {
                Debug.Log("Maximum press count reached. Disabling button.");
                DisableButton();
            }
        }
    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = $"Presses: {pressCount}/{maxPressCount}";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI for the count text is not assigned!");
        }
    }

    private void DisableButton()
    {
        // Optionally disable interaction
        GetComponent<Collider>().enabled = false; // Disables physical interactions
    }
}
