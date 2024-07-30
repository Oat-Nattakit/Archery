using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public Transform MapParent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D col) {
		if (col.contacts.Length > 0) {
			col.transform.SetParent (this.transform);
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		col.transform.SetParent (MapParent);
	}
}
