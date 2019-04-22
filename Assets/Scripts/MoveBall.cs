using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour {

    Vector3 ballStartPosition;
    Rigidbody2D rb;
    float speed = 400;
    public AudioSource blip;
    public AudioSource blop;
    public Text myText;
    public Text aiText;
    public int myScore = 0;
    public int aiScore = 0;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        ballStartPosition = this.transform.position;
        ResetBall();
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "backwall" )
        {
            blop.Play();
            myScore++;
            ResetBall();
        }
        else if (col.gameObject.tag == "justawall")
        {
            blop.Play();
            aiScore++;
            ResetBall();
        }

        else { 
            blip.Play();
        }
       
        SetScore();
    }

    /**
     * Reset the ball's position and shoot it towards one of the 2 field sides
     */
    public void ResetBall()
    {
        if (GameManager.hasStarted)
        {
           
            this.transform.position = ballStartPosition;
            rb.velocity = Vector3.zero;
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
    void SetScore()
    {
        myText.text = myScore + ""; //using string and int concatenation instead of casting
        aiText.text = aiScore + ""; //same thing here
    }
}
