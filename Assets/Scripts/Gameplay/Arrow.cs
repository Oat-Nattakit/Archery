using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Arrow : MonoBehaviour
{

	public float speed;
	public Rigidbody2D rb;
	public AudioClip shootsound;
	public AudioSource audioPlay;
	bool hit = false;

	void FixedUpdate()
	{
		if (!hit)
		{
			Vector2 v = rb.velocity;
			float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	public void OnCollisionEnter2D(Collision2D col)
	{
		Physics2D.IgnoreLayerCollision(10, 11, true);		
		if (!hit)
		{
			hit = true;
			transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
			ArrowStick(col);
		}
	}

	void ArrowStick(Collision2D col)
	{
		if (col.transform.tag == "object")
		{
			transform.parent = col.transform;					
		}
		Destroy(this.rb);
		Destroy(this.GetComponent<CircleCollider2D>());
		Destroy(this.GetComponent<TrailRenderer>());
		Destroy(this.GetComponent<munition>());
		fallow.m_instance.StartReset();
		audioPlay.PlayOneShot(shootsound);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Physics2D.IgnoreLayerCollision(10, 11, true);
		if (col.transform.tag == "Zomb")
		{
			col.gameObject.GetComponent<Zombie>().Dead();
			GameManager.instance.LevelManager.IncreateHitEnemies(1);
		}
		if (col.transform.tag == "Shield")
		{
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			fallow.m_instance.StartReset();
		}
		if (col.transform.tag == "Ice")
		{
			triggerIceObject(col);
		}
	}

	private void triggerIceObject(Collider2D col)
	{
		var moveOut = col.gameObject.GetComponentInParent<MoveoutChild>();
		if (moveOut != null)
		{
			List<GameObject> child = new List<GameObject>();
			for (int i = 0; i < col.transform.childCount; i++)
			{
				child.Add(col.transform.GetChild(i).gameObject);
			}
			foreach (GameObject ch in child)
			{
				moveOut.moveChildToworld(ch.transform);
			}
			col.gameObject.SetActive(false);

		}
		else
		{
			Destroy(col.gameObject);
		}
	}
}
