using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	OnGround,//走っているか止まっているか

	Jump,	//ジャンプ
	Fall,   //落下
	Land,	//着地
	Dead	//死亡
}

public class PlayerStateController : MonoBehaviour 
{
	public Action<PlayerState, PlayerState> OnStateChanged;

	[SerializeField]
	PlayerState state;
	public PlayerState CurrentState{
		get{
			return state;
		}
		set{
			if (state == value) return;
			if(OnStateChanged != null) OnStateChanged.Invoke (state,value);
			state = value;
		}
	}

	void Start()
	{
		OnStateChanged += StateChanged;
	}

	void StateChanged(PlayerState oldState, PlayerState state)
	{
		if (state == PlayerState.Dead) {
			MainGameManager.I.GameOver (false);
		}
	}
}
