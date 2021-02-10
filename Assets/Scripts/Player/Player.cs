using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //player variables
    [Header("Player Variables")]
    [SerializeField] private float _moveSpd;
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] private int _HP = 3;
    public bool alive = true;
    public bool _iFrames = false;
    //[SerializeField] private List<Bullet> _bullets;

    //other variables
    [Header("")]
    [SerializeField] private Transform top;
    private Vector2 _moveVec = Vector2.zero;
    public bool isShooting = false;
    public bool isDodging = false;
    private float dodgeTimer = 3.0f;
    [SerializeField] private float dodgeDuration = 1.0f;
    [SerializeField] private float timeBetweenDodges = 2.0f;

    private GameObject _managerObj;
    private BulletPool _manager;
    [SerializeField] private List<GameObject> _healthUI;


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
        //if (isDodging && dodgeTimer >= dodgeDuration) {
        //    isDodging = false;
        //    _iFrames = false;
        //    dodgeTimer = 0.0f;
        //}

        //if (isDodging)
        //{
        //    _animator.SetInteger("Anim", 3);
        //}
        //else if (isShooting)
        //{
        //    _animator.SetInteger("Anim", 2);
        //}
        //else if (_moveVec == Vector2.zero)
        //{
        //    _animator.SetInteger("Anim", 0);
        //}
        //else if (!isShooting && !isDodging)
        //{
        //    _animator.SetInteger("Anim", 1);
        //}
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            _Movement();
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (alive)
        {
            _moveVec = context.ReadValue<Vector2>().normalized * 0.5f;
            if (_moveVec != Vector2.zero)
            {
                _animator.SetInteger("Anim", 1);
                float angle = Mathf.Atan2(-_moveVec.x, _moveVec.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            } else
            {
                _animator.SetInteger("Anim", 0);
            }
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && !_manager.isEmpty() && alive)
        {
            Debug.Log("shots");
            isShooting = true;
            _animator.SetInteger("Anim", 2);
        }
    }

    public void Fire()
    {
        GameObject _newBullet = _manager.GetBullet();
        _newBullet.GetComponent<Bullet>().moveDir = transform.up;
        _newBullet.transform.position = top.transform.position;
        _newBullet.transform.rotation = transform.rotation;
    }

    public void OnDodge(InputAction.CallbackContext context) {
        if (!isDodging && dodgeTimer >= timeBetweenDodges && alive) {
            isDodging = true;
            _iFrames = true;
            dodgeTimer = 0.0f;
            _animator.SetInteger("Anim", 3);
        }
    }

    private void _Movement()
    {
        _rb.AddForce(_moveVec * _moveSpd * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.0f);
    }

    public void OnHit()
    {
        _healthUI[_HP-1].SetActive(false);

        _HP -= 1;

        _iFrames = true;

        if (_HP <= 0)
        {
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
            alive = false;
            _animator.SetTrigger("Dead");
        }
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(3);
    }

    public void AnimReset()
    {
        _animator.SetInteger("Anim", 0);
        isShooting = false;
        if (isDodging == true)
        {
            dodgeTimer = 0.0f;
            _iFrames = false;
        }
        isDodging = false;

    }
}
