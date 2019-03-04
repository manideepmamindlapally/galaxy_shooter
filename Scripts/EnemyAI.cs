using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    //speed
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private GameObject _enemyExplosionPreFab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _explosionAuidio;

	// Use this for initialization
	void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //move the enemy
        Movement();

	}


    //check collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the collider is laser
        if(other.tag == "laser")
        {
            //access the laser
            Laser laserHit = other.GetComponent<Laser>();

            if(laserHit != null)
            {
                //change the hitEnemy variable to true
                laserHit.hitEnemy = true;
            }

            //destroy yourself
            SelfDestroy();
        }

        //if the collider is laser
        else if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                //change the hitEnemy variable to true
                player.hitEnemy = true;
            }

            //destroy yourself
            SelfDestroy();
        }
    }

    //move the enemy
    private void Movement()
    {
        //move the enemy
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if moves below the screen
        if (transform.position.y < -6)
        {
            //spawn on top
            transform.position = new Vector3(Random.Range(-9f, 9f), 6f, 0);
        }
    }

    //destroy the enemy
    private void SelfDestroy()
    {
        //play the explosion animation
        Instantiate(_enemyExplosionPreFab, transform.position, Quaternion.identity);
        //play the explosion audio
        AudioSource.PlayClipAtPoint(_explosionAuidio, Camera.main.transform.position);
        //update score
        _uiManager.UpdateScore();

        //destroy
        Destroy(this.gameObject);
    }
}
