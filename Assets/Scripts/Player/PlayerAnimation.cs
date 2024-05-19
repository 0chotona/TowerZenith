using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    static PlayerAnimation _instance;

    public static PlayerAnimation Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerAnimation>();
            return _instance;
        }

    }
    Animator _anim;
    float _comboDurTime = 0.5f;

    [SerializeField] InputController _inputController;
    [SerializeField] GroundChecker _groundChecker;

    [SerializeField] SoundPlayer _soundPlayer;
    [SerializeField] AudioSource _voiceAudio;
    public int _activeIndex = 0;
    public int _curCombo = 0;
    public int _inputCombo = 0;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MoveAnimation();
        JumpAnimation();
        DashAnimation();
    }
    void MoveAnimation()
    {
        _anim.SetFloat("Speed", _inputController.GetSpeed());
    }
    void JumpAnimation()
    {
        _anim.SetBool("IsGrounded", _groundChecker.IsGrounded()); 

    }
    void DashAnimation()
    {
        _anim.SetBool("IsDash", _inputController._IsDash);
    }
    public void SetWeaphonAnim(int index)
    {
        _anim.SetInteger("WeaphonIndex", index);
    }
    public void PlaySpirit()
    {
        _voiceAudio.Play();
    }
    
}
