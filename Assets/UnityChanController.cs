using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;

    private Rigidbody myRigidbody;

    //前方向の速度
    private float velocityZ = 16f;

    //横方向の速度
    private float velocityX = 10f;

    //縦方向の速度(ジャンプ)
    private float velocityY = 10f;

    //左右の移動範囲
    private float movableRange = 3.4f;

    //動きを減速させる係数
    private float coefficient = 0.99f;

    private float TextCoefficient = 0.01f;

    //ゲーム終了判定
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト
    private GameObject stateText;

    //スコア表示用のテキスト
    private GameObject scoreText;
    private int score = 0;//得点

    //テキストの透明度
    private float a_color;

    //ボタン
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;
    private bool isJButtonDown = false;

    //private float speed;

    // Start is called before the first frame update
    void Start()
    {
        //this.speed = 1;

        this.myAnimator = GetComponent<Animator>();

        //this.myAnimator.SetFloat("Speed", speed);

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();

        //スコアテキストオブジェクト取得
        this.scoreText = GameObject.Find("ScoreText");

        //ゲームスタート時テキスト
        this.a_color = 1;

        this.stateText = GameObject.Find("GameResultText");

        this.stateText.GetComponent<Text>().text = "GAME START";

        this.stateText.GetComponent<Text>().color = new Color(1, 1, 1, a_color);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isEnd)
        {
            //this.speed *= this.coefficient;
            //this.myAnimator.SetFloat("Speed", speed); ←ユニティちゃんの動きは止まるけど、物体本体がずっとスライドするように動き続ける。座標の移動も止めないといけない。

            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

            //テキストを徐々に表示...テキストが出てくるタイミングが遅い。。。
            this.a_color += this.TextCoefficient;
            this.stateText.GetComponent<Text>().color = new Color(1, 1, 1, a_color);
        }
        else
        {
           //スタート直後、"GAME START"の文字をだんだん薄くする。
                this.a_color -= this.TextCoefficient;
                this.stateText.GetComponent<Text>().color = new Color(1, 1, 1, a_color);
            
        }


        float inputVelocityX = 0;
        float inputVelocityY = 0;

        //ジャンプしてないときにスペースキーを押す
        if((Input.GetKey(KeyCode.Space)||this.isJButtonDown)&&this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);

            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jumpステートの場合にはJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        if((Input.GetKey(KeyCode.LeftArrow)||this.isLButtonDown)&& -this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        }
        if((Input.GetKey(KeyCode.RightArrow)||isRButtonDown)&& this.transform.position.x < this.movableRange)
        {
            inputVelocityX = this.velocityX;
        }

        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);
    }



    //Triggerモードでほかのオブジェクトと衝突したときの処理
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
           
        }

        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        if(other.gameObject.tag == "CoinTag")
        {
            //接触したコインオブジェクトの破棄
            Destroy(other.gameObject);

            GetComponent<ParticleSystem>().Play();

            this.score += 10;
            this.scoreText.GetComponent<Text>().text = $"Score {score}pt ";
        }
    }


    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
