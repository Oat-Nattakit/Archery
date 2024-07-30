using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireController : MonoBehaviour
{
	public static FireController m_instance;
	public GameObject projectile;
	public GameObject Hand;
	public Image PowerBar;
	public Text PowerText;
	public TextMesh AngleText;
	public Text LastPower;
	public Text LastAngle;
	public float speed;
	public TrajectoryMotor motor = new TrajectoryMotor();
	public GameObject target;
	public Transform muzzle;
	public bool canFire;
	public bool progressFire;
	public AudioClip shootsound;

	[HideInInspector]
	public float angleForText;
	[HideInInspector]
	public float powerForText;

	AudioSource audioPlay;

	private bool isDown;
	private Vector2 startPos;
	private Vector2 curPos;
	private float distance;
	float anglelast;
	float powerlast;

	// Awake this instance.
	void Awake()
	{
		motor.Init ();
		m_instance = this;
	}

	// Use this for initialization
	void Start()
	{
		motor.InitTrajectory();
		audioPlay = GetComponent<AudioSource> ();
		LastPower = GameObject.Find("LastPower").gameObject.GetComponent<Text> ();
		LastAngle = GameObject.Find("LastAngle").gameObject.GetComponent<Text> ();
		startPos = target.transform.position;
	}
	
	// Update is called once per frame
	void Update(){
		if (isDown) {
			canFire = true;

			Vector2 v = (curPos - startPos).normalized;
			float angle = Mathf.Atan2 (v.y, v.x);
			angle = (angle < 0f) ? (angle + 2f * Mathf.PI) * Mathf.Rad2Deg : angle * Mathf.Rad2Deg;

			angleForText = angle;
			target.transform.eulerAngles = new Vector3 (0f, 0f, angle + 180);


			//FORCE ANGLE 0°min TO 90°max
			if (angleForText < 180f) {
				target.transform.eulerAngles = new Vector3 (0f, 0f, 0f);
				//angleForText = 0f;
			} else if (angleForText > 270f) {
				target.transform.eulerAngles = new Vector3 (0f, 0f, 90f);
				//angleForText = 90f;
			} else if (angleForText < 270f && angleForText > 180f) {
				//angleForText = angle + 180;
				target.transform.eulerAngles = new Vector3 (0f, 0f, angle + 180);
			}

			float textangle = 0f;
			float angle2 = angleForText - 270;
			float angle3 = 90 - angle2;
			if (angleForText >= 180f && angleForText <= 270f) {
				textangle = angleForText - 180;
			} else if (angleForText > 270f && angleForText <= 360f) {
				textangle = 90;
			} else if (angleForText >= 0f && angleForText < 180f) {
				textangle = 0f;
			}

			motor.trajectory.Angle.GetComponentInChildren<Text> ().text = textangle.ToString ("00") + "°";

			curPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			distance = Vector2.Distance (this.transform.position, curPos);

			speed = distance * 3f;

			if (speed > 15f) {
				speed = 15f;
			}

			PowerText.enabled = true;
			powerForText = speed * 100f / 15f;
			PowerText.text = powerForText.ToString ("00");
			PowerBar.fillAmount = powerForText / 100f;

			anglelast = textangle;
			powerlast = powerForText;

			Vector2 screenPoint = Camera.main.WorldToScreenPoint (target.transform.position);
			float width = Vector2.Distance (screenPoint, Input.mousePosition);
			motor.UpdateTrajectory (muzzle.position, motor.GetDirection (target.transform.eulerAngles.z, speed * 1.5f));
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.instance.LevelManager.ShowOption ();
		}

	}

	// Fires the object.
	void FireObject(){
		Vector2 vec2 = motor.GetDirection (target.transform.eulerAngles.z, speed * 1.5f);
		GameObject projectileObj = Instantiate (projectile, muzzle.position, Quaternion.identity) as GameObject;
		projectileObj.GetComponent<Rigidbody2D>().AddForce (vec2 * motor.framePerSecond);
	}
	
	// Raises the mouse down event.
	void OnMouseDown(){
		if (!isDown && !progressFire && !GameManager.instance.LevelManager.GameEnd && !GameManager.instance.LevelManager.option) {
			isDown = true;
			motor.trajectory.trajectoryRoot.gameObject.SetActive (true);
		}
	}
	
	// Raises the mouse up event.
	void OnMouseUp(){
		if (isDown && !progressFire && !GameManager.instance.LevelManager.GameEnd && !GameManager.instance.LevelManager.option) {
			progressFire = true;
			isDown = false;
			motor.trajectory.trajectoryRoot.gameObject.SetActive (false);
			motor.trajectory.Angle.SetActive (false);
			audioPlay.PlayOneShot (shootsound);
			PowerBar.fillAmount = 0f;
			PowerText.text = "";
			LastPower.text = "Power : " + powerlast.ToString ("00");
			LastAngle.text = "Angle : " + anglelast.ToString ("00") + "°";			
			GameManager.instance.LevelManager.btnOption.GetComponent<Button>().interactable = false;
			GameManager.instance.LevelManager.UpdateCurrentArrow(-1);			
			if (canFire) {
				FireObject ();
				canFire = false;
			}
			curPos = Vector2.zero;
		}
	}
}
