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

	[SerializeField]
	GameObject exitMessage = null;

	bool isTransition = false;

	[SerializeField]
	Animator[] animators = null;

	bool showResult = false;

	protected override void Start ()
	{
		base.Start ();
		ScoreManager score = ScoreManager.I;

		clearTime.Second = (int)score.clearTime;
		hitCount.text = score.hitCount.ToString ();
	}

	protected override void OnDestroy()
	{
		ScoreManager.I.ResetData ();
		base.OnDestroy ();
	}

	void Update()
	{
		if (!showResult) return;
		if (MyInputManager.GetButtonDown (MyInputManager.Button.Start)) 
		{
			if (isTransition) return;
			isTransition = true;
			LoadSceneManager.I.LoadScene(nextSceneName, true, 1.0f, 0.3f);
		}
	}


	public IEnumerator ShowResult()
	{
		for (int i = 0; i < animators.Length; i++) 
		{
			animators [i].SetTrigger ("IsClose");
		}
		yield return new WaitForSeconds (2.0f);

		clearTime.transform.parent.gameObject.SetActive(true);
		yield return new WaitForSeconds (1.0f);

		hitCount.transform.parent.gameObject.SetActive(true);
		yield return new WaitForSeconds (1.0f);

		exitMessage.SetActive (true);
		showResult = true;

	}
}
