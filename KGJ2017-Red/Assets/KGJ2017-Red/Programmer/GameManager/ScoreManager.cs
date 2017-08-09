using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : BaseManager<ScoreManager>
{
	[System.NonSerialized]
	public float clearTime = 0.0f;
	[System.NonSerialized]
	public int hitCont = 0;
}
