using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class ForkTool : MonoBehaviour
{
    public Transform stickPoint;
    public GrabInteractable grabInteractable;
    public HandGrabInteractable handGrabInteractable;

    private GameObject stuckFood = null;

    // Start is called before the first frame update
    void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<GrabInteractable>();

        if (handGrabInteractable == null)
            handGrabInteractable = GetComponent<HandGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isHeld =
            (grabInteractable != null && grabInteractable.Interactors.Count > 0) ||
            (handGrabInteractable != null && handGrabInteractable.State == InteractableState.Select);

        if (stuckFood != null && !isHeld)
        {
            UnstickFood();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (stuckFood != null)
            return;

        if (other.CompareTag("ForkTarget") || other.CompareTag("KnifeAndForkTarget"))
        {
            StickFood(other.gameObject);
        }
    }

    void StickFood(GameObject food)
    {
        stuckFood = food;

        if (food.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        food.transform.SetParent(stickPoint, true);
    }

    void UnstickFood()
    {
        if (stuckFood.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        stuckFood.transform.SetParent(null, true);
        stuckFood = null;
    }
}
