using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour {

	public Text textLevel;
	public Image imgLock;
	public Image rating;
	public Button button;
	public string sceneName;		

	public void InitButton(bool isActive = false){
		setButtonInteract(isActive);
		OnClicklvl();
	}
	private void setButtonInteract(bool isActive){
		button.interactable = isActive;
		textLevel.enabled = isActive;
		rating.enabled = isActive;
		imgLock.enabled = !isActive;
	}	

	public void OnClicklvl(){	
		button.onClick.RemoveAllListeners();	
		button.onClick.AddListener(()=>{
			GameManager.instance.LoadSceAsync(sceneName);
		});
	}

	public void Exit(){
		Time.timeScale = 1f;		
		GameManager.instance.LoadScene(SceneName.MainLevel);
	}

	public void Back(){
		Time.timeScale = 1f;		
		GameManager.instance.LoadScene(SceneName.Index);
	}	
}
