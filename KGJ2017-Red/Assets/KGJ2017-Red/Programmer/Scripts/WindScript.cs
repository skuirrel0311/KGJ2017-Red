using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour {

    [SerializeField]
    float coefficient;//空気抵抗
    [SerializeField]
    Vector3 velocity;//風速
    [SerializeField]
    Transform player;

    [SerializeField]
    float goalPosZ = 0;

    [SerializeField]
    WaitForSeconds wait = new WaitForSeconds(1.0f);

    float stageSizeZ = 0;

    List<Rigidbody> tempList = new List<Rigidbody>();

    [SerializeField]
    List<WindSetting> windSettings = new List<WindSetting>();

    //Action<WindSetting> OnWindChenged;

	// Use this for initialization
	void Start () {
        stageSizeZ = goalPosZ * 2;

        //OnWindChenged += (setting) =>
        //{

        //};
	}
	
	// Update is called once per frame
	void Update () {
        var relativeVelocity = Vector3.zero;

        WindDirectionChange();
        foreach (Rigidbody r in tempList)
        {
            //relativeVelocity = velocity - r.velocity;
            r.velocity += velocity * Time.deltaTime;
        }

        //if (OnWindChenged != null) OnWindChenged.Invoke(new WindSetting());
	}

    void WindDirectionChange()
    {
        foreach (WindSetting w in windSettings)
        {
            if ((1.0f - ((goalPosZ - player.position.z) / stageSizeZ)) * 100.0f < w.percent)
            {
                coefficient = w.coefficient;
                velocity = w.velocity;
                return;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Tumbleweed") return;
        tempList.Add(col.GetComponent<Rigidbody>());
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Tumbleweed") return;

        tempList.Remove(col.GetComponent<Rigidbody>());
    }
}

[System.Serializable]
public class WindSetting
{
    public float percent;
    public Vector3 velocity;
    public float coefficient;//空気抵抗
    public float wait;
}