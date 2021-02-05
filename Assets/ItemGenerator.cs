using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;
 

    public GameObject coinPrefab;


    public GameObject conePrefab;
 


    //課題
    private Renderer targetRenderer;

    //スタート地点
    private int startPos = 15;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //アイテム自動生成されるz方向の範囲
    private int zRange = 50;
    //Unityちゃんのz軸方向の位置＋zRangeの値。このラインをUnityちゃんが超えたら、再び自動生成メソッドがかかる。
    private float z = 0;

    // Start is called before the first frame update
    void Start()
    {
        //課題

        Vector3 UnityPos = GameObject.Find("unitychan").transform.position;

        z += UnityPos.z + zRange;
        for(int i = startPos; i < z; i += 15)//Unitychan.transform.position.z+50
        {
            this.ItemGenerate(i);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 UnityPos = GameObject.Find("unitychan").transform.position;

        
        if (UnityPos.z >= startPos)//z % 15 == 0　はダメだった。15m進むごとに一つオブジェクト生成されるイメージだった。update()が1フレームごとに呼ばれる中、そのフレーム毎のUnityPos.zの値が全然整数じゃないから…？
        {
            z += zRange;
            startPos += zRange;

            for (int i = startPos; i < z; i += 15)//Unityちゃんが進むごとにアイテム生成されたけど、なんか変な作り方してる気がする。
            {
                //zがゴールラインを越えたら生成しない。
                if (z < goalPos)
                {
                    this.ItemGenerate(i);
                }
            }
            
        }
    }

    void ItemGenerate(float i)
    {
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int j = -1; j <= 1; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);

                int offsetZ = Random.Range(-5, 6);

                //60%coin 30%car 10%nothing
                if (1 <= item && item <= 6)
                {
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);

                }
                else if (7 <= item && item <= 9)
                {
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                }
            }
        }
    }
}
