using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioSource audioPlay;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable(){
		audioPlay.Play();
	}
}
