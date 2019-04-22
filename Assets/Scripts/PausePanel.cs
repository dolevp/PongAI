using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {

        Time.timeScale = 0;
	}
	void OnDisable()
    {
        Time.timeScale = 1;
    }
	// Update is called once per frame
	public void Resume () {

        gameObject.SetActive(false);
	}

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
