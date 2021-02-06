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

    //ゴールしたらユニティちゃんにカメラが少し近づく。
    private float approach = 5f;

    // Start is called before the first frame update
    void Start()
    {
        this.unitychan = GameObject.Find("unitychan");

        //this.uniRigidbody = unitychan.GetComponent<Rigidbody>();

        this.difference = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);

        if(this.unitychan.transform.position.z >= goalPos )
        {
            rotateSpeed = 0.5f;

            this.difference = approach;

        }

        this.transform.RotateAround(unitychan.transform.position, Vector3.up, rotateSpeed);
    }
}
