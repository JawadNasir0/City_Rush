using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public ParticleSystem explosionEffect;
    public ParticleSystem trailEffect;

    private BoxCollider boxCollider;

    private Animator playerAnimator;
    public float jumpForce =700f;
    public float gravityMultiplier=2;
    public bool isOnGround=true;
    public bool isRolling=false;
    public float laneDistance = 3.0f; // Distance between lanes
    private int currentLane = 0; // Start in the middle lane (assuming 0 is the middle lane)
    private Vector3 targetPosition;
    public float moveSpeed = 7.0f;
    private GameManager gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb=GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
        playerAnim= GetComponent<Animator>();
        playerAudio= GetComponent<AudioSource>();
        targetPosition=transform.position;
        boxCollider = GetComponent<BoxCollider>();
        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        jumpForce =700f;
        if(Input.GetKeyDown(KeyCode.UpArrow) && isOnGround==true && gameManager.isGameActive)
        {
            
            playerRb.AddForce(Vector3.up* 700, ForceMode.Impulse); 
            isOnGround= false;
            isRolling= false;
            playerAudio.PlayOneShot(jumpSound);
            trailEffect.Stop();
            playerAnim.SetTrigger("Jump_trig");
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && isOnGround==true && gameManager.isGameActive && isRolling==true )
        {
            playerAnim.SetTrigger("Jump_mid_roll_trig");
            playerRb.AddForce(Vector3.up* 700, ForceMode.Impulse); 
            playerAudio.PlayOneShot(jumpSound);
            isOnGround= false;
            isRolling =false;
               
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && isOnGround==true && gameManager.isGameActive)
        {
            trailEffect.Stop();
            playerAnim.SetTrigger("Roll_trig");
            isRolling=true;
        }
    
         if(Input.GetKeyDown(KeyCode.DownArrow) && isOnGround==false && isRolling==false && gameManager.isGameActive)
        {
            trailEffect.Stop();
            playerRb.AddForce(Vector3.down* jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Roll_mid_air_trig");
            isRolling=true;
        }
        
         if (playerAnimator.GetCurrentAnimatorStateInfo(3).IsName("Roll"))
        {
            // Adjust the Box Collider size and center during the roll
            boxCollider.size = new Vector3(0.6f, 0.65f, 0.4f);  // Example values
            boxCollider.center = new Vector3(0f, 0.33f, 0f);
        }
        else
        {
            // Reset to the default size and center when not rolling
           boxCollider.size = new Vector3(0.6f, 1.58f, 0.4f);  // Example values
            boxCollider.center = new Vector3(0f, 0.81f, 0f);
        }
        
            // Detect lane change input
            if (Input.GetKeyDown(KeyCode.LeftArrow ) || Input.GetKeyDown(KeyCode.A ))
            {
                ChangeLane(-1); // Move to the left lane
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow ) || Input.GetKeyDown(KeyCode.D ))
            {
                ChangeLane(1); // Move to the right lane
            }
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);        
    }
    void ChangeLane(int direction)
    {
        // Update the lane index
        currentLane += direction;

        // Clamp the lane index to ensure it stays within valid range (e.g., -1, 0, 1)
        currentLane = Mathf.Clamp(currentLane, -1, 1);

        targetPosition=new Vector3(currentLane*laneDistance,transform.position.y,transform.position.z);
        
    }
      
    private void OnCollisionEnter(Collision collision) {
            if(collision.gameObject.CompareTag("Ground")){
                isOnGround= true;
                trailEffect.Play();
            }
            if(collision.gameObject.CompareTag("Obstacle")){
                Debug.Log("Game over!");
                playerAudio.PlayOneShot(crashSound);
                playerAnim.SetBool("Death_b",true);
                playerAnim.SetInteger("DeathType_int",1);
                explosionEffect.Play();
                trailEffect.Stop();
                gameManager.GameOver();
            }
        }

}