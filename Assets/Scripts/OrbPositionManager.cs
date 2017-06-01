using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPositionManager : MonoBehaviour
{


    [SerializeField] private List<Clickable> _buttons;

    [SerializeField] private ballBehavior _ballBehavior;

    private int _currentTargetIndex = 0;

    // Use this for initialization
    void Awake ()
    {
        _ballBehavior = GetComponent<ballBehavior>();
    }
    
    // Update is called once per frame
    void Update () {
        
    }




    public void GoToNextTarget()
    {
        if (_currentTargetIndex >= _buttons.Count)
        {
            _currentTargetIndex = 0;

        }

        Transform result = _buttons[_currentTargetIndex].transform;

        _buttons[_currentTargetIndex].EnableCollider();

        _currentTargetIndex++;

        _ballBehavior.SetNewTarget(result);


    }
}
