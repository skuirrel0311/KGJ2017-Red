using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobuController : MonoBehaviour 
{
	Animator m_animator;

	bool isDamage = false;
	float damageTime = 1.8f;

	[SerializeField]
	GameObject[] effects = null;

	void Start()
	{
		m_animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (isDamage) return;
		if (col.gameObject.tag != "Tumbleweed") return;
		Debug.Log ("hit tumbleweed");
		TumbleweedScript t = col.gameObject.GetComponent<TumbleweedScript> ();
		if(t.tumbleweedType == TumbleweedScript.Type.Normal) return;

		isDamage = true;
		m_animator.SetTrigger ("Damaged");
		if(effects != null)
			for(int i = 0;i < effects.Length;i++)
				effects[i].SetActive(true);

		KKUtilities.Delay (damageTime, () =>{
			if(effects != null)
				for(int i = 0;i < effects.Length;i++)
					effects[i].SetActive(false);
			isDamage = false;
		}, this);
	}
}
