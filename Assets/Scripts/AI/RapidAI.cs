using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidAI : EnemyAIBase
{
    public float movementDistance = 5.0f;
    public int bulletsPerShot = 6;
    private int bulletCount;
    public float timeBetweenBullets = 0.05f;
    private float bulletTimer = 0.0f;
    private bool isShooting = false;


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
        GameObject _newBullet = pool.GetBullet();
        _newBullet.transform.position = this.transform.position;

        var offset = -90.0f;
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _newBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        _newBullet.GetComponent<Bullet>()._moveDir = _newBullet.transform.up;
        _newBullet.GetComponent<Bullet>()._speed = bulletSpeed;
    }

    protected override void Update() {
        Move();
        shotTimer += Time.deltaTime;
        if (shotTimer >= timeBetweenShots) {
            isShooting = true;
        }
    }
    private void FixedUpdate() {
        if (isShooting) {
            if (bulletCount < bulletsPerShot) {
                if (bulletTimer >= timeBetweenBullets) {
                    Attack();
                    bulletCount++;
                    bulletTimer = 0;
                }
                else {
                    bulletTimer += Time.fixedDeltaTime;
                }
            }
            else if (bulletCount >= bulletsPerShot) {
                shotTimer = 0;
                bulletCount = 0;
                isShooting = false;
            }
        }
    }
}
