using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAI : EnemyAIBase
{
    public float movementDistance = 5.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Shotgun;
        pool.SetSize(poolSize);
    }

    protected override void Move()
    {
        // shotgun AI's movement
        // sinusoidal movement in the y axis

        gameObject.transform.position = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * speed) * movementDistance, 0.0f);
    }

    protected override void Attack()
    {
        // shotgun AI's attack
        shotTimer += Time.deltaTime;

        if (shotTimer >= timeBetweenShots) {

            GameObject _newBullet;

            var offset = -60.0f;
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            for (int i = 0; i < 5; i++) {
                _newBullet = pool.GetBullet();
                _newBullet.transform.position = this.transform.position;
                _newBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
                _newBullet.GetComponent<Bullet>()._moveDir = _newBullet.transform.up;
                _newBullet.GetComponent<Bullet>()._speed = bulletSpeed;
                offset -= 15.0f;
            }

            shotTimer = 0;
        }
    }
}
