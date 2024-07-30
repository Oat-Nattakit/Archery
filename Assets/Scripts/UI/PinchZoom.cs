using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinchZoom : MonoBehaviour {

	//if the camera is in orthographic view
	public float orthographicZoomSensitivity;
	public float MaxZoom = 5f;
	public float MinZoom = 15f;

	private Camera cam;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	bool Drag = false;
	Vector2 velocity;
	Vector3 Origin;
	Vector3 Diference;

	void Start(){		
		cam = Camera.main;
	}

	void Update() 
	{
		if (!FireController.m_instance.canFire && !FireController.m_instance.progressFire && !GameManager.instance.LevelManager.GameEnd && !GameManager.instance.LevelManager.option) {
			if (Input.touchCount == 2) {
				Touch touch0 = Input.GetTouch (0);
				Touch touch1 = Input.GetTouch (1);

				Vector2 touch0_prevPos = touch0.position - touch0.deltaPosition;
				Vector2 touch1_prevPos = touch1.position - touch1.deltaPosition;

				float prev_TouchDeltaMag = (touch0_prevPos - touch1_prevPos).magnitude;
				float current_TouchDeltaMag = (touch0.position - touch1.position).magnitude;

				float deltaMagDiff = prev_TouchDeltaMag - current_TouchDeltaMag;

				//if the camera is orthographic view
				cam.orthographicSize += deltaMagDiff * orthographicZoomSensitivity;
				//if the camera is in orthographic view
				cam.orthographicSize = Mathf.Max (cam.orthographicSize, 0.1f);

			} else {
				if (Input.GetMouseButton (0)) {
					Diference = (GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition)) - GetComponent<Camera> ().transform.position;
					if (Drag == false) {
						Drag = true;
						Origin = GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
					}
				} else {
					Drag = false;
				}
				if (Drag == true) {
					Vector3 tmp = Origin - Diference;
					GetComponent<Camera> ().transform.position = new Vector3 (tmp.x, 0f, -10f);
					if (transform.position.x > maxCameraPos.x) {
						transform.position = new Vector3 (maxCameraPos.x, 0f, -10f);
					} else if (transform.position.x < minCameraPos.x) {
						transform.position = new Vector3 (0f, 0f, -10f);
					}
				}
			}

			if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
			{
				cam.orthographicSize++;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
			{
				cam.orthographicSize--;
			}

			if (cam.orthographicSize > MinZoom) {
				cam.orthographicSize = MinZoom;
			} else if (cam.orthographicSize < MaxZoom) {
				cam.orthographicSize = MaxZoom;
			}
		}
	}
}