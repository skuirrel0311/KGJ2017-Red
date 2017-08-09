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

	bool isInvicible = false;

	void Start () {
		stateController = GetComponent<PlayerStateController> ();
	}

	public void Damage(int point)
	{
		if (isInvicible) return;

		hpGauge.Value = hpGauge.Value - point;
		isInvicible = true;

		KKUtilities.Delay (invincibleTime, () => isInvicible = false, this);

		if (hpGauge.Value == 0) 
		{
			stateController.CurrentState = PlayerState.Dead;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Tumbleweed") return;

		//todo:当たった草の状態でダメージ量を変化させる
		Damage(5);
	}
}
