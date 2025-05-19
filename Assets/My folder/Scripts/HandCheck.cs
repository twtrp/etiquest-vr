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
        var leftHeld = leftHand.Interactable;
        var rightHeld = rightHand.Interactable;

        bool leftOK = leftHeld != null && leftHeld.gameObject == expectedLeftItem;
        bool rightOK = rightHeld != null && rightHeld.gameObject == expectedRightItem;

        Debug.Log($"leftHeld: {leftHeld}, rightHeld: {rightHeld}");
        Debug.Log($"leftOK: {leftOK}, rightOK: {rightOK}");

        if (leftOK && rightOK)
        {
            Debug.Log("Both hands have the correct items!");
            // Trigger next logic
        }
    }
}
