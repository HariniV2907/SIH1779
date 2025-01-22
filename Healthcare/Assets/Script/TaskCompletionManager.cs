using UnityEngine;
using TMPro;

public class TaskCompletionManager : MonoBehaviour
{
    public CombinedButtonAnimController script1;
    public GreenColliderScript_Score script2;
    public RedColliderScript_Score script3;
    public ColliderScript_Mask_Score script4;

    public GameObject rewardObject;
    public TextMeshProUGUI scoreText;
    public ParticleSystem[] particleSystems;

    public AudioSource completionAudio; // AudioSource for completion sound

    private bool allTasksCompleted = false;

    void Start()
    {
        // Initially disable the script
        this.enabled = false;
    }

    void Update()
    {
        if (!allTasksCompleted && CheckTasksCompletion())
        {
            allTasksCompleted = true;

            // Trigger the delayed activation
            StartCoroutine(ActivateAfterDelay(5f)); // Wait for 5 seconds before enabling the script
        }
    }

    private bool CheckTasksCompletion()
    {
        return script1 != null && script1.isTask1Complete &&
               script2 != null && script2.isTask2Complete &&
               script3 != null && script3.isTask3Complete &&
               script4 != null && script4.isTask4Complete;
    }

    private System.Collections.IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Enable this script to run its completion actions
        this.enabled = true;

        // Trigger completion actions
        DisplayCompletionMessage();
        TriggerParticleSystems();
        EnableRewardObject();
    }

    private void DisplayCompletionMessage()
    {
        if (scoreText != null)
        {
            scoreText.text = "Congratulations! You have completed the tasks successfully!";
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the inspector.");
        }
    }

    private void TriggerParticleSystems()
    {
        if (particleSystems != null && particleSystems.Length > 0)
        {
            foreach (var particleSystem in particleSystems)
            {
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
            }
        }

        // Play completion sound
        if (completionAudio != null)
        {
            completionAudio.Play();
        }
        else
        {
            Debug.LogError("Completion AudioSource is not assigned in the inspector.");
        }
    }

    private void EnableRewardObject()
    {
        if (rewardObject != null)
        {
            rewardObject.SetActive(true);
        }
    }
}
