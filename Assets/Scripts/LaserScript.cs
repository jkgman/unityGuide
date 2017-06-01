using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
	private SteamVR_TrackedObject _trackedObj;

	public GameObject laserPrefab;

	private GameObject _laser;
	private GameObject isTriggerPressed;
	private Transform _laserTransform;

	private Vector3 _hitPoint;

	private SteamVR_Controller.Device Controller{
		get{
			return SteamVR_Controller.Input ((int)_trackedObj.index);
		}
	}

	void Awake() {
		_trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Start() {
		_laser = Instantiate (laserPrefab);
		_laserTransform = _laser.transform;
	}

	private void ShowLaser(RaycastHit hit) {

		_laser.SetActive (true);

		_laserTransform.position = Vector3.Lerp (_trackedObj.transform.position, _hitPoint,0.5f);
		_laserTransform.LookAt(_hitPoint);
		Debug.Log(_hitPoint);
		_laserTransform.localScale = new Vector3 (_laserTransform.localScale.x, _laserTransform.localScale.y, hit.distance);

	}


	private void ShowLaser(Vector3 position) {

		_laser.SetActive (true);

		_laserTransform.position = Vector3.Lerp (_trackedObj.transform.position,position,0.5f);
		_laserTransform.LookAt(position);

		_laserTransform.localScale = new Vector3 (_laserTransform.localScale.x, _laserTransform.localScale.y, Vector3.Distance(_trackedObj.transform.position,position));

	}


	// Update is called once per frame
	void FixedUpdate () {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {

			RaycastHit hit;
			if(Physics.Raycast (_trackedObj.transform.position, transform.forward, out hit, 100)) {
				//hitpoint laser is causing the flicker
				_hitPoint = hit.point;
				ShowLaser (hit);
				//ShowLaser (_trackedObj.transform.position + 100 * transform.forward);
			} else {

				ShowLaser (_trackedObj.transform.position + 100 * transform.forward);
			}

		} else {
			_laser.SetActive (false);
		}
	}
}
