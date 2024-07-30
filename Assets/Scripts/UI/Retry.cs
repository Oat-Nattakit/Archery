using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

	string targetScene;

	// Use this for initialization
	void Start () {		
		targetScene = GameManager.instance.CurrentScene;
	}
	
	// Update is called once per frame
	public void RetryLevel () {
		//SceneManager.LoadScene (level, LoadSceneMode.Single);		
		GameManager.instance.LoadSceAsync(targetScene);
	}
}
