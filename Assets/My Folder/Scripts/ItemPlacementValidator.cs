using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Oculus.Interaction; // for OVRGrabbable

public class ItemPlacementValidator : MonoBehaviour
{
    [Header("Placement Settings")]
    public Transform target;            // Drag in your LapTarget here [don't let dev redundant drag LapTarget to target]
    public float snapDistance = 0.05f;  // 5cm snap threshold
    public bool isTutorialMode = true;  // Leave true for tutorial

    [Header("References (set these in Inspector)")]
    public RayHighlighter rayHighlighter;
    public FeedbackUIManager uiManager;

    private OVRGrabbable grabbable;
    private Rigidbody rb;
    private bool prevGrabbed = false;

    void Awake()
    {
        grabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();

        // Initialize rayHighlighter with the same target
        if (rayHighlighter != null)
            rayHighlighter.SetTarget(target);
    }

    private void Start()
    {
        uiManager.ShowHint("debug");
    }

    void Update()
    {
        bool isGrabbed = grabbable.isGrabbed;

        // 1. Detect grab start
        if (isTutorialMode && isGrabbed && !prevGrabbed)
            OnGrabStart();

        // 2. While held, update feedback
        if (isGrabbed)
            OnGrabMove();

        // 3. Detect grab end
        if (!isGrabbed && prevGrabbed)
            OnGrabRelease();

        prevGrabbed = isGrabbed;
    }

    private void OnGrabStart()
    {
        // Show the ray guiding to the target
        if (rayHighlighter != null)
            rayHighlighter.Show();

        // UI instruction
        uiManager.ShowHint("Put the napkin on your lap");
    }

    private void OnGrabMove()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        // Near-target preview (twice threshold)
        if (distance < snapDistance * 2f)
        {
            // Glow ray and item green
            if (rayHighlighter != null) rayHighlighter.SetColor(Color.green);
            uiManager.ShowHint("Almost there!");
        }
        else
        {
            if (rayHighlighter != null) rayHighlighter.SetColor(Color.white);
            uiManager.ShowHint("Put the napkin on your lap");
        }
    }

    private void OnGrabRelease()
    {
        // Hide the ray
        if (rayHighlighter != null)
            rayHighlighter.Hide();

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= snapDistance)
        {
            // Snap into place
            transform.position = target.position;
            transform.rotation = target.rotation;

            // Lock physics & grabbing
            rb.isKinematic = true;
            grabbable.enabled = false;

            uiManager.ShowSuccess("Napkin placed correctly!");
        }
        else
        {
            // Shake feedback
            StartCoroutine(ShakeAndRetry());
        }
    }

    System.Collections.IEnumerator ShakeAndRetry()
    {
        uiManager.ShowFail("Try again!");

        Vector3 original = transform.position;
        float elapsed = 0f;
        while (elapsed < 0.3f)
        {
            transform.position = original + Random.insideUnitSphere * 0.01f;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = original;
    }
}

