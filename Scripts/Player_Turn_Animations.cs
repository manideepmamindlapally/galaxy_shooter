using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Turn_Animations : MonoBehaviour {

    private Animator _playerAnimator;

    private bool _leftKey = false;
    private bool _rightKey = false;
	// Use this for initialization
	void Start () {
        _playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // left turn animations
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _leftKey = true;
            if(_rightKey == false)
            {
                _playerAnimator.SetBool("Turn_Left", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _leftKey = false;
            _playerAnimator.SetBool("Turn_Left", false);
            if (_rightKey == true)
            {
                _playerAnimator.SetBool("Turn_Right", true);
            }
            
        }

        // right turn animations
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rightKey = true;
            if(_leftKey == false)
            {
                _playerAnimator.SetBool("Turn_Right", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _rightKey = false;
            _playerAnimator.SetBool("Turn_Right", false);
            if (_leftKey == true)
            {
                _playerAnimator.SetBool("Turn_Left", true);
            }
            
        }
    }
}
