using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfter : MonoBehaviour {

	public float time;

	void Start () {
		StartCoroutine (explose ());
	}

	void OnEnable () {
		StartCoroutine (explose ());
	}
	
	IEnumerator explose(){
		yield return new WaitForSeconds (time);
		Destroy(this.gameObject);
	}
}
