using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTumbleweedScript : MonoBehaviour {

    [SerializeField]
    GameObject tumbleweedPrefab = null;
    [SerializeField]
    GameObject fireTumbleweedPrefab = null;
    [SerializeField]
    BoxCollider m_col=null;

    float width=0;

    [SerializeField]
    float stageMinZ = 0;
    const int MaxTumbleweed = 200;

    List<TumbleweedScript> list_Tumbleweeds = new List<TumbleweedScript>();

    [SerializeField]
    WaitForSeconds wait;

    float time = 0;

    Vector3 instancePosition;

    [SerializeField]
    WindScript windScript=null;
    [SerializeField]
    int rand = 0;

    //キャッシュ
    Vector3 zeroVec = Vector3.zero;

	// Use this for initialization
	void Start () {
        m_col=GetComponent<BoxCollider>();

        width = m_col.size.x * transform.localScale.x;

        for (int i = 0; i < MaxTumbleweed; i++)
        {
            TumbleweedScript tumbleweed;
            if (i < MaxTumbleweed / 2)
            {
                tumbleweed = ((GameObject)Instantiate(tumbleweedPrefab)).GetComponent<TumbleweedScript>();
            }
            else
            {
                tumbleweed = ((GameObject)Instantiate(fireTumbleweedPrefab)).GetComponent<TumbleweedScript>();
				tumbleweed.tumbleweedType = TumbleweedScript.Type.Fire;
            }
            tumbleweed.transform.parent = this.transform;
            tumbleweed.gameObject.SetActive(false);
            list_Tumbleweeds.Add(tumbleweed);
        }

        instancePosition = new Vector3(0, 0, transform.position.z);
        StartCoroutine(CreateTumbleweed());

        time = windScript.CurrentWindSettingWait;
	}

    IEnumerator CreateTumbleweed()
    {
        WaitForSeconds wait = new WaitForSeconds(time);
        while (true)
        {
            rand = Random.Range(1, 100);
            if (rand % windScript.CurrentWindSettingEmissionRate > 0)
            {
                for (int i = 0; i < MaxTumbleweed / 2; i++)
                {
                    if (list_Tumbleweeds[i].gameObject.activeSelf == false)
                    {
                        list_Tumbleweeds[i].m_rigidbody.velocity = zeroVec;
                        Debug.Log("velocity = " + list_Tumbleweeds[i].m_rigidbody.velocity);
                        list_Tumbleweeds[i].gameObject.SetActive(true);
                        list_Tumbleweeds[i].transform.position = InitializePosition();

                        if (time != windScript.CurrentWindSettingWait)
                        {
                            time = windScript.CurrentWindSettingWait;
                            wait = new WaitForSeconds(time);
                        }
                        yield return wait;
                        break;
                    }
                }
            }
            else
            {
                for (int i = MaxTumbleweed / 2; i < MaxTumbleweed; i++)
                {
                    if (list_Tumbleweeds[i].gameObject.activeSelf == false)
                    {
                        list_Tumbleweeds[i].m_rigidbody.velocity = zeroVec;
                        Debug.Log("velocity = " + list_Tumbleweeds[i].m_rigidbody.velocity);
                        list_Tumbleweeds[i].gameObject.SetActive(true);
                        list_Tumbleweeds[i].transform.position = InitializePosition();

                        if (time != windScript.CurrentWindSettingWait)
                        {
                            time = windScript.CurrentWindSettingWait;
                            wait = new WaitForSeconds(time);
                        }
                        yield return wait;
                        break;
                    }
                }
            }
        }
    }

    void CreateTumbleweed(List<TumbleweedScript> list_Tumbleweeds,int i)
    {
    }

	
    Vector3 InitializePosition()
    {
        instancePosition.y = Random.Range(1.0f, 2.0f);
        instancePosition.x = Random.Range(-width, width);
        return instancePosition;
    }

	// Update is called once per frame
	void Update () {

        for (int i = 0; i < MaxTumbleweed; i++)
        {
            if (list_Tumbleweeds[i].gameObject.activeSelf == true)
            {
                if(list_Tumbleweeds[i].transform.position.z<stageMinZ)
                {
                    list_Tumbleweeds[i].m_rigidbody.velocity = zeroVec;
                    list_Tumbleweeds[i].gameObject.SetActive(false);
                }
            }
        }

	}
}
