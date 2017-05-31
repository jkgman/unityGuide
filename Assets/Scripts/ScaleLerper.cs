using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerper : MonoBehaviour
{
    [SerializeField] private AnimationCurve _lerpCurve;

    [SerializeField] private float _lerpTime;


   [SerializeField] private float _maxScale;

   [SerializeField] private float _minScale;

    private float _lerpTimer;

    private bool _forward = true;

    private Transform _transform;

    private float _currentScaleValue;

    // Use this for initialization
    void Start()
    {
        _transform = transform;

    }

    // Update is called once per frame
    void Update()
    {
        float lerpStep = Mathf.Clamp01(_lerpCurve.Evaluate(
               1 - (_lerpTimer / _lerpTime)));

        _lerpTimer -= Time.deltaTime;


        if (_forward)
        {
           


            _currentScaleValue = Mathf.Lerp(_minScale, _maxScale, lerpStep);

            _transform.localScale = new Vector3(_currentScaleValue, _currentScaleValue, _currentScaleValue);

            if (_lerpTimer <= 0)
            {
                _forward = false;
                _lerpTimer = _lerpTime;
            }

        }
        else
        {

            _currentScaleValue = Mathf.Lerp(_maxScale, _minScale, lerpStep);

            _transform.localScale = new Vector3(_currentScaleValue, _currentScaleValue, _currentScaleValue);


            if (_lerpTimer <= 0)
            {
                _forward = true;
                _lerpTimer = _lerpTime;
            }

        }

    }
}