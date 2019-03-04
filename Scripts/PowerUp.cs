using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerUpId;  //0-tripleshoot 1-speedboost 2-shield
    [SerializeField]
    private AudioClip _powerUpAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //destroy when off screen
        if (transform.position.y < -6) Destroy(this.gameObject);
    }

    //is called on Triggered Collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        //print to console
        //print("collided with : " + other);

        //check if collided with player
        if(other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();
            //play the PowerUp Audio
            AudioSource.PlayClipAtPoint(_powerUpAudio, Camera.main.transform.position);

            if(player != null)
            {
                if(_powerUpId == 0)
                {
                    //enable triple shoot for the player
                    player.TripleShootPowerUpOn();
                    //powerdown triple shoot
                    player.TripleShootPowerDown();
                }
                else if(_powerUpId == 1)
                {
                    //enable Speedboost
                    player.SpeedBoostOn();
                    //powerdown speedboost
                    player.SpeedBoostDown();
                }
                else if(_powerUpId == 2)
                {
                    //enable shield
                    player.ShieldOn();
                    //powerdown shield
                    player.ShieldDown();
                }
            }           

            //destroy yourself
            Destroy(this.gameObject);
        }
    }
}
