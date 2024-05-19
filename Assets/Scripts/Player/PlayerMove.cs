using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _charControl;
    GroundChecker _groundCheck;
    PlayerAttack _playerAttack;


    [SerializeField] float _moveSpeed = 5;

    [SerializeField] float _gravity = 10;
    public float _jumpPower = 5;


    [SerializeField] float _finalDashDistance = 1;
    [SerializeField] float _finalDashSpeed = 5;

    [Header("대쉬 거리"), SerializeField] float _dashDistance = 1;
    [Header("대쉬 스피드"), SerializeField] float _dashSpeed = 10;

    [SerializeField] InputController _inputController;
    //Anim_OneHandSword _animEvent;

    [SerializeField] CameraMove _camMove;

    [SerializeField] SoundPlayer _soundPlayer;
    float _inputH;
    float _inputV;
    public Vector3 _moveDir;

    


    Vector3 _camForward;
    private void Awake()
    {
        _charControl = GetComponent<CharacterController>();
        _groundCheck = GetComponent<GroundChecker>();
        //_animEvent = GetComponentInChildren<Anim_OneHandSword>();

        _playerAttack = GetComponentInChildren<PlayerAttack>();
    }
    private void Update()
    {
        if (!_playerAttack._isAttack)
            Move();
        if(!_groundCheck.IsGrounded())
            ApplyGravity();
        FinalAttackDash();

        if (_camMove._isAiming)
        {
            _camForward = Camera.main.transform.forward;
            transform.rotation = Quaternion.LookRotation(_camForward);
        }
        else if(_moveDir.x != 0 || _moveDir.x != 0)
            Rotate();

        Dash();

    }

    private void Move()
    {
        _moveSpeed = _inputController.GetSpeed();
        if (_groundCheck.IsGrounded() && !_inputController._IsDash)
        {


            _camForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

            _moveDir = _inputController._InputDir.x * new Vector3(_camForward.z, 0, -_camForward.x) +
                _inputController._InputDir.z * _camForward;
            _moveDir.Normalize();
            _moveDir *= _moveSpeed;




            if (Input.GetKeyDown(KeyCode.Space))
                _moveDir.y = _jumpPower;

            
        }
        if (_camMove._isAiming)
            _moveDir = Vector3.zero;
        _charControl.Move(_moveDir * Time.deltaTime);

        if (_moveSpeed != 0 && _groundCheck.IsGrounded() && !_soundPlayer._isPlaying)
            _soundPlayer.PlayWalk(true);

        if(_moveSpeed == 0 || !_groundCheck.IsGrounded() || _inputController._IsDash)
            _soundPlayer.PlayWalk(false);
    }
    void Rotate()
    {
        if (_moveDir != Vector3.zero)
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(_moveDir.x, _moveDir.z) * Mathf.Rad2Deg, 0);
    }
    void FinalAttackDash()
    {
        if (_playerAttack._isFinalAttack)
        {
            Vector3 targetPos = transform.forward * _finalDashDistance;
            //_playerTrs.position = Vector3.MoveTowards(_playerTrs.position, _playerTrs.position + targetPos, Time.deltaTime * _dashSpeed);
            _charControl.Move(targetPos * _finalDashSpeed * Time.deltaTime);
        }

    }
    void Dash()
    {
        if(_inputController._IsDash)
        {
            if(_groundCheck.IsGrounded() && !_playerAttack._isAttack)
            {
                Vector3 targetPos = transform.forward * _dashDistance;
                _charControl.Move(targetPos * _finalDashSpeed * Time.deltaTime);
            }
            
        }
    }
    public void SetSpeedAndJump(float speed, float jumpPower)
    {
        _moveSpeed += (_moveSpeed * speed / 100);
        _jumpPower += (_jumpPower * jumpPower / 200);
    }
    private void ApplyGravity() { _moveDir.y -= _gravity * Time.deltaTime; }
}