using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	[SerializeField]
	Transform target = null;

	Vector3 targetOffset;

	Transform m_transform;

	void Start()
	{
		m_transform = transform;
		targetOffset = m_transform.position - target.position;
	}

	void Update()
	{
		m_transform.position = target.position + targetOffset;
	}
}
