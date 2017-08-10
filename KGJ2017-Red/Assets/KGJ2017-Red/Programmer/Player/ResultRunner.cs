using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultRunner : MonoBehaviour 
{
	[SerializeField]
	float speed = 4.0f;
	bool isStop = false;

	void Update()
	{
		if (isStop)
			return;
		transform.position += Vector3.forward * speed;
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("hit");
		if (col.gameObject.tag == "Goal") 
		{
			StartCoroutine (ResultManager.I.ShowResult ());
		}
	}
}
