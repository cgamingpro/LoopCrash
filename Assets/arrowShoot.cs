using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowShoot : MonoBehaviour
{
   
    public float bulletForce = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shootORigon;
    [SerializeField] AudioClip shootSound;
    AudioSource shootSoundSource;
    Camera cam;
    float orifinalFov;
    [SerializeField] float smooth;
    // Start is called before the first frame update
    void Start()
    {
        shootSoundSource = GetComponent<AudioSource>();
        cam = Camera.main;
        orifinalFov = cam.orthographicSize;
    }

    private void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, orifinalFov, smooth);
    }
    public void Shoot()
    {
        
        shootSoundSource.PlayOneShot(shootSound);
        cam.orthographicSize = cam.orthographicSize + 2;
        GameObject bullet = Instantiate(bulletPrefab, shootORigon.transform.position, Quaternion.FromToRotation(Vector3.up, shootORigon.transform.up));
        if (bullet.GetComponent<Rigidbody>() == null) { Debug.Log("no rigidlb"); }
        bullet.GetComponent<Rigidbody>().AddForce(shootORigon.transform.up * bulletForce, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        

    }
}
