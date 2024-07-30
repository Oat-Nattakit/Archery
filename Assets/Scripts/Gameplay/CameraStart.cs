using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CameraStart : MonoBehaviour
{	
	public float duration = 3;

	async void Start()
	{	
		await MoveCamera();
	}
	
	private async UniTask MoveCamera(){
		await UniTask.WaitForSeconds(1);
		Vector3 target = new Vector3(0f, 0f, -10f);
		transform.DOMove(target,3);
	}	
}



