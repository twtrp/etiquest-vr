using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopMovement : MonoBehaviour
{
    public Vector3 MovementDirection { get; private set; }

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.position;
    }

    void Update()
    {
        MovementDirection = (transform.position - _lastPosition).normalized;
        _lastPosition = transform.position;
    }
}
