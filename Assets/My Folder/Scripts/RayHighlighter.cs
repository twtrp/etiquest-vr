//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class RayHighlighter : MonoBehaviour
//{
//    LineRenderer _line;

//    void Awake()
//    {
//        _line = GetComponent<LineRenderer>();
//        _line.positionCount = 2;
//        _line.enabled = false;
//        _line.startWidth = _line.endWidth = 0.01f;
//    }

//    /// <summary>
//    /// Immediately draws a single frame ray from A -> B.
//    /// </summary>
//    public void DrawRay(Vector3 from, Vector3 to, Color color)
//    {
//        if (!_line) return;
//        _line.enabled = true;
//        _line.startColor = _line.endColor = color;
//        _line.SetPosition(0, from);
//        _line.SetPosition(1, to);
//    }

//    /// <summary>
//    /// Hides the ray.
//    /// </summary>
//    public void Hide()
//    {
//        if (_line) _line.enabled = false;
//    }
//}
