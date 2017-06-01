using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    private ballBehavior _ballBehavior;
    private OrbPositionManager _orbPositionManager;
    private Collider _collider;

    private ParticleSystem _glowParticle;

    private RotateObject _rotateObject;

    // Use this for initialization
    void Awake ()
    {
        _ballBehavior = FindObjectOfType<ballBehavior>();
        _orbPositionManager = FindObjectOfType<OrbPositionManager>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
		_rotateObject = GetComponent<RotateObject>();
        _glowParticle = GetComponentInChildren<ParticleSystem>();
        _glowParticle.Stop();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
        _glowParticle.Play();
    }

    public void Activate() {
        Debug.Log ("Activated");

        _orbPositionManager.GoToNextTarget();
        _collider.enabled = false;
        _glowParticle.Stop();
        StartCoroutine(Rotate());

    }

    private IEnumerator Rotate()
    {
        _rotateObject.enabled = true;

        yield return new WaitForSeconds(2);

        _rotateObject.enabled = false;
    }
}
