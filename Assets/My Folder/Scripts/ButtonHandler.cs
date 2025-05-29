using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    public UnityEvent onPress;
    private bool buttonPressed = false;

    public OVRInput.Button button;

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
            if (OVRInput.GetDown(button))
            {
                onPress.Invoke();
                Debug.Log("Button pressed with index trigger!");
            }
        }
    }
}
