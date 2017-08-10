using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{

    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform goal = null;

    [SerializeField]
    RectTransform ui_Player = null;
    [SerializeField]
    Transform ui_Start = null;
    [SerializeField]
    Transform ui_Goal = null;

    float stageSizeZ;
    // Use this for initialization
    void Start()
    {
        stageSizeZ = goal.position.z * 2;
    }

    // Update is called once per frame
    void Update()
    {
        ui_Player.position = Vector3.Lerp(ui_Start.position, ui_Goal.position, (1.0f - ((goal.position.z - player.position.z) / stageSizeZ)));
    }
}
