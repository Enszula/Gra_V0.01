using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] Transform platform;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    public float speed;

    Vector3 direction;
    Transform destination;
    
    // Use this for initialization
    void Start () {
        SetDestination(startPosition);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        platform.GetComponent<Rigidbody2D>().MovePosition(platform.position + direction * speed * Time.fixedDeltaTime);
        if(Vector3.Distance(platform.position, destination.position) < speed * Time.fixedDeltaTime)
        {
            SetDestination( destination == startPosition ? endPosition : startPosition);
        }		
	}

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }

    
}
