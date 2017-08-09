using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : BaseManager<ResultManager> 
{
	[SerializeField]
	Text clearTime = null;
	[SerializeField]
	Text hitCount = null;
	[SerializeField]
	string nextSceneName = "Title";
	bool isTransition = false;

	protected override void Start ()
	{
		base.Start ();
		ScoreManager score = ScoreManager.I;

		clearTime.text = score.clearTime.ToString();
		hitCount.text = score.hitCont.ToString ();
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
