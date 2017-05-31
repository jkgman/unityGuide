using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    protected enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField] protected RotationAxis RotationDirection;
    [SerializeField] protected float RotationSpeed = 10f;
    protected Transform _transform;

    void Start()
    {
        _transform = transform;
    }

    protected virtual void Update()
    {
        switch (RotationDirection)
        {
            case RotationAxis.X:
                _transform.Rotate(new Vector3(Time.deltaTime * RotationSpeed, 0, 0));
                break;
            case RotationAxis.Y:
                _transform.Rotate(new Vector3(0, Time.deltaTime * RotationSpeed, 0));

                break;
            case RotationAxis.Z:
                _transform.Rotate(new Vector3(0, 0, Time.deltaTime * RotationSpeed));

                break;
        }
    }
}