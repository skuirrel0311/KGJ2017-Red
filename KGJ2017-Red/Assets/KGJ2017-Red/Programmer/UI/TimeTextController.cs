using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextController : MonoBehaviour 
{
	[SerializeField]
	Text timeText = null;

	//秒
	int second = 0;
	public int Second
	{
		get
		{
			return second;
		}
		set 
		{
			if (second == value) return;
			second = value;
			SetText ();
		}
	}

	void SetText()
	{
		float minute = Second / 60;

		timeText.text = minute.ToString("00") + " ： " + (Second - (minute * 60)).ToString("00");
	}
}
