using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = Vector3.zero;

    [SerializeField] public float _xSize;
    [SerializeField] public float _ySize;

    private float _xBound;
    private float _yBound;

    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _smoothing = 1.0f;

    private void Start()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);


        var vertical = Camera.main.orthographicSize;
        var horizontal = vertical * Screen.width / Screen.height;

        _xBound = _xSize / 2.0f - horizontal;
        _yBound = _ySize / 2.0f - vertical;
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(
            Mathf.Clamp(_target.position.x + _offset.x, -_xBound, _xBound),
             Mathf.Clamp(_target.position.y + _offset.y, -_yBound, _yBound), 
             transform.position.z);
        //Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
        transform.position = targetPos;
    }
}
