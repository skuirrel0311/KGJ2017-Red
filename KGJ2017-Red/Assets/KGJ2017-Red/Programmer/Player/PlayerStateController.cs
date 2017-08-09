using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Idle,	//アイドル
	Run,	//走り
	Jump,	//ジャンプ
	Fall,	//落下
	Land,	//着地
	Damage,	//ダメージ
	invincible, //無敵
	Dead	//死亡
}

public class PlayerStateController : MonoBehaviour 
{
	public Action<PlayerState, PlayerState> OnStateChanged;

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
