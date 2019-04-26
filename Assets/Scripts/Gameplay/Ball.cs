using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private BallScoreManager ballScoreManager;

    void Awake()
    {
        ballScoreManager = GameObject.Find("BallScoreManager").GetComponent<BallScoreManager>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "backwall")
        {
            ballScoreManager.blop.Play();
            ballScoreManager.myScore++;
            ballScoreManager.ResetBall();
        }
        else if (col.gameObject.tag == "justawall")
        {
            ballScoreManager.blop.Play();
            ballScoreManager.aiScore++;
            ballScoreManager.ResetBall();
        }

        else
        {
            ballScoreManager.blip.Play();
        }

        ballScoreManager.SetScore();
    }
}
