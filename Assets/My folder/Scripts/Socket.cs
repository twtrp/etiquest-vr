using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus.Interaction;
using System.Collections;

public class Socket : MonoBehaviour
{
    public Transform snapPoint;
    public string targetTag = "Napkin";
    public GameObject successPanel;
    public FeedbackUIManager uiManager;
    public GameObject passButton;

    private bool hasSnapped = false;
    private Grabbable currentGrabbable;
    private Rigidbody currentRb;

    public GameObject hintPanel;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;                      // Only Napkin interaction

        Rigidbody rb = other.GetComponent<Rigidbody>();
        Grabbable grab = other.GetComponentInChildren<Grabbable>();

        if (rb == null || grab == null) return;

        /// If held, don't snap — but show hint
        if (grab.SelectingPointsCount > 0)
        {
            if (hasSnapped)
            {
                UnsnapObject(grab, rb);
            }

            if (!hasSnapped && uiManager != null)
            {
                uiManager.ShowHint("Release to place the napkin");
            }

            return;
        }

        // Snap only once
        if (!hasSnapped)
        {
            SnapObject(grab, rb, other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        // If leaving snap zone with grabbing, allow future snaps: PREVENT bugs
        if (hasSnapped)
        {
            //Grabbable grab = other.GetComponentInChildren<Grabbable>();
            //Rigidbody rb = other.GetComponent<Rigidbody>();

            //UnsnapObject(grab, rb);

            UnsnapObject(currentGrabbable, currentRb);
        }

        if (!hasSnapped && uiManager != null)
        {
            uiManager.ShowHint("Put the napkin back on your lap");
        }
    }

    private void SnapObject(Grabbable grab, Rigidbody rb, Transform target)
    {
        // Clean physics before freezing
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        target.SetPositionAndRotation(snapPoint.position, snapPoint.rotation);
        grab.enabled = false;

        // Flag
        hasSnapped = true;
        currentGrabbable = grab;
        currentRb = rb;

        //if (passButton != null)
        //    passButton.SetActive(true);

        uiManager.ShowHint("Good Job!!");
        Debug.Log("[Socket] Object snapped.");

        StartCoroutine(ShowSuccessAndLoadScene());              // win & change scene
    }

    private void UnsnapObject(Grabbable grab, Rigidbody rb)
    {
        rb.isKinematic = false;

        grab.enabled = true;

        // If parent has GrabInteractable/HandGrabInteractable, re-enable them
        //var interactables = grab.GetComponentsInParent<MonoBehaviour>();
        //foreach (var interactable in interactables)
        //{
        //    if (interactable is IInteractable)
        //    {
        //        ((MonoBehaviour)interactable).enabled = true;
        //    }
        //}

        hasSnapped = false;

        //if (passButton != null)
        //    passButton.SetActive(false);

        Debug.Log("[Socket] Object unsnapped.");
    }

    private IEnumerator ShowSuccessAndLoadScene()
    {
        if (successPanel != null)
            successPanel.SetActive(true);
            hintPanel.SetActive(false);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("EtiQuest Soup Test Scene (Three)");
    }
}