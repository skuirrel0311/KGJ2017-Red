using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTumbleweedScript : MonoBehaviour {

    [SerializeField]
    GameObject tumbleweedPrefab = null;
    [SerializeField]
    BoxCollider m_col=null;

    float width=0;

    [SerializeField]
    float stageMinZ = 0;
    const int MaxTumbleweed = 200;

    List<TumbleweedScript> list_Tumbleweeds = new List<TumbleweedScript>();

    [SerializeField]
    WaitForSeconds wait;

    Vector3 instancePosition;

    //キャッシュ
    Vector3 zeroVec = Vector3.zero;

	// Use this for initialization
	void Start () {
        m_col=GetComponent<BoxCollider>();
        width = m_col.size.x * transform.localScale.x;

        for (int i = 0; i < MaxTumbleweed; i++)
        {
            TumbleweedScript tumbleweed = ((GameObject)Instantiate(tumbleweedPrefab)).GetComponent<TumbleweedScript>();
            tumbleweed.transform.parent = this.transform;
            tumbleweed.gameObject.SetActive(false);
            list_Tumbleweeds.Add(tumbleweed);
        }

        instancePosition = new Vector3(0, 0, transform.position.z);
        StartCoroutine(CreateTumbleweed());
	}

    IEnumerator CreateTumbleweed()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while(true)
        {
            for (int i = 0; i < MaxTumbleweed; i++)
            {
                if (list_Tumbleweeds[i].gameObject.activeSelf == false)
                {
                    list_Tumbleweeds[i].m_rigidbody.velocity = zeroVec;
                    Debug.Log("velocity = " + list_Tumbleweeds[i].m_rigidbody.velocity);
                    list_Tumbleweeds[i].gameObject.SetActive(true);
                    list_Tumbleweeds[i].transform.position = InitializePosition();
                    // Instantiate(tumbleweedPrefab, InitializePosition(), Quaternion.identity);
                    yield return wait;
                }
            }
            yield return wait;
        }
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
