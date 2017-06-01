using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    private ballBehavior _ballBehavior;
    private OrbPositionManager _orbPositionManager;


    // Use this for initialization
    void Awake ()
    {
        _ballBehavior = FindObjectOfType<ballBehavior>();
        _orbPositionManager = FindObjectOfType<OrbPositionManager>();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void Activate() {
        Debug.Log ("Activated");

        _orbPositionManager.GoToNextTarget();
    }
}
