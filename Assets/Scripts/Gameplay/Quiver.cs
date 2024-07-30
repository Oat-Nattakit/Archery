using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Quiver : MonoBehaviour
{
	public GameObject ArrowPrefabs;
	public Sprite ArrowRef;
	public GameObject Parent = null;
	public Text MunitionText;

	private List<GameObject> arrowList = new List<GameObject>();

	[SerializeField] private Vector2 RangeSpawn;

	public void Init(int MaxArrow)
	{
		createArrow(MaxArrow);
	}
	public void UpdateCurrentArrowText(int value)
	{
		this.MunitionText.text = value.ToString();
	}

	private void createArrow(int MaxArrow)
	{
		float startPOs = RangeSpawn.x;
		for (int i = 0; i < MaxArrow; i++)
		{
			GameObject arrow = GameObject.Instantiate<GameObject>(ArrowPrefabs, Parent.transform, false);

			var localPos = new Vector3(startPOs, 90);
			arrow.transform.localPosition = localPos;
			arrowList.Add(arrow);
			if (arrowList.Count > 0)
			{
				float startValue = arrowList[arrowList.Count - 1].transform.localPosition.x;
				startPOs = Random.Range((startValue +9.0f), (startValue + 1.0f));
				if (startPOs > RangeSpawn.y)
				{
					startPOs = RangeSpawn.x;
				}
			}


			/*
			
			float arrowRot = Random.Range(-10,11);
			var localRot = Quaternion.Euler(0, 0, arrowRot);
			arrow.transform.SetLocalPositionAndRotation(localPos, localRot);
			arrowList.Add(arrow);*/
		}
	}

	public void DecreaseArrow()
	{
		GameObject lastArrow = arrowList[arrowList.Count - 1];
		arrowList.Remove(lastArrow);
		Destroy(lastArrow);
	}
}