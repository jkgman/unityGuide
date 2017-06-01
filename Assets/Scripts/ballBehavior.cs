using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour {
	//[SerializeField] SteamVR_TrackedObject _trackedObj1;
	//[SerializeField] SteamVR_TrackedObject _trackedObj2;
  private float _timer = 1;
  private MeshRenderer _renderer;
  [SerializeField] float distance;
  [SerializeField] float spawnDist;
  [SerializeField] Camera _camera;
  [SerializeField] Transform _startTransform;
  [SerializeField] float _lerpMin;
  [SerializeField] float _lerpMax;
  [SerializeField] float _ballSpeed;
  Transform _endTransform;
  private bool _isShown;
  private bool _hasChosenDirection;
  private bool _isLeft;
  private MeshRenderer _meshRenderer;
  private OrbPositionManager _positionManager;
  //private ScaleLerper _scaleLerper;

  // Use this for initialization
  void Start ()
  {
    _meshRenderer = GetComponent<MeshRenderer>();

    //_scaleLerper = GetComponent<ScaleLerper> ();
    _positionManager = FindObjectOfType<OrbPositionManager>();
    int count;
    _renderer = GetComponent<MeshRenderer> ();
    Hide ();
  }

	/*public SteamVR_Controller.Device Controller1{
		get{
			return SteamVR_Controller.Input ((int)_trackedObj1.index);
		}
	}
	public SteamVR_Controller.Device Controller2{
		get{
			return SteamVR_Controller.Input ((int)_trackedObj2.index);
		}
	}*/
  // Update is called once per frame
  void Update () {
    _timer -= Time.deltaTime;

    if(_timer < 0 && !_isShown){
      Show ();
    }


    if (_isShown) {
      MoveTowardsTarget ();

      float dist = Mathf.Abs(Vector3.Distance (transform.position,_camera.transform.position));

      if (dist < distance) {
        transform.position = (transform.position + (transform.position - _camera.transform.position).normalized * (distance-dist));
        //Debug.Log (transform.position);

        if (!_hasChosenDirection) {

          if (transform.position.x < _endTransform.position.x) {
            _isLeft = true;
          } else {
            _isLeft = false;
          }
          _hasChosenDirection = true;
        }

        if (_isLeft) {
          transform.position = new Vector3 (transform.position.x + 2 * Time.deltaTime, transform.position.y, transform.position.z);
        } else {
          transform.position = new Vector3 (transform.position.x - 2 * Time.deltaTime, transform.position.y, transform.position.z);
        }
      }

      if (distance - dist > 1) {
        _hasChosenDirection=false;
      }

      Vector3 viewportPos = Camera.main.WorldToViewportPoint (transform.position);
      viewportPos.x = Mathf.Clamp (viewportPos.x,0.25f,0.75f);
      viewportPos.y = Mathf.Clamp (viewportPos.y,0.25f,0.75f);
      float radiusinview;
      radiusinview = Mathf.Sqrt(Mathf.Abs(viewportPos.x-.5f) *Mathf.Abs(viewportPos.x-.5f) + Mathf.Abs(viewportPos.y-.5f)*Mathf.Abs(viewportPos.y-.5f));
      /*
      if (viewportPos.x > 0.6f || viewportPos.y < 0.4f) {
      _scaleLerper.enabled = true;

      } else {
      */
      //_scaleLerper.enabled = false;
      transform.localScale = new Vector3 (Mathf.Lerp(_lerpMin, _lerpMax, radiusinview), Mathf.Lerp(_lerpMin, _lerpMax, radiusinview), Mathf.Lerp (_lerpMin, _lerpMax, radiusinview));
      transform.position = Camera.main.ViewportToWorldPoint (viewportPos);
      //}

      if (transform.position.y < -0.44f) {
        transform.position = new Vector3 (transform.position.x, -0.44f, transform.position.z);
      }
    }
		/*if (Controller1.GetPress (SteamVR_Controller.ButtonMask.Touchpad)||Controller2.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
        transform.position = _camera.transform.position + _camera.transform.forward * spawnDist;
    }*/
  }
  public void shownew(){
      transform.position = _camera.transform.position + _camera.transform.forward * spawnDist;
  }

  void Show(){
    _positionManager.GoToNextTarget();
    transform.position = _camera.transform.position + _camera.transform.forward * spawnDist;
    _renderer.enabled = true;
    _isShown = true;
    MoveTowardsTarget ();
  }

  void Hide(){
    _renderer.enabled = false;
    _isShown = false;
  }

  void MoveTowardsTarget(){
		transform.position = Vector3.MoveTowards (transform.position ,_endTransform.position + Vector3.up * 1f,Time.deltaTime * _ballSpeed);

    //if (Mathf.Abs(Vector3.Distance(_endTransform.position,transform.position)) < 1)
    //{
    //  _positionManager.GoToNextTarget();
    //}
  }

  public void SetNewTarget(Transform target)
  {
		_endTransform = target;
    _hasChosenDirection = false;
    StartCoroutine(disapear());
  }
  private IEnumerator disapear()
  {
      _meshRenderer.enabled = false;

      yield return new WaitForSeconds(5);
      shownew();
      _meshRenderer.enabled = true;

  }
}
