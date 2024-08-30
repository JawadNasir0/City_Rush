using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed=5;
    private PlayerController PlayerControllerScript;
    
    
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {   if(gameManager.isGameActive){
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        
        }

        if(transform.position.z < -15 && gameObject.CompareTag("Obstacle")){
            Destroy(gameObject);
        }
       
    }

    
}
