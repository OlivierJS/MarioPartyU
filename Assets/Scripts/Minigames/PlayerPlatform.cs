using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    public int playerID;
    public float speed = 7.0f;
    public float jumpHeight = 3.0f;
    public Rigidbody rb;
    public Vector3 movement;
    public bool isGrounded = true;
    PlatformingManager thePlatformingManager;

    // Start is called before the first frame update
    void Start()
    {
        thePlatformingManager = GameObject.FindObjectOfType<PlatformingManager>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerID)
        {
            case 0:
                movement = new Vector3(Input.GetAxis("HorizontalP1"), 0f, Input.GetAxis("VerticalP1"));
                break;
            case 1:
                movement = new Vector3(Input.GetAxis("HorizontalP2"), 0f, Input.GetAxis("VerticalP2"));
                break;
        }
        if (thePlatformingManager.timeUp != true && thePlatformingManager.P1Win != true && thePlatformingManager.P2Win != true)
        {
            jumpCharacter();
        }
    }

    void FixedUpdate()
    {
        if (thePlatformingManager.timeUp != true && thePlatformingManager.P1Win != true && thePlatformingManager.P2Win != true)
        {
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
    }

    void jumpCharacter()
    {
        switch(playerID)
        {
            case 0:
                if(Input.GetButtonDown("JumpP1") && isGrounded == true)
                {
                    rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
                    isGrounded = false;
                }
                break;
            case 1:
                if(Input.GetButtonDown("JumpP2") && isGrounded == true)
                {
                    rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
                    isGrounded = false;
                }
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            switch (playerID)
            {
                case 0:
                    thePlatformingManager.P2Win = true;
                    break;
                case 1:
                    thePlatformingManager.P1Win = true;
                    break;
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }   
        if (collision.gameObject.name == "Lava")
        {
            switch(playerID)
            {
                case 0:
                    thePlatformingManager.P2Win = true;
                    break;
                case 1:
                    thePlatformingManager.P1Win = true;
                    break;
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }
}