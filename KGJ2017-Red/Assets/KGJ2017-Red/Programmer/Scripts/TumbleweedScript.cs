using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedScript : MonoBehaviour {
	public enum Type{Normal, Fire }
	public Type tumbleweedType;
    public int pow = -1000;
    public Rigidbody m_rigidbody { get; private set; }

	// Use this for initialization
	void Awake () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        m_rigidbody.AddForce(0, 0, pow, ForceMode.Force);
	}
}
