using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    public float speed;
    float targetY;
    int r, g, b = 125;

    //initialization
    void Start()
    {
        targetY = (GameSettings.PADDLE_MIN_Y + GameSettings.PADDLE_MAX_Y) / 2;
    }
	// Update is called once per frame
	void Update () {

        if (GameManager.hasStarted)
        {


            if (Input.GetKey(KeyCode.DownArrow))
            {

                targetY = Mathf.Clamp(transform.position.y - speed, GameSettings.PADDLE_MIN_Y, GameSettings.PADDLE_MAX_Y);

            }
            else if (Input.GetKey(KeyCode.UpArrow))

                targetY = Mathf.Clamp(transform.position.y + speed, GameSettings.PADDLE_MIN_Y, GameSettings.PADDLE_MAX_Y);


            Vector3 targetYPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = targetYPosition;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ball")
        {
            col.gameObject.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color; //paint the ball
        }
    }
}
