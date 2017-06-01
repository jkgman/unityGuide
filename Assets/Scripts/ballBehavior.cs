using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour {
    private float _timer = 1;
    private MeshRenderer _renderer;
    [SerializeField] float distance;
    [SerializeField] float spawnDist;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _startTransform;
    Transform _endTransform;
    private bool _isShown;
    private bool _hasChosenDirection;
    private bool _isLeft;
    private OrbPositionManager _positionManager;

    // Use this for initialization
    void Start ()
    {
        _positionManager = FindObjectOfType<OrbPositionManager>();
        int count;
        _renderer = GetComponent<MeshRenderer> ();
        Hide ();
    }

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
            transform.position = Camera.main.ViewportToWorldPoint (viewportPos);



        }
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
        transform.position = Vector3.MoveTowards (transform.position,_endTransform.position,Time.deltaTime * 5);

        if (Mathf.Abs(Vector3.Distance(_endTransform.position,transform.position)) < 1)
        {
            _positionManager.GoToNextTarget();
        }
    }

    public void SetNewTarget(Transform target)
    {
        _endTransform = target;
        _hasChosenDirection = false;

    }
}
