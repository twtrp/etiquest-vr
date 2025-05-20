using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCheck : MonoBehaviour
{
    public HandGrabInteractor leftHand;
    public HandGrabInteractor rightHand;

    public GameObject expectedLeftItem;
    public GameObject expectedRightItem;

    void Update()
    {
        var leftHeld = leftHand?.Interactable;
        var rightHeld = rightHand?.Interactable;

        bool leftOK = leftHeld != null && leftHeld.gameObject == expectedLeftItem;
        bool rightOK = rightHeld != null && rightHeld.gameObject == expectedRightItem;

        if (leftHeld != null || rightHeld != null)
        {
            Debug.Log($"Left Held: {(leftHeld != null ? leftHeld.gameObject.name : "None")}");
            Debug.Log($"Right Held: {(rightHeld != null ? rightHeld.gameObject.name : "None")}");
            Debug.Log($"Left OK: {leftOK}, Right OK: {rightOK}");
        }

        if (leftOK && rightOK)
        {
            Debug.Log("Both hands have the correct items!");
            // Trigger next logic
        }
    }
}
