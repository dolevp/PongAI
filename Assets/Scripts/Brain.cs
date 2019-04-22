using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Brain : MonoBehaviour {
   
    public GameObject paddle;
    public GameObject ball;
    Rigidbody2D brb;
    float distY;
    public string backwallTag = "backwall";
    float paddleMaxSpeed = 15;
    public float numSaved = 0;
    public float numMissed = 0;
    public double alpha = 0.83;

    ANN ann; //the ANN this Brain is using - the core of this whole script

	//GameObject initialization
	void Start (){

        ann = new ANN(6, 1, 2, 5, alpha);

        brb = ball.GetComponent<Rigidbody2D>();
        
	}

    /**
     * Calculate and return a list of output from given parameters
     * @param bx - ball x position
     * @param by - ball y position
     * @param bvx - the ball's x velocity
     * @param bvy - the ball's y velocity
     * @param px - paddle's x position
     * @param py - paddle's y position
     * @param pv - the distance between the paddle and the expected hit (expected output)
     * @param train - whether we should train the ANN or not
     * @return a list of outputs that represent the new paddle's position in relation to the current one
     */
    List<double> Run(double bx, double by, double bvx, double bvy, double px, double py, double pv, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(bx);
        inputs.Add(by);
        inputs.Add(bvx);
        inputs.Add(bvy);
        inputs.Add(px);
        inputs.Add(py);
        outputs.Add(pv);
        if (train)
            return (ann.Train(inputs, outputs));
        else
            return (ann.CalcOutput(inputs, outputs));
    }

    //Update is called once per frame
    void Update() {

        if (GameManager.hasStarted)
        {

            float posy = Mathf.Clamp(paddle.transform.position.y + (distY * Time.deltaTime * paddleMaxSpeed),
                GameSettings.PADDLE_MIN_Y, GameSettings.PADDLE_MAX_Y); //we clamp the y position of the paddle between its max and min values
            paddle.transform.position = new Vector3(paddle.transform.position.x, posy, paddle.transform.position.z);

            List<double> output = new List<double>();
            int layerMask = 1 << 9; //we use shifting to get the layerMask as a bit mask
            RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, brb.velocity, 1000, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "boundary")
                {
                    //the ball is going towards one of the field's boundaries (top, bottom or corners)

                    Vector3 reflection = Vector3.Reflect(brb.velocity, hit.normal);
                    hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask);
                }


                if (hit.collider != null && hit.collider.gameObject.tag == backwallTag)
                {
                    //the ball is going towards the paddle's back wall

                    float dy = (hit.point.y - paddle.transform.position.y); 

                    output = Run(ball.transform.position.x,
                        ball.transform.position.y,
                        brb.velocity.x, brb.velocity.y,
                        paddle.transform.position.x, paddle.transform.position.y,
                        dy, true);
                    distY = (float)output[0];

                }
            }
        }
	}
}
