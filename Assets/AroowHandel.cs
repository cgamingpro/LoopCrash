using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroowHandel : MonoBehaviour
{
    [SerializeField] int damge = 10;
    Rigidbody rb;
    GameObject player;
    [SerializeField] float returnSpeed;
    int wallcollsion = 0;
    [SerializeField]AudioClip wallhit;
    [SerializeField]AudioClip playethit;
    [SerializeField]AudioClip brokenarrow;
    AudioSource AudioSource;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = transform.GetComponent<Rigidbody>();
        AudioSource = gameObject.GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy: " + other.name);
            other.GetComponent<ApplyDamage>().apply(damge);
            if(other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * 2,ForceMode.Impulse);
               
                Debug.Log("force aded");
            }

        }
        
        if(other.CompareTag("Wall"))
        {
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.up);
            transform.LookAt(player.transform.position,Vector3.up);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * returnSpeed, ForceMode.Impulse);
            wallcollsion++;
            AudioSource.PlayOneShot(wallhit);
        }

        if(other.CompareTag("Player"))
        {
            player.GetComponent<playerMovment>().canshoot = true;
            Destroy(this.gameObject);
            AudioSource.PlayOneShot(playethit);
        }

        if(wallcollsion > 1)
        {
            player.GetComponent<playerMovment>().AmmoCOunt--;
            Destroy(this.gameObject);
            AudioSource.PlayOneShot(brokenarrow);
        }
    }


}
