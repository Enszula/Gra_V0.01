using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int fallDeathLimit = -20;

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Imit()
        {
            curHealth = maxHealth;
        }
    }

    public PlayerStats playerStats = new PlayerStats();

    [Header("Optional")]
    [SerializeField] private PlayerStatus statusIndicator;

    void Start()
    {
        playerStats.Imit();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
        }
    }

    void Update()
    {   
        if(transform.position.y <= fallDeathLimit)  // obrazenia od spadania poza mape
        {
            DamagePlayer(999999);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Dealt 10 dmg");
            DamagePlayer(10);
        }
    }
        // otrzymywanie obrazen przez postac
    public void DamagePlayer(int damage)
    {
        playerStats.curHealth -= damage;
        if (playerStats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
        }
    }
    
}
