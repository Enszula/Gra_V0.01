using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    
    public float fireRate = 0f;
    public float damage = 10f;
    public float fireDistance = 100f;
    public LayerMask whatToHit;

    public Transform bulletTrailPrefab;

    float timeToFire = 0;
    Transform firePoint;

	void Awake ()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("Brak celownika!");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fireRate == 0)
        {
            // sprawdzanie czy to pojedynczy strzal
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            // czy seria strzalow
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        
	}

    void Shoot()
    {   
        // konwersa punktow z obrazu do swiata gry
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, fireDistance, whatToHit);
        Effect();
        //Debug.DrawLine(firePointPosition, mousePosition, Color.cyan);
        if(hit.collider != null)
        {
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("we hit" + hit.collider + " and did" + damage + " damage");
        }
    }

    void Effect()
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}