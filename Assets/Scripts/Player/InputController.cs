using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    float _inputH;
    float _inputV;

    Vector3 _inputDir;
    public Vector3 _InputDir => _inputDir;

    bool _isDash = false;
    public bool _IsDash => _isDash;

    public float _maxSpeed = 6f;
    float _speed;
    void Update()
    {
        _inputH = Input.GetAxis("Horizontal");
        _inputV = Input.GetAxis("Vertical");
        _inputDir = new Vector3(_inputH, 0, _inputV);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(CRT_Dash());
        }
    }
    public float GetSpeed()
    {
        if (_inputDir != Vector3.zero)
            _speed = Mathf.Lerp(_speed, _maxSpeed, Time.deltaTime * 4);
        else
            _speed = 0;

        return _speed;
    }
    IEnumerator CRT_Dash()
    {
        _isDash = true;
        yield return new WaitForSeconds(0.5f);
        _isDash = false;
    }
}
