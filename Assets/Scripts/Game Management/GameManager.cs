using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text countdownText;
    public GameObject startPanel;
    public static bool hasStarted = false;
    public PausePanel pausePanel;
    public BallScoreManager bscoreManager;

	// Use this for initialization
	void Start () {

        
        startPanel.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
      
        
		
	}
	
    //Called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.gameObject.SetActive(true);
        }
    }

    /**
     * Start the countdown in the start panel
     */
    IEnumerator StartCountdown() //IEnumerator because we have to return an instance of WaitForSeconds
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1); //no sleep function in Unity :(
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.1f); //allow the "GO" message to appear for 0.1 more seconds;
        startPanel.gameObject.SetActive(false);
        
        
        hasStarted = true;
        bscoreManager.ResetBall();

    }
}
