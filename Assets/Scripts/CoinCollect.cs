using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour {

    public Text countText;
    public int count;
    private string collected;

    private AudioSource audioSource;
    public AudioClip CoinCollectSound;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        count = 0;
        SetCountText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        collected = collision.name;
        audioSource.clip = CoinCollectSound;
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.Play();
            count += 1;
            SetCountText();
            collision.gameObject.SetActive(false);
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }
}
