using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : BaseManager<ResultManager> 
{
	[SerializeField]
	TimeTextController clearTime = null;
	[SerializeField]
	Text hitCount = null;
	[SerializeField]
	string nextSceneName = "Title";
	bool isTransition = false;

	protected override void Start ()
	{
		base.Start ();
		ScoreManager score = ScoreManager.I;

		clearTime.Second = (int)score.clearTime;
		hitCount.text = score.hitCount.ToString ();
	}

	void OnDestroy()
	{
		
	}

	void Update()
	{
		if (MyInputManager.GetButtonDown (MyInputManager.Button.Start)) 
		{
			if (isTransition) return;
			isTransition = true;
			LoadSceneManager.I.LoadScene(nextSceneName, true, 1.0f, 0.3f);
		}
	}
}
