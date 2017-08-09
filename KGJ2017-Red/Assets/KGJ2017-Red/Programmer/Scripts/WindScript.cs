using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour {

    public float coefficient;//空気抵抗
    public float velocity;//風速

    List<Rigidbody> tempList = new List<Rigidbody>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Rigidbody r in tempList)
        {
            //r.AddForce()
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "") return;
    }
}
