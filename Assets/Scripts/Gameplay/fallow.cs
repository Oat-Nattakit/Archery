using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fallow : MonoBehaviour {

	public static fallow m_instance;
	public munition objects;
	Vector3 defaultpos = new Vector3 (0f, 0f, -10f);

	//private bool fais = false;

	void Awake(){
		m_instance = this;
	}

	// Update is called once per frame
	void Update () {
		objects = GameObject.FindObjectOfType<munition> ();
		if (objects != null) {
			if (objects.transform.position.x >= 0f) {
				this.transform.position = new Vector3 (objects.transform.position.x, this.transform.position.y, this.transform.position.z);
			}
		}
	}

	public void StartReset(){
		StartCoroutine (resetpos ());
	}

	IEnumerator resetpos(){
		yield return new WaitForSeconds (2f);
		GameManager.instance.LevelManager.verify ();
		//fais = false;
		this.transform.position = defaultpos;
		FireController.m_instance.progressFire = false;
		GameManager.instance.LevelManager.btnOption.GetComponent<Button>().interactable = true;
	}
}
