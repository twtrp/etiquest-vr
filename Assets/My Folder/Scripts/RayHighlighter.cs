using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayHighlighter : MonoBehaviour
{
    private LineRenderer line;
    private Transform source;
    private Transform target;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false;
    }

    /// <summary>
    /// Call once at startup to tell the ray what to point at. [set this in ItemPlacementValidator for no redundant]
    /// </summary>
    public void SetTarget(Transform t)
    {
        target = t;
        source = transform; // assumes this script is on the Napkin
    }

    /// <summary>
    /// Turn on the ray.
    /// </summary>
    public void Show()
    {
        if (line != null && source != null && target != null)
            line.enabled = true;
    }

    /// <summary>
    /// Turn off the ray.
    /// </summary>
    public void Hide()
    {
        if (line != null)
            line.enabled = false;
    }

    /// <summary>
    /// Change its color (e.g. green when near).
    /// </summary>
    public void SetColor(Color c)
    {
        if (line != null)
        {
            line.startColor = c;
            line.endColor = c;
        }
    }

    void LateUpdate()
    {
        if (line.enabled && source != null && target != null)
        {
            line.SetPosition(0, source.position);
            line.SetPosition(1, target.position);
        }
    }
}
