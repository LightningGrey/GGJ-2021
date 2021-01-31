using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidAI : EnemyAIBase
{
    public float movementDistance = 5.0f;
    public int bulletsPerShot = 6;
    private int bulletCount;
    public int framesBetweenBullets = 15;
    private int bulletTimer = 0;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Rapid;
        pool.SetSize(poolSize);
    }

    protected override void Move()
    {
        // rapid AI's movement
        // sinusoidal movement in the x axis

        gameObject.transform.position = startPos + new Vector3(Mathf.Sin(Time.time) * movementDistance, 0.0f, 0.0f);
    }

    protected override void Attack()
    {
        // rapid AI's attack
        shotTimer += Time.deltaTime;

        if (shotTimer >= timeBetweenShots)
        {
            if (bulletCount < bulletsPerShot) {
                if(bulletTimer>=framesBetweenBullets){
                    GameObject _newBullet;

                    var offset = -90.0f;
                    Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
                    direction.Normalize();
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    for (int i = 0; i < 5; i++) {
                        _newBullet = pool.GetBullet();
                        _newBullet.transform.position = this.transform.position;
                        _newBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
                    }
                    bulletCount++;
                    bulletTimer = 0;
                }
                else {
                    bulletTimer += 1;
                }
            }
            else if (bulletCount >= bulletsPerShot) {
                shotTimer = 0;
                bulletCount=0;
            }
        }
    }
}
