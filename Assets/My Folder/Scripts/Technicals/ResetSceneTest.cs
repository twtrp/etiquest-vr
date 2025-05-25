using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneTest : MonoBehaviour
{
    public OVRInput.Button resetSceneButton;
    public OVRInput.Button recenterButton;

    public Transform cameraRigRoot;     // [BuildingBlock] Camera Rig
    public Transform centerEyeAnchor;   // CenterEyeAnchor (headset)

    private static Vector3 savedRigPosition = Vector3.zero;
    private static Quaternion savedRigRotation = Quaternion.identity;
    private static bool hasSavedTransform = false;

    private Vector3 initialHeadsetPosition;
    private Vector3 initialForward;

    void Start()
    {
        if (centerEyeAnchor != null)
        {
            // Save the initial headset position and facing direction
            initialHeadsetPosition = centerEyeAnchor.position;

            initialForward = centerEyeAnchor.forward;
            initialForward.y = 0;
            initialForward.Normalize();
        }

        // Restore camera rig position/rotation if saved from previous scene
        if (hasSavedTransform && cameraRigRoot != null)
        {
            cameraRigRoot.position = savedRigPosition;
            cameraRigRoot.rotation = savedRigRotation;
        }
    }

    void Update()
    {
        if (OVRInput.GetDown(resetSceneButton, OVRInput.Controller.RTouch))
        {
           ResetScene();
        }

        if (OVRInput.GetDown(recenterButton, OVRInput.Controller.RTouch))
        {
            RecenterView();
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RecenterView()
    {
        if (cameraRigRoot == null || centerEyeAnchor == null)
        {
            Debug.LogWarning("Camera Rig or CenterEyeAnchor not assigned.");
            return;
        }

        // Flatten headset forward
        Vector3 currentForward = centerEyeAnchor.forward;
        currentForward.y = 0;
        currentForward.Normalize();

        // Get rotation to align current forward with initial forward
        Quaternion rotationOffset = Quaternion.FromToRotation(currentForward, initialForward);
        cameraRigRoot.rotation = rotationOffset * cameraRigRoot.rotation;

        // Move rig to keep headset at initial position
        Vector3 currentHeadsetPosition = centerEyeAnchor.position;
        Vector3 positionOffset = initialHeadsetPosition - currentHeadsetPosition;
        cameraRigRoot.position += positionOffset;

        // Save this transform for the next scene load
        savedRigPosition = cameraRigRoot.position;
        savedRigRotation = cameraRigRoot.rotation;
        hasSavedTransform = true;
    }
}
