using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    public UnityEvent onPress;
    private bool buttonPressed = false;

    public OVRInput.Button indexButton = OVRInput.Button.PrimaryIndexTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Button pressed");
        buttonPressed = true;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Button unpressed");
        buttonPressed = false;
    }

    private void Update()
    {
        if (buttonPressed)
        {
            Debug.Log("Button is being pressed");
            if (OVRInput.GetDown(indexButton))
            {
                onPress.Invoke();
                Debug.Log("Button pressed with index trigger!");
            }
        }
    }
}
