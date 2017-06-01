using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
	private SteamVR_TrackedObject _trackedObj;

	public GameObject laserPrefab;

	private GameObject _laser;
	private GameObject isTriggerPressed;
	private Transform _laserTransform;

	public Vector3 _hitPoint;

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
	void Update () {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {

			RaycastHit[] hits;

			hits = Physics.RaycastAll (transform.position, transform.forward, 100);


			if (hits.Length > 0) {



				foreach(RaycastHit hit in hits) {
					if (hit.transform.CompareTag ("Button")) {
					
						hit.transform.GetComponent<Clickable> ().Activate();
					}

				}

				//hitpoint laser is causing the flicker
				_hitPoint = hits [hits.Length - 1].point;
				ShowLaser (_hitPoint);
				//ShowLaser (_trackedObj.transform.position + 100 * transform.forward);
			} else {

				//ShowLaser (_trackedObj.transform.position + 100 * transform.forward);
			}

		} else {
			_laser.SetActive (false);
		}
	}
}
