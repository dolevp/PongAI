using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StartManager : MonoBehaviour
{

    public Text inputText;
    public GameObject errorTextGO;
    public GameObject startPanel;

    private enum InputType
    {
        Number,
        Inf,
        Invalid
    }
    void Start()
    {
        errorTextGO.SetActive(false);
        startPanel.SetActive(false);
    }

    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
    }
    private void GoPlay(int maxScore)
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        SceneManager.LoadScene(1);
    }


    private InputType getInputType(string input)
    {

        if (Regex.IsMatch(input, @"^\d+$") && int.Parse(input) >= 0) // It's a number, and input >= 0
        {
            return InputType.Number;
        }
        else if(input == "inf" || input == "Inf" || input == "Infinity" || input == "infinity")
        {
            return InputType.Inf;
        }
        return InputType.Invalid;
    }

    public void Submit()
    {
        string input = inputText.text;

        if (getInputType(input) == InputType.Number)
        {

            errorTextGO.SetActive(false);
            GoPlay(int.Parse(input));
        }
        else if (getInputType(input) == InputType.Inf)
        {
            errorTextGO.SetActive(false);
            GoPlay(0);
        }
        else //getInputType(input) == InputType.Invalid
        {
            errorTextGO.SetActive(true);
        }
    }
}
