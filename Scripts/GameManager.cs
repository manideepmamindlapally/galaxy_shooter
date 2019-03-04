using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject _spawnManager;
    private UIManager _uiManager;
    
    [SerializeField]
    private bool _isGameOn =false;

    // Use this for initialization
    void Start () {
        //access the ui manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //endgame
        _uiManager.EndGame();
    }
	
	// Update is called once per frame
	void Update () {
        // if game if off and want to play
        CheckGame();
    }

    private void CheckGame()
    {
        //if game is not on turn it on when the user presses space bar
        if (Input.GetKeyDown(KeyCode.Space) && _isGameOn == false) StartGame();
    }

    private void StartGame()
    {
        //turn the game off
        _isGameOn = true;
        //Initialize Game On Spawn Manager
        SpawnManager spawnner = _spawnManager.GetComponent<SpawnManager>();
        spawnner.InitGame();
        //Initialise Game on UI Manager
        _uiManager.InitGame();
    }

    //called by player
    public void EndGame()
    {
        //end game
        _isGameOn = false;
        //End Game on UI Manager
        _uiManager.EndGame();
        //End the game in Spawn manager
        SpawnManager spawnner = _spawnManager.GetComponent<SpawnManager>();
        spawnner.EndGame();  
    }
}
