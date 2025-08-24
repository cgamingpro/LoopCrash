using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] float moveSpeed = 2f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity =  Vector3.Lerp(rb.velocity,direction * moveSpeed,0.1f);
    }
}
