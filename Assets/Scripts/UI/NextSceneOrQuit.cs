using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneOrQuit : MonoBehaviour {

	public string NextsceneName;

	public void NextLevel(){		
		GameManager.instance.LoadSceAsync(NextsceneName);
	}

	public void QuitLevel(){		
		GameManager.instance.LoadScene(SceneName.MainLevel);
	}

	public void Back(){
		GameManager.instance.LevelManager.ShowOption ();
	}

}
