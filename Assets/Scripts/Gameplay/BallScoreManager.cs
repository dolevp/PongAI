using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScoreManager : MonoBehaviour {

    Vector3 ballStartPosition;
    public GameObject ball; //current ball that's showing on the field
    public GameObject ballPrefab;
    Rigidbody2D rb;
    float speed = 400;
    public AudioSource blip;
    public AudioSource blop;
    public Text myText;
    public Text aiText;
    public int myScore = 0;
    public int aiScore = 0;
    private TrailRenderer trail;
    public int maxScore;

    //Specific, winner-dependable panels
    public GameObject humanWonPanel;
    public GameObject aiWonPanel;

    public enum Player
    {
        Human,
        AI
    }
	// Use this for initialization
	void Awake () {
        ballStartPosition = ball.transform.position;
        Destroy(ball);
        ball = (GameObject)Instantiate(ballPrefab, ballStartPosition, Quaternion.identity);
        SetBallProperties();
        aiWonPanel.SetActive(false);
        humanWonPanel.SetActive(false);

        //Get the max score from PlayerPrefs
        maxScore = PlayerPrefs.GetInt("MaxScore");
	}


    /**
     * Resets the ball's properties like rigidbody, trail after we change GameObject ball
     */
    void SetBallProperties()
    {
        rb = ball.GetComponent<Rigidbody2D>();
        trail = ball.GetComponent<TrailRenderer>();
        
    }
    
    /**
     * Reset the ball's position and shoot it towards one of the 2 field sides
     */
    public void ResetBall()
    {
        if (GameManager.hasStarted)
        {
            Destroy(ball);
            ball = (GameObject)Instantiate(ballPrefab, ballStartPosition, Quaternion.identity);
            SetBallProperties();
            rb.velocity = Vector3.zero;

            //Shoot ball to random direction
            int d = Random.Range(1, 3) == 1 ? 1 : -1;
            Vector3 dir = new Vector3(d * Random.Range(100, 300), Random.Range(-100, 100), 0).normalized;
            rb.AddForce(dir * speed);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("space"))
        {
            ResetBall();
            
        }
	}

    void ResetScore()
    {
        myScore = aiScore = 0;
    }
    void CheckVictoryStatus()
    {
        if(myScore >= maxScore)
        {
            AnnounceWinner(Player.Human);
            
        }
        else if (aiScore >= maxScore)
        {
            AnnounceWinner(Player.AI);
        }
    }

    void AnnounceWinner(Player winner)
    {
        if(winner == Player.Human) //human player won
        {
            humanWonPanel.SetActive(true);
        }
        else //ai won
        {
            aiWonPanel.SetActive(true);
        }
    }
    public void SetScore()
    {
        myText.text = myScore + ""; //using string and int concatenation instead of casting
        aiText.text = aiScore + ""; //same thing here
        if(maxScore > 0) //infinite game, no need to check victory status
            CheckVictoryStatus();
    }
}
