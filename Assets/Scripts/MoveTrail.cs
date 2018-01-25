using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int bulletSpeed = 200;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        Destroy(gameObject, .3f);
	}
}
