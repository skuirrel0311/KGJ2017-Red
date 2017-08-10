using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour {

    [SerializeField]
    Transform player=null;

    [SerializeField]
    Transform goal = null;

    [SerializeField]
    Transform wind = null;

    [SerializeField]
    WaitForSeconds wait = new WaitForSeconds(1.0f);


    float stageSizeZ = 0;

    List<Rigidbody> tempList = new List<Rigidbody>();

    [SerializeField]
    List<WindSetting> windSettings = new List<WindSetting>();

    Action<WindSetting> OnWindChenged;
    [SerializeField]
    WindSetting windSetting;
    public int CurrentWindSettingNumber
    {
        get
        {
            return windSetting.number;
        }
		set{
			if (windSetting.number == value) return;
            if (OnWindChenged != null) OnWindChenged.Invoke(windSettings[value]);
            windSetting.number = value;
        }
    }
    public float CurrentWindSettingWait
    {
        get
        {
            return windSetting.wait;
        }
    }
    public int CurrentWindSettingEmissionRate
    {
        get
        {
            return windSetting.emissionRate;
        }
    }


	// Use this for initialization
	void Start () {
        stageSizeZ = goal.position.z * 2;
        windSetting = windSettings[0];

        OnWindChenged += (setting) =>
        {
            windSetting = setting;
            if (windSetting.velocity.x > 0)
            {
                wind.transform.Rotate(new Vector3(0, -120, 0));
            }
            else
            {
                wind.transform.Rotate(new Vector3(0, 120, 0));
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
        var relativeVelocity = Vector3.zero;

        WindDirectionChange();
        foreach (Rigidbody r in tempList)
        {
            //relativeVelocity = velocity - r.velocity;
            r.velocity +=windSetting.velocity * Time.deltaTime;
        }
	}

    void WindDirectionChange()
    {
        for(int i=0;i<windSettings.Count;i++)
        {
            if ((1.0f - ((goal.position.z - player.position.z) / stageSizeZ)) * 100.0f < windSettings[i].percent)
            {
                CurrentWindSettingNumber = i;
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
    public int number;
    public int emissionRate;
}