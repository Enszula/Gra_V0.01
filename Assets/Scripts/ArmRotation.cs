using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        //roznica pomiedzy polozeniem postaci i myszki
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // normalizacja wartosci punktow(sumy) x,y,z zeby zawsze sie rownaly 1 i byly pewniejsze

        //kat pomiedzy punktami (0,0) i (2,2) i konwersja z rad do stopni
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
