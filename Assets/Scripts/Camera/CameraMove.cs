using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform _playerTrs;

    float _yAxis;
    float _xAxis;

    [SerializeField] float _camRotSpeed = 3f;
    [SerializeField] float _maxDist = 5f;
    [SerializeField] float _minDist = 1f;
    [SerializeField] float _distance = 5f;
    [SerializeField] float _rotMin = -10;
    [SerializeField] float _rotMax = 80;
    [SerializeField] float _smoothTime = 0.12f;

    [SerializeField] Transform _aimPos;
    [SerializeField] GameObject _aimImg;

    [SerializeField] Transform _mainCamTrs;

    
    private Vector3 _targetRot;
    private Vector3 _curVel;

    public bool _isAiming = false;
    bool _isAimingSpear = false;
    bool _isBow = false;

    Vector3 _camPos;
    Vector3 _camArmPos;

    public bool _isPassWall = false;

    [SerializeField] Vector3 _pointPos;


    Vector3 _rndPos;

    private void Update()
    {

        if (InGameUI._Instance._onInven)
            return;
        LookAround();

        PreventPassWall();

        //PreventPassGround();

        _yAxis = _yAxis + Input.GetAxis("Mouse X") * _camRotSpeed;
        _xAxis = _xAxis + Input.GetAxis("Mouse Y") * _camRotSpeed;

        _xAxis = Mathf.Clamp(_xAxis, _rotMin, _rotMax);

        _targetRot = Vector3.SmoothDamp(_targetRot, new Vector3(-_xAxis, _yAxis), ref _curVel, _smoothTime);
        _mainCamTrs.eulerAngles = _targetRot;
        _camPos = _playerTrs.position - (_mainCamTrs.forward * _distance);
        _camArmPos = _playerTrs.position - (_mainCamTrs.forward * _maxDist);
        if (!_isAiming)
        {
            _mainCamTrs.position = _camPos + _rndPos;
            transform.position = _camArmPos;
            _aimImg.SetActive(false);
        }
        else if(_isBow)
        {
            _mainCamTrs.position = _aimPos.position + _rndPos;
            _aimImg.SetActive(true);
        }
        //transform.position = _camPos;
        if(_isAimingSpear)
        {
            _mainCamTrs.position = _aimPos.position;
            _aimImg.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.J))
            TriggerCameraShake(0.7f, 0.1f);
    }

    
    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _camRotSpeed;
        Vector3 camAngle = _mainCamTrs.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 325f, 361f);
        }

        float xRot = _isAimingSpear ? 0f : x;
        Quaternion camRotation = Quaternion.Euler(xRot, camAngle.y + mouseDelta.x, camAngle.z);
        _mainCamTrs.rotation = camRotation;
        //_playerRot = Quaternion.Euler(0, camAngle.y + mouseDelta.x, camAngle.z);

    }
    public void SetIsAiming(bool isAiming)
    {
        _isAiming = isAiming;
    }
    public void SetIsAimingSpear(bool isAiming)
    {
        _isAimingSpear = isAiming;
    }
    public void SetIsBow(bool isBow)
    { _isBow = isBow;}
    void PreventPassWall()
    {
        Vector3 dir = transform.position - _playerTrs.position;
        Vector3 rayStartPos = _playerTrs.position - (dir.normalized * 1f);

        RaycastHit hitWall;
        RaycastHit hitGround;

        
        Physics.Raycast(_playerTrs.position, dir, out hitWall, _distance, LayerMask.GetMask("Wall"));
        Debug.DrawRay(_playerTrs.position, dir.normalized * hitWall.distance, Color.red);

        Physics.Raycast(_playerTrs.position, dir, out hitGround, _distance, LayerMask.GetMask("Ground"));
        if (hitWall.point != Vector3.zero)
        {
            _minDist = Vector3.Distance(_playerTrs.position, hitWall.point) + 0.1f;
            if (_minDist < 1f)
                _minDist = 1f;
            //SetDistance(_minDist);
            _distance = _minDist;
            _pointPos = hitWall.point;
        }
        else if(hitGround.point != Vector3.zero)
        {
            _minDist = Vector3.Distance(_playerTrs.position, hitGround.point) + 0.1f;
            if (_minDist < 1f)
                _minDist = 1f;
            //SetDistance(_minDist);
            _distance = _minDist;
            _pointPos = hitGround.point;
        }
        else
        {
            _pointPos = hitWall.point;
            _distance = _maxDist;
        }
    }
    
    public void TriggerCameraShake(float time, float amount)
    {
        StartCoroutine(CRT_CameraShake(time, amount));

    }
    IEnumerator CRT_CameraShake(float time, float amount)
    {
        Vector3 camPos = Camera.main.transform.position;

        float shakeTime = time;

        while(shakeTime > 0)
        {
            _rndPos = Random.insideUnitSphere * amount;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        shakeTime = 0;
        _rndPos = Vector3.zero;
    }
}