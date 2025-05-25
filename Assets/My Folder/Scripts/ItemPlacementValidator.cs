//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;              // Is enough if OVRGrabbable is globally available

//[RequireComponent(typeof(Rigidbody))]
//public class ItemPlacementValidator : MonoBehaviour
//{
//    public LayerMask snapZoneLayer;
//    public Transform snapTarget;
//    public float snapDistance = 0.1f;
//    public bool isTutorialMode = true;
//    public RayHighlighter rayHighlighter;
//    public FeedbackUIManager uiManager;

//    private Rigidbody _rb;
//    private OVRGrabbable _grab;
//    private bool _insideZone = false;
//    private bool _hasSnapped = false;

//    void Awake()
//    {
//        _rb = GetComponent<Rigidbody>();
//        _grab = GetComponentInChildren<OVRGrabbable>();

//        if (snapTarget == null) Debug.LogError("Snap Target not assigned!");
//        if (rayHighlighter == null) Debug.LogError("Missing RayHighlighter.");
//        if (uiManager == null) Debug.LogError("Missing FeedbackUIManager.");

//        uiManager?.ShowHint("Objective: Put napkin on your lap.");
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (_hasSnapped) return;

//        if (((1 << other.gameObject.layer) & snapZoneLayer) != 0)
//        {
//            _insideZone = true;
//            if (isTutorialMode) uiManager.ShowHint("Release the napkin to place it.");
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (_hasSnapped) return;

//        if (((1 << other.gameObject.layer) & snapZoneLayer) != 0)
//        {
//            _insideZone = false;
//            if (isTutorialMode)
//            {
//                rayHighlighter?.Hide();
//                uiManager.ShowHint("Put the napkin back on your lap.");
//            }
//        }
//    }

//    void Update()
//    {
//        if (_hasSnapped || _grab == null || !_insideZone) return;

//        if (_grab.isGrabbed)
//        {
//            if (isTutorialMode)
//            {
//                float dist = Vector3.Distance(transform.position, snapTarget.position);
//                Color c = dist < snapDistance ? Color.green : Color.white;
//                rayHighlighter.DrawRay(transform.position, snapTarget.position, c);
//                uiManager.ShowHint(dist < snapDistance ? "Almost there—release now!" : "Drag closer to your lap.");
//            }
//        }
//        else
//        {
//            // Snap only after release inside the zone
//            SnapObject();
//        }
//    }

//    void SnapObject()
//    {
//        _hasSnapped = true;
//        _rb.isKinematic = true;
//        _rb.useGravity = false;
//        transform.SetPositionAndRotation(snapTarget.position, snapTarget.rotation);
//        _grab.enabled = false;
//        rayHighlighter?.Hide();
//        uiManager?.ShowSuccess("Correct! Napkin placed.");
//    }
//}
