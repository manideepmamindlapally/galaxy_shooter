using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10;

    public bool hitEnemy = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //incorporate movement
        Movement();
        //check enemy hit
        CheckEnemyHit();
	}

    private void Movement()
    {
        //shoot the laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //destroy the laser when out of the screen
        if (transform.position.y > 6)
        {
            //destroy the parent if any
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            //destroy yourself
            Destroy(this.gameObject);
        }
    }

    private void CheckEnemyHit()
    {
        //destroy the laser on hitting enemy
        if (hitEnemy)
        {
            //destroy the parent if any
            if (this.transform.parent != null)
            {
                //if the parent doesn't have other child
                if (this.transform.parent.childCount <= 1)
                {
                    Destroy(this.transform.parent.gameObject);
                }
            }
            //destroy yourself
            Destroy(this.gameObject);
        }
    }
}
