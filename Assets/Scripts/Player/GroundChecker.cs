using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://velog.io/@nagi0101/Unity-%EC%99%84%EB%B2%BD%ED%95%9C-CharacterController-%EA%B8%B0%EB%B0%98-Player%EB%A5%BC-%EC%9C%84%ED%95%B4
 * */
public class GroundChecker : MonoBehaviour
{
    [SerializeField] Vector3 _boxSize;
    [SerializeField] float _maxDistance;
    [SerializeField] LayerMask _groundLayer;

    private void OnDrawGizmos()
    {
        Vector3 boxCenter = transform.position;
        Quaternion boxRotation = transform.rotation;

        /*
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(boxCenter, _boxSize * 2); 
        */
        bool isGrounded = Physics.BoxCast(transform.position, _boxSize, -transform.up, transform.rotation, _maxDistance, _groundLayer);

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(boxCenter - Vector3.up * _maxDistance, _boxSize * 2);
    }
    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, _boxSize, -transform.up, transform.rotation, _maxDistance, _groundLayer);
    }
}
