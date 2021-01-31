using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidAI : EnemyAIBase
{
    private Vector2 _moveVec;
    public float timeBetweenMoves = 5;
    private float moveTimer = 0;
    private bool moveDir = true;

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
    }

    protected override void Move()
    {
        if (moveDir)
        {
            _moveVec.x = 1.0f;
        }
        else
        {
            _moveVec.x = -1.0f;
        }
        _moveVec.y = 0.0f;

        _moveVec.Normalize();

        _rb.AddForce(_moveVec * speed, ForceMode2D.Impulse);

        moveDir = !moveDir;
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
        shotTimer += Time.deltaTime;
        if (shotTimer >= timeBetweenShots) {
            isShooting = true;
        }
    }
    private void FixedUpdate() {
        moveTimer += Time.fixedDeltaTime;

        // friction
        _rb.AddForce(-_rb.velocity * 1.1f * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if (moveTimer >= timeBetweenMoves)
        {
            _rb.velocity = Vector2.zero;
            Move();
            moveTimer = 0;
        }

        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.0f);

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
