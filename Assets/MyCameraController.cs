using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    private GameObject unitychan;

    //private Rigidbody uniRigidbody;

    private float difference;

    //ゴール時カメラ回転速度
    private float rotateSpeed = 0;

    //ゴールポジション
    private float goalPos = 370f;

    //ゴールの可否
    private bool isGoal = false;

    //ゴールしたらユニティちゃんにカメラが少し近づく。
    private float approach = 0.1f;

    private float y_approach = 3f;

    // Start is called before the first frame update
    void Start()
    {
        this.unitychan = GameObject.Find("unitychan");

        this.difference = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGoal)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
        }
        else
        {
            rotateSpeed = 0.3f;

            this.transform.RotateAround(unitychan.transform.position, Vector3.up, rotateSpeed);
        }

        if(this.unitychan.transform.position.z >= goalPos && (!isGoal))
        {
            //ゴールラインをユニティちゃんが超えたら、isGoalをtrueに。
            isGoal = true;

            this.difference = approach;
            this.transform.position = new Vector3(0, this.transform.position.y - y_approach, this.unitychan.transform.position.z - difference);
        }

    }
}
