using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    // kod ktory powoduje przesowanie sie elementow terenu niezaleznie od siebie

    public Transform[] background; // lista elementow z terenu ktore maja sie poruszac niezaleznie od elementow gamplayu
    private float[] parallaxScales; // poruszanie sie kamery do elementow terenu
    public float smoothing = 1f; // wartosc ktora okresla jak lagodnie beda sie "poruszac" elementy

    private Transform cam; // odwolanie do glownej kamery
    private Vector3 previousCameraPosition; //pozycja kamery w poprzedniej klatce

    // aktywowane przed Start().
    void Awake()
    {
            //odwolanie do glownej kamery
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start ()
    {
            // ustalanie pozycji kamery z wartosci pozycji kamery w poprzedniej klatce
        previousCameraPosition = cam.position;

            // przypisywanie odpowiadajacych elementow
        parallaxScales = new float[background.Length];
        for(int i = 0; i < background.Length; i++)
        {
            parallaxScales[i] = background[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < background.Length; i++)
        {   
            float parallax = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];
                // ustawienie wartosic elementu x aktualnego obiektu obecna wartosia parallaxu
            float backgroundTargetPositionX = background[i].position.x + parallax;
                // tworzenie pozycji obiektu z tla z wartoscia x 
            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, background[i].position.y, background[i].position.z);
                // przemieszczanie pomiedzy klatkami z odpowiednia plynnoscia
            background[i].position = Vector3.Lerp(background[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
        }
        previousCameraPosition = cam.position;
    }
}
