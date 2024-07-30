using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public GameObject splash;
	public GameObject skull;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Dead(){
		Instantiate (skull, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), Quaternion.identity);
		Instantiate (splash, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		Physics2D.IgnoreLayerCollision(10,11, true);		
		if (col.transform.tag == "Sprike") {			
			Destroy(gameObject);	
			Dead();
			GameManager.instance.LevelManager.IncreateHitEnemies(1);				
		}
	}
}
