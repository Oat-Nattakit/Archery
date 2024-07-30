using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMusic : MonoBehaviour {

	public GameObject PrefabMusic;
	GameObject musicObj;

	// Use this for initialization
	void Start () {
		musicObj = GameObject.Find ("Music");
		if (musicObj == null) {
			GameObject music = Instantiate (PrefabMusic, new Vector3 (0f, 0f, 0f), Quaternion.identity);
			music.name = "Music";
		}
	}
	

}
