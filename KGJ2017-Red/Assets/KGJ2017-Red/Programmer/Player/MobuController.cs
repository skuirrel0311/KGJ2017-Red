using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobuController : MonoBehaviour 
{
	Animator m_animator;

	bool isDamage = false;
	float damageTime = 1.5f;

	void Start()
	{
		m_animator = GetComponent<Animator> ();
	}

	void OnCollisonEnter(Collision col)
	{
		if (isDamage) return;
		if (col.gameObject.tag != "Tumbleweed") return;

		//todo:燃えていたら悶える
		isDamage = true;

		KKUtilities.Delay (damageTime, () => isDamage = false, this);
	}
}
