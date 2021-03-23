using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Setup(int dir, float spd)
    {
        direction = dir;
        speed = spd;
    }
    
    public float speed = 15.0f;
    Rigidbody rb;
    public Vector3 movement;
    public int direction = 0; //0 = right, 1 = left, 2 = up, 3 = down
    PlatformingManager thePlatformingManager;

    // Start is called before the first frame update
    void Start()
    {
        thePlatformingManager = GameObject.FindObjectOfType<PlatformingManager>();
        gameObject.tag = "Bullet";
        rb = this.GetComponent<Rigidbody>();
        switch (direction)
        {
            case 0:
                movement = new Vector3(1f, 0f, 0f);
                break;
            case 1:
                movement = new Vector3(-1f, 0f, 0f);
                break;
            case 2:
                movement = new Vector3(0, 0f, 1f);
                break;
            case 3:
                movement = new Vector3(0f, 0f, -1f);
                break;
        }
        StartCoroutine(destroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlatformingManager.timeUp == true || thePlatformingManager.P1Win == true || thePlatformingManager.P2Win == true)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        moveBullet(movement);
    }

    void moveBullet(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
