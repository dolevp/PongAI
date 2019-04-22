using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private BallManager ballManager;

    void Awake()
    {
        ballManager = GameObject.Find("BallManager").GetComponent<BallManager>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "backwall")
        {
            ballManager.blop.Play();
            ballManager.myScore++;
            ballManager.ResetBall();
        }
        else if (col.gameObject.tag == "justawall")
        {
            ballManager.blop.Play();
            ballManager.aiScore++;
            ballManager.ResetBall();
        }

        else
        {
            ballManager.blip.Play();
        }

        ballManager.SetScore();
    }
}
