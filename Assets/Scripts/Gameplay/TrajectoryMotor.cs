using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TrajectoryMotor
{

	[System.Serializable]
	public class Trajectory
	{
		public Transform trajectoryRoot;
		public GameObject trajectoryObj;
		public GameObject Angle;
		public GameObject AngleObj;
		public int length;
	}

	[HideInInspector]
	public List<GameObject> trajectoryList = new List<GameObject> ();
	public Trajectory trajectory;
	[HideInInspector]
	public Vector2 gravity = new Vector2(0f, -10f);
	[HideInInspector]
	public int framePerSecond = 60;


	// Init this instance.
	public void Init()
	{
		Physics2D.gravity = gravity;
		Application.targetFrameRate = framePerSecond;
		Time.fixedDeltaTime = 1f / framePerSecond;
	}
	
	// Gets the direction.
	public Vector2 GetDirection(float degree, float speed)
	{
		float radian = degree * Mathf.Deg2Rad;

		Vector2 vec2 = Vector2.zero;

		vec2.x = Mathf.Cos (radian) * speed;
		vec2.y = Mathf.Sin (radian) * speed;

		return vec2;
	}
	
	// Inits the trajectory.
	public void InitTrajectory()
	{
		for (int i = 0; i < trajectory.length; i++)
		{
			GameObject obj = GameObject.Instantiate(trajectory.trajectoryObj) as GameObject;
			obj.transform.parent = trajectory.trajectoryRoot;
			trajectoryList.Add(obj);
		}
		trajectory.trajectoryRoot.gameObject.SetActive (false);
		trajectory.Angle = GameObject.Instantiate (trajectory.AngleObj) as GameObject;		
		trajectory.Angle.transform.parent = trajectory.trajectoryRoot;
	}
	
	// Updates the trajectory.
	public void UpdateTrajectory(Vector2 startPos, Vector2 dir)
	{
		trajectory.trajectoryRoot.gameObject.SetActive (true);
		for(int i = 0; i < trajectory.length; i++)
		{
			float t = i / 10f;
			int moitier = trajectoryList.Count / 2;

			trajectoryList[i].transform.gameObject.SetActive (true);
			trajectory.Angle.SetActive (true);
			trajectoryList[i].transform.position = new Vector3(startPos.x + dir.x * t, startPos.y + dir.y * t + (0.5f * (gravity.y) * Mathf.Pow(t, 2f)), 0f);
			trajectory.Angle.transform.position = new Vector3 (trajectoryList [moitier].transform.position.x, trajectoryList [moitier].transform.position.y + 0.65f, trajectoryList [moitier].transform.position.z);
		}
	}
	

}
