using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour {
	private float _timer = 2;
	private MeshRenderer _renderer;
	// Use this for initialization
	void Start () {
		int count;
		_renderer = GetComponent<MeshRenderer> ();
		_renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		_timer -= Time.deltaTime;

		if(_timer < 0){
			_renderer.enabled = true;
		}

	}
}
