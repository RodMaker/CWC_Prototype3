using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject enemy;
    private Rigidbody enemyRB;
    private Animator enemyAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem fireworksParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip explodeSound;
    public AudioClip moneySound;
    private AudioSource playerAudio;
    private Animator playerAnim;
    private Rigidbody playerRB;
    public float jumpForce = 10;
    public float doubleJumpForce = 400;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool canDoubleJump = false;
    public bool doubleSpeed = false;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = enemy.GetComponent<Rigidbody>();
        enemyAnim = enemy.GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            enemyRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            enemyAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !gameOver)
        {
            playerRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            enemyRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            canDoubleJump = false;
            //playerAnim.SetTrigger("Jump_trig");
            playerAnim.Play("Running_Jump", 3, 0f);
            enemyAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            enemyAnim.SetFloat("Speed_Multiplier", 2.0f);
        } 
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            enemyAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        else if (other.gameObject.CompareTag("Bomb") && !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
        }
        else if (other.gameObject.CompareTag("Money") && !gameOver)
        {
            Debug.Log("Money grabbed");
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
