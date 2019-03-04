using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Sprite[] _livesUI;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private Image _mainMenu;
    [SerializeField]
    private Text _scoreText;
    

    [SerializeField]
    private int _score;

    public void UpdateLives(int currentLives)
    {
        _livesDisplay.sprite = _livesUI[currentLives];
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "SCORE: " + _score;
    }

    public void InitGame()
    {
        //set score to zero
        _score = 0;
        //set score text to zero
        _scoreText.text = "SCORE: ";
        //disable main menu
        _mainMenu.enabled = false;
    }

    public void EndGame()
    {
        //show the menu
        _mainMenu.enabled = true;
    }   
}
