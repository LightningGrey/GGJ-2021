using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player variables
    [Header("Player Variables")]
    [SerializeField] private float _moveSpd;
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] private int _HP = 3;
    private bool _alive = true;
    //[SerializeField] private List<Bullet> _bullets;

    //other variables
    [Header("")]
    [SerializeField] private Transform top;
    private Vector2 _moveVec = Vector2.zero;
    public bool isDodging = false;
    private float dodgeTimer = 0.0f;
    [SerializeField] private float dodgeDuration = 1.0f;
    [SerializeField] private float timeBetweenDodges = 3.0f;

    private GameObject _managerObj;
    private BulletPool _manager;


    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _managerObj = GameObject.FindGameObjectWithTag("Manager");
        _manager = _managerObj.GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        dodgeTimer += Time.deltaTime;
        if (isDodging && dodgeTimer >= dodgeDuration) {
            isDodging = false;
            dodgeTimer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if (_alive)
        {
            _Movement();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVec = context.ReadValue<Vector2>().normalized * 0.5f;
        if (_moveVec != Vector2.zero)
        {
            float angle = Mathf.Atan2(-_moveVec.x, _moveVec.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && !_manager.isEmpty() && _alive)
        {
            GameObject _newBullet = _manager.GetBullet();
            _newBullet.GetComponent<Bullet>().moveDir = transform.up;
            _newBullet.transform.position = top.transform.position;
            _newBullet.transform.rotation = transform.rotation;
        }
    }

    public void OnDodge(InputAction.CallbackContext context) {
        if (!isDodging && dodgeTimer >= timeBetweenDodges && _alive) {
            isDodging = true;
            dodgeTimer = 0.0f;
        }
    }

    private void _Movement()
    {
        _rb.AddForce(_moveVec * _moveSpd * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.0f);
    }

    public void OnHit()
    {
        _HP -= 1;
        if (_HP <= 0)
        {
            _rb.velocity = Vector2.zero;
            _alive = false;

        }
    }

    public void OnDead()
    {

    }

}
