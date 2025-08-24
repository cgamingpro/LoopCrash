using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class playerMovment : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI ammoCount_text;
    [SerializeField] TextMeshProUGUI score_text;
    public float score = 0f;
    public int AmmoCOunt = 7;
    Camera cam;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSmooth;
    Vector3 move ;
    [SerializeField] GameObject crossairObj;
    [SerializeField] Sprite hitcross;
    [SerializeField] Sprite relosCross;
    Transform bow;
    arrowShoot arrowShoot;
    public bool canshoot = true;
    float timmer = 0f;
    [SerializeField] float realodTime = 2f;
    bool isrloading = false;
    [SerializeField] GameObject projectileShow;
    void Start()
    {
        cam = Camera.main;
        bow = transform.GetChild(0);
        arrowShoot = bow.GetComponent<arrowShoot>();
        Cursor.visible = false;
    }

    void Update()
    {
        ammoCount_text.text = AmmoCOunt.ToString();
        score_text.text = score.ToString();
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(MoveX, MoveY, 0f);
       
        // Initialize 'move' to avoid CS0165 error  
       

        move = Vector3.Lerp(move, moveDir, moveSmooth);
        transform.position += move * Time.deltaTime * moveSpeed;




        // Get mouse world position with proper Z distance  
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        
        mousePos.z = 0f; // Keep it in 2D plane  
        crossairObj.transform.position = mousePos;

        // Direction from player to mouse  
        Vector3 direction = (mousePos - transform.position).normalized;

        // Get angle in degrees  
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate player  
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);


       
        if(isrloading )
        {
            timmer += Time.deltaTime;
            if(timmer >= realodTime)
            {
                canshoot = true; 
                isrloading = false;
                Debug.Log("reloaded");
                
            }
        }

       if (Input.GetMouseButtonDown(0) && canshoot && !isrloading)
       {
            shootArrow();
       }
       if (Input.GetKeyDown(KeyCode.R) && AmmoCOunt > 0)
       {
            timmer = 0f;
            isrloading = true;
       }

       if(canshoot)
        {
            projectileShow.SetActive(true);
        }
        else
        {
            projectileShow.SetActive(false);
        }


       if(!isrloading)
        {
            crossairObj.GetComponent<SpriteRenderer>().sprite = relosCross;
        }
       else
        {
            crossairObj.GetComponent<SpriteRenderer>().sprite = hitcross;
        }
    }


    void shootArrow()
    {
        arrowShoot.Shoot();
        canshoot = false;
    }
   
   
}
