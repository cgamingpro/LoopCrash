using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField] int health = 100;
    int maxHalth;
    float healthchange = 1;
    [SerializeField] Material mat;
  
    playerMovment playermmnt;
    trailSpawner trailSpawner;
    [SerializeField]AudioClip audioClip;
    AudioSource audioSource;
    
    private void Start()
    {
       
        mat = GetComponent<Renderer>().material;
        
        maxHalth = health;
        playermmnt = GameObject.FindWithTag("Player").GetComponent<playerMovment>();
        trailSpawner = GameObject.FindWithTag("Player").GetComponent<trailSpawner>();
        audioSource = GetComponent<AudioSource>();
    }
    public void apply(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            playermmnt.score += 100f;
            trailSpawner.currentTrial--;
            Destroy(gameObject);
        }
        audioSource.PlayOneShot(audioClip);
       
    }
    private void Update()
    {
        
        healthchange = Mathf.Lerp(healthchange, health / (float)maxHalth, Time.deltaTime * 5);
        mat.SetFloat("_health", healthchange); 
     
    }
}
