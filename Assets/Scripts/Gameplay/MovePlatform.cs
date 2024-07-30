using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
	public GameObject platform;
	public float moveSpeed;
	[SerializeField] private float waitingTime = 0;
	public Transform currentPoint;
	public Transform[] points;
	public int pointSelection;

	private bool isWaiting = true;
	private float counttime = 0;
	[SerializeField] private bool randomPath = false;

	// Use this for initialization
	void Start()
	{
		currentPoint = points[pointSelection];
		isWaiting = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isWaiting)
		{
			counttime += Time.deltaTime;
			if (counttime >= waitingTime)
			{

				isWaiting = true;
				counttime = 0;
			}
		}
		else
		{
			platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
			if (platform.transform.position == currentPoint.position)
			{
				if (!randomPath)
				{
					pointSelection++;
					if (pointSelection == points.Length)
					{
						pointSelection = 0;
					}
				}else{
					pointSelection = Random.Range(0,points.Length);
				}

				currentPoint = points[pointSelection];
				isWaiting = false;
			}
		}
	}
}
