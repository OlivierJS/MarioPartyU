using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private Transform Bullet;
    public int direction = 0; //0 = right, 1 = left, 2 = up, 3 = down
    Quaternion rotation;
    public float interval;
    PlatformingManager thePlatformingManager;

    // Start is called before the first frame update
    void Start()
    {
        thePlatformingManager = GameObject.FindObjectOfType<PlatformingManager>();
        interval = Random.Range(3.0f, 20.0f);
        switch (direction)
        {
            case 0:
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 1:
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                rotation = Quaternion.Euler(90, 0, 0);
                break;
            case 3:
                rotation = Quaternion.Euler(90, 0, 0);
                break;
        }
        StartCoroutine(randomFire(interval));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void shootBullet()
    {
        Transform bulletTransform = Instantiate(Bullet, transform.position, rotation);

        int dir = direction;
        float spd = Random.Range(5.0f, 12.0f);
        bulletTransform.GetComponent<Bullet>().Setup(dir, spd);
    }

    IEnumerator randomFire(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (thePlatformingManager.timeUp != true && thePlatformingManager.P1Win != true && thePlatformingManager.P2Win != true)
            {
                shootBullet();
            }
        }
    }
}
