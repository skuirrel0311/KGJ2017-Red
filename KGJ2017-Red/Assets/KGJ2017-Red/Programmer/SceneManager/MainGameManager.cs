using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : BaseManager<MainGameManager> 
{
	[SerializeField]
	string nextSceneName = "Result";
	bool isTransition = false;

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
