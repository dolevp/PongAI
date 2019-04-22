using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PADDLE : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ball")
        {
            col.gameObject.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        }
    }
}
