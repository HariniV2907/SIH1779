using UnityEngine;

public class TriggerAnimationOnCollision : MonoBehaviour
{
    public GameObject objectB; // Reference to Object B
    public GameObject objectC; // Reference to Object C (with Animator)
    public string animationTriggerName = "Activate"; // Name of the Animator trigger

    private Animator objectCAnimator;

    void Start()
    {
        if (objectC != null)
        {
            objectCAnimator = objectC.GetComponent<Animator>();
        }

        if (objectCAnimator == null)
        {
            Debug.LogError("Animator not found on Object C. Please attach an Animator component.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == objectB)
        {
            ActivateAnimation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectB)
        {
            ActivateAnimation();
        }
    }

    private void ActivateAnimation()
    {
        if (objectCAnimator != null)
        {
            objectCAnimator.SetTrigger(animationTriggerName);
        }
    }
}
