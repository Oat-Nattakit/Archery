using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeStart : MonoBehaviour {

	public Text textTime;
	public GameObject LastShoot;
	public float temps = 6f;
	public bool TimeInprogress = true;
	public GameObject Hand;

	// Update is called once per frame
	void Update () {
		if (TimeInprogress) {
			temps -= Time.deltaTime;
			if (temps > 1.5f) {
				textTime.text = (temps - 1f).ToString ("0");
			} else if (temps <= 1.5f) {
				textTime.text = "GO!";
			}
			if (temps <= 1f) {
				TimeInprogress = false;
				StartCoroutine ("EndTime");
			}
		}
	}

	IEnumerator EndTime(){
		yield return new WaitForSeconds (0.5f);
		this.gameObject.SetActive (false);
		GameManager.instance.LevelManager.GameEnd = false;
		GameManager.instance.LevelManager.btnOption.GetComponent<Button>().interactable = true;
		LastShoot.GetComponent<Text> ().enabled = true;
		Hand.SetActive (true);
		StopCoroutine("EndTime");
	}
}
