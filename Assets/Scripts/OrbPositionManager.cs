using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPositionManager : MonoBehaviour
{


    [SerializeField] private List<Clickable> _buttons;

    [SerializeField] private ballBehavior _ballBehavior;

    private int _currentTargetIndex = 0;

	bool first = true;
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

		if (first) {
			_buttons[_currentTargetIndex].EnableCollider();
			first = false;

		} else {
			StartCoroutine (GlowStar (_currentTargetIndex));

		}

        int prev = _currentTargetIndex;
        _currentTargetIndex = Random.Range(0, _buttons.Count);
        if (_currentTargetIndex==prev) {
          _currentTargetIndex++;
          if (_currentTargetIndex >= _buttons.Count) {
            _currentTargetIndex -= 2;
          }
        }

        _ballBehavior.SetNewTarget(result);


    }


	private IEnumerator GlowStar(int index)
	{
		


		yield return new WaitForSeconds(5);

		_buttons[index].EnableCollider();
	}
}
