using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	float moveSpeed = 4.0f;
	[SerializeField]
	float rotateSpeed = 360.0f;
	[SerializeField]
	float jumpPower = 5.0f;
	//着地モーションの時間
	[SerializeField]
	float landingTime = 0.1f;

	Transform m_transform;
	Rigidbody m_body;
	Vector3 oldPosition;

	Vector2 leftStick;
	Vector3 movement = Vector3.zero;

	[SerializeField]
	float nearGround = 0.1f;

	Collider[] hits;

	PlayerStateController stateController;
	PlayerOverlap overlap;

	[SerializeField]
	Animator m_animator = null;

	[SerializeField]
	Vector3 gravityVec = new Vector3(0.0f, -0.98f,0.0f);

	Vector3 inerVec;

	void Start()
	{
		m_transform = transform;
		m_body = GetComponent<Rigidbody> ();
		oldPosition = m_transform.position;
		stateController = GetComponent<PlayerStateController> ();
		overlap = GetComponent<PlayerOverlap> ();
	}

	void FixedUpdate()
	{
		if (overlap.isHitMobu) movement.z = 0.0f;
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
		if (stateController.CurrentState == PlayerState.Dead) return;

		leftStick = MyInputManager.GetAxis (MyInputManager.Axis.LeftStick);
		movement.x = leftStick.x;
		//movement.z = leftStick.y;
		movement.y = 0.0f;

		//todo:z軸に勝手に移動？
		movement.z = 1.0f;

		movement *= moveSpeed * Time.deltaTime;

		//ジャンプ
		if (MyInputManager.GetButtonDown (MyInputManager.Button.A)) 
		{
			if (stateController.CurrentState == PlayerState.OnGround) {
				ActionJump ();
			}
		}

		switch (stateController.CurrentState) 
		{
		case PlayerState.Jump:
			movement *= 0.4f;
			inerVec *= 1.01f;
			m_body.velocity += inerVec * Time.deltaTime;
			if (m_transform.position.y - oldPosition.y < 0) 
			{
				stateController.CurrentState = PlayerState.Fall;
			}
			break;
		case PlayerState.Fall:
			movement *= 0.4f;
			inerVec *= 1.01f;
			m_body.velocity += inerVec * Time.deltaTime;
			if (IsNearGround ()) 
			{
				AkSoundEngine.PostEvent ("Footsteps", gameObject);

				m_animator.SetTrigger ("Landing");
				stateController.CurrentState = PlayerState.Land;
			}
			break;
		case PlayerState.Land:
			movement *= 0.8f;
			KKUtilities.Delay (landingTime, () => stateController.CurrentState = PlayerState.OnGround, this);
			break;
		}

		oldPosition = m_transform.position;
	}

	void ActionJump()
	{
		
		stateController.CurrentState = PlayerState.Jump;
		m_body.velocity = Vector3.up * jumpPower;
		m_animator.SetTrigger ("Jumping");
		inerVec = gravityVec;
		AkSoundEngine.PostEvent ("Jump", gameObject);
	}

	bool IsNearGround()
	{
		if (m_transform.position.y > nearGround)
			return false;

		return true;
	}
}
