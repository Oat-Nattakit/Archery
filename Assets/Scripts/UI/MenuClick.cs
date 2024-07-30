using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClick : MonoBehaviour {

	public void Playgame(){		
		GameManager.instance.LoadScene(SceneName.MainLevel);
	}

	public void Exitgame(){
		Application.Quit ();
	}
}
