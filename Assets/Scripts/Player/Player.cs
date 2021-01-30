using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player variables
    [Header("Player Variables")]
    [SerializeField] private float moveSpd;
    [SerializeField] private Rigidbody2D rb;

    //other variables
    private Vector2 moveVec = new Vector2(0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement(); 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>().normalized * 0.5f;
        if (moveVec != Vector2.zero)
        {
            float angle = Mathf.Atan2(-moveVec.x, moveVec.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //Debug.Log(moveVec);

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        
    }

    private void Movement()
    {
        rb.AddForce(moveVec * moveSpd, ForceMode2D.Impulse);
    }

}
