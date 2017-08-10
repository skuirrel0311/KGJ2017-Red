using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

//プレイヤーの当たり判定はここで全て取りたいな
public class PlayerOverlap : MonoBehaviour 
{
	[SerializeField]
	BarGauge hpGauge = null;

	PlayerStateController stateController;

	[SerializeField]
	float invincibleTime = 1.0f;

	bool isInvicible = false;

	[SerializeField]
	Animator m_Animator = null;

	[SerializeField]
	PostProcessingProfile profile = null;

	public bool isHitMobu = false;

	void Start () {
		stateController = GetComponent<PlayerStateController> ();
	}

	public void Damage(int point)
	{
		if (isInvicible) return;

		m_Animator.SetTrigger ("Damaged");
		hpGauge.Value = hpGauge.Value - point;
		isInvicible = true;

		KKUtilities.Delay (invincibleTime, () => isInvicible = false, this);
		StartCoroutine (DamageEffect ());
		if (hpGauge.Value == 0) 
		{
			stateController.CurrentState = PlayerState.Dead;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Tumbleweed") return;

		//todo:当たった草の状態でダメージ量を変化させる
		ScoreManager.I.hitCount = ScoreManager.I.hitCount + 1;
		Damage(5);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Mobu") 
		{
			isHitMobu = true;
		}
		if (col.gameObject.tag == "Goal") {
			MainGameManager.I.GameOver (true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Mobu") 
		{
			isHitMobu = false;
		}
	}

	IEnumerator DamageEffect()
	{
		profile.vignette.enabled = true;

		yield return KKUtilities.FloatLerp (invincibleTime, (t) => {
			VignetteModel.Settings setting = profile.vignette.settings;
			setting.intensity = Mathf.Lerp(0.5f, 0.0f, t * t);
			profile.vignette.settings = setting;
		});

		yield return null;
		profile.vignette.enabled = false;
	}
}
