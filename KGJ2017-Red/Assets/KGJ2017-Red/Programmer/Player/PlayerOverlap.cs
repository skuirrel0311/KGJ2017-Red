using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの当たり判定はここで全て取りたいな
public class PlayerOverlap : MonoBehaviour 
{
	[SerializeField]
	BarGauge hpGauge = null;

	PlayerStateController stateController;

	[SerializeField]
	float invincibleTime = 1.0f;

	void Start () {
		stateController = GetComponent<PlayerStateController> ();
	}

	public void Damage(int point)
	{
		if (stateController.CurrentState == PlayerState.invincible) return;

		hpGauge.Value = hpGauge.Value - point;

		if (hpGauge.Value == 0) 
		{
			stateController.CurrentState = PlayerState.Dead;
		} else 
		{
			stateController.CurrentState = PlayerState.invincible;
			KKUtilities.Delay (invincibleTime, () => {
				stateController.CurrentState = PlayerState.Idle;
			}, this);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Tumbleweed") return;

		Debug.Log ("hit tumbleweed");
		//todo:当たった草の状態でダメージ量を変化させる
		Damage(5);
	}
}
