using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneFunction : MonoBehaviour
{
    public Transform cameraRigRoot;     // [BuildingBlock] Camera Rig
    public Transform centerEyeAnchor;   // CenterEyeAnchor (headset)

    private static Vector3 savedRigPosition = Vector3.zero;
    private static Quaternion savedRigRotation = Quaternion.identity;
    private static bool hasSavedTransform = false;

    private Vector3 initialHeadsetPosition;
    private Vector3 initialForward;

    public void Start()
    {
        if (centerEyeAnchor != null)
        {
            initialHeadsetPosition = centerEyeAnchor.position;

            initialForward = centerEyeAnchor.forward;
            initialForward.y = 0;
            initialForward.Normalize();
        }

        if (hasSavedTransform && cameraRigRoot != null)
        {
            cameraRigRoot.position = savedRigPosition;
            cameraRigRoot.rotation = savedRigRotation;
        }

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RecenterView()
    {
        if (cameraRigRoot == null || centerEyeAnchor == null)
        {
            Debug.LogWarning("Camera Rig or CenterEyeAnchor not assigned.");
            return;
        }

        Vector3 currentForward = centerEyeAnchor.forward;
        currentForward.y = 0;
        currentForward.Normalize();

        Quaternion rotationOffset = Quaternion.FromToRotation(currentForward, initialForward);
        cameraRigRoot.rotation = rotationOffset * cameraRigRoot.rotation;

        Vector3 currentHeadsetPosition = centerEyeAnchor.position;
        Vector3 positionOffset = initialHeadsetPosition - currentHeadsetPosition;
        cameraRigRoot.position += positionOffset;

        savedRigPosition = cameraRigRoot.position;
        savedRigRotation = cameraRigRoot.rotation;
        hasSavedTransform = true;
    }
}
