using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float CamFollowSmooth;
    void Update()
    {

        Vector3 playerSmooth = player.position + offset;

       


        this.transform.position = Vector3.Lerp(transform.position, playerSmooth, CamFollowSmooth * Time.deltaTime);
    }
}
    
