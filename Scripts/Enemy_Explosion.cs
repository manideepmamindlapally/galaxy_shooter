using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SelfDestroy());
	}

    //destroy the animation
    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
