using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField] int health = 100;
    int maxHalth;
    [SerializeField] Renderer enmyRender;
    Color col;
    playerMovment playermmnt;
    trailSpawner trailSpawner;
    [SerializeField]AudioClip audioClip;
    AudioSource audioSource;
    
    private void Start()
    {
        enmyRender = GetComponent<Renderer>();
        col = enmyRender.material.color;
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
        col.a = health / maxHalth;
        enmyRender.material.color = col;
    }
}
