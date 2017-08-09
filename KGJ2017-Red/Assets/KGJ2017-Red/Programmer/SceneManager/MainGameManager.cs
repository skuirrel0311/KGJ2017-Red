using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : BaseManager<MainGameManager> 
{
	[SerializeField]
	string nextSceneName = "Result";
	bool isTransition = false;

	[SerializeField]
	TimeTextController timeText = null;
	float totalTime = 0.0f;

	void Update()
	{
		totalTime += Time.deltaTime;
		timeText.Second = (int)totalTime;

		//リザルトを完成させるまでは残しておく
		if (MyInputManager.GetButtonDown (MyInputManager.Button.Start)) 
		{
			GameOver (false);
		}
	}


	public void GameOver(bool gameClear)
	{
		if (isTransition) return;

		//todo:true or false で分岐？
		isTransition = true;
		LoadSceneManager.I.LoadScene(nextSceneName, true, 1.0f, 0.3f);
	}
}
