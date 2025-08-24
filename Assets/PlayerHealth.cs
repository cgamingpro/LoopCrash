using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] TextMeshProUGUI scoretesct;
    int maxHalth;
    [SerializeField] GameObject deathPannel;
    [SerializeField] Slider healthSlider;
    playerMovment playermmnt;
    trailSpawner trailSpawner;
    private void Start()
    {
        maxHalth = health;
        playermmnt = GameObject.FindWithTag("Player").GetComponent<playerMovment>();
        trailSpawner = GameObject.FindWithTag("Player").GetComponent<trailSpawner>();
    }
    public void apply(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Cursor.visible = true;
            deathPannel.SetActive(true);
            scoretesct.text = playermmnt.score.ToString();
            Debug.Log("PlayerDIed");
            
        }
    }
    private void Update()
    {
        float hh = health;
        float mh = maxHalth;
        healthSlider.value = hh/mh;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            apply(10);
        }
    }
    
}
