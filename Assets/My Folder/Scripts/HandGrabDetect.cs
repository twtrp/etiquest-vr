using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabDetect : MonoBehaviour
{
    public HandGrabInteractor Hand;

    public GameObject expectedItem;

    void Update()
    {
        
        //var itemHeld = Hand.HasSelectedInteractable;

        //bool OK = itemHeld != null && itemHeld.gameObject == expectedItem;

        //if (itemHeld != null)
        //{
        //    if (OK)
        //    {
        //        Debug.Log("This hand have the correct items!");
        //    }
        //    else
        //    {
        //        Debug.Log("Wrong");
        //    }
        //}
        //else
        //{
        //    Debug.Log("None");
        //}
    }
}
