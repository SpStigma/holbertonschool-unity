using UnityEngine;
using Vuforia;

public class StatusObserver : MonoBehaviour
{
    public GameObject imageToTrack;
    private ObserverBehaviour observerBehaviour;
    public GameObject linkedinButton;
    public GameObject githubButton;
    public GameObject xButton;
    public Animator nameAnimator;
    public Animator jobAnimator;
    public Animator linkedinAnimator;
    public Animator githubAnimator;
    public Animator xAnimator;

    void Start()
    {
        // Get composant ObserverBehavior
        observerBehaviour = imageToTrack.GetComponent<ObserverBehaviour>();

        if (observerBehaviour == null)
        {
            Debug.LogError("ObserverBehaviour not found.");
            return;
        }

        // Capture the changement of status
        observerBehaviour.OnTargetStatusChanged += OnObserverStatusChanged;
    }

    private void OnObserverStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            linkedinButton.SetActive(true);
            githubButton.SetActive(true);
            xButton.SetActive(true);
            nameAnimator.SetBool("isFadeIn", true);
            jobAnimator.SetBool("isFadeIn", true);
            linkedinAnimator.SetBool("isTrigger", true);
            githubAnimator.SetBool("isTrigger", true);
            xAnimator.SetBool("isTrigger", true);
        }
        else
        {
            linkedinButton.SetActive(false);
            githubButton.SetActive(false);
            xButton.SetActive(false);
            nameAnimator.SetBool("isFadeIn", false);
            jobAnimator.SetBool("isFadeIn", false);
            linkedinAnimator.SetBool("isTrigger", false);
            githubAnimator.SetBool("isTrigger", false);
            xAnimator.SetBool("isTrigger", false);
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnObserverStatusChanged;
        }
    }
}
