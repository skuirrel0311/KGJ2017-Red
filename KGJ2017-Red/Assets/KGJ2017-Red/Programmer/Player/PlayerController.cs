using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	float moveSpeed = 4.0f;
	[SerializeField]
	float rotateSpeed = 360.0f;

	Transform m_transform;

	Vector2 leftStick;
	Vector3 movement = Vector3.zero;

	void Start()
	{
		m_transform = transform;
	}

	void FixedUpdate()
	{
		Vector3 forward = Vector3.Slerp(
			transform.forward,
			movement, 
			rotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, movement));

		m_transform.LookAt(transform.position + forward);

		//移動
		m_transform.Translate(movement, Space.World);
	}

	void Update()
	{
		leftStick = MyInputManager.GetAxis (MyInputManager.Axis.LeftStick);
		movement.x = leftStick.x;
		movement.z = leftStick.y;

		movement *= moveSpeed * Time.deltaTime;
	}

}
