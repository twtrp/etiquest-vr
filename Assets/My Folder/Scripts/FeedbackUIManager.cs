using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackUIManager : MonoBehaviour
{
    public TextMeshProUGUI hintText;   // assign in Inspector [Text TMP??]

    public void ShowHint(string msg)
    {
        hintText.text = msg;
        hintText.color = Color.white;
    }

    public void ShowSuccess(string msg)
    {
        hintText.text = msg;
        hintText.color = Color.green;
    }

    public void ShowFail(string msg)
    {
        hintText.text = msg;
        hintText.color = Color.red;
    }
}
