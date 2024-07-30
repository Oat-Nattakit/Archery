using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour {

	GameObject musicObj;

	// Use this for initialization
	void Start () {
		musicObj = GameObject.Find ("Music");
		if (musicObj != null) {
			Destroy (musicObj);
		}
	}

}
