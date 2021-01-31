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
    //[SerializeField] private List<Bullet> _bullets;

    //other variables
    [Header("")]
    [SerializeField] private Transform top;
    private Vector2 _moveVec = Vector2.zero;

    private GameObject _managerObj;
    private BulletPool _manager;

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

    }

    private void FixedUpdate()
    {
        _Movement(); 
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
        if (context.performed && !_manager.isEmpty())
        {
            GameObject _newBullet = _manager.GetBullet();
            _newBullet.GetComponent<Bullet>()._moveDir = transform.up;
            _newBullet.transform.position = top.transform.position;
            _newBullet.transform.rotation = transform.rotation;
        }
    }

    private void _Movement()
    {
        _rb.AddForce(_moveVec * _moveSpd * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.0f);
    }

}
