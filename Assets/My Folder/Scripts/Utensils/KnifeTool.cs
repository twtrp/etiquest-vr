using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class KnifeTool : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public GrabInteractable grabInteractable;
    public HandGrabInteractable handGrabInteractable;

    private float lastCutTime = -Mathf.Infinity;
    private float cutCooldown = 0.5f; //seconds

    // Start is called before the first frame update
    void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<GrabInteractable>();

        if (handGrabInteractable == null)
            handGrabInteractable = GetComponent<HandGrabInteractable>();
    }

    void FixedUpdate()
    {
        bool isHeld =
            (grabInteractable != null && grabInteractable.Interactors.Count > 0) ||
            (handGrabInteractable != null && handGrabInteractable.State == InteractableState.Select);

        if (!isHeld)
            return;

        if (Time.time - lastCutTime < cutCooldown)
            return;

        if (Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("KnifeTarget") || hit.transform.CompareTag("KnifeAndForkTarget"))
            {
                KnifeTarget target = hit.transform.GetComponent<KnifeTarget>();
                if (target != null)
                {
                    Vector3 velocity = velocityEstimator.GetVelocityEstimate();
                    Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

                    target.Cut(endSlicePoint.position, planeNormal);
                    lastCutTime = Time.time;
                }
            }
        }
    }
}
