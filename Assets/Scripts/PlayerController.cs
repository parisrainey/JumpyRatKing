using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isPlayer1 = true;

    [Space]
    [SerializeField]
    private float _maxSpeed = 2;
    [SerializeField]
    private float _acceleration = 100;
    [SerializeField]
    private float _jumpHeight = 2;

    [Space]
    [SerializeField]
    private Vector3 _groundCheckPosition = new Vector3();
    [SerializeField]
    private float _groundCheckRadius = 0.45f;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private bool _jumpInput;
    private bool _isGrounded;

    private void Awake()
    {
        //Get rigidbody component
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Get movement input
        if(_isPlayer1)
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Player1Horizontal"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player1Jump") != 0;
        }
        else
        {
            Debug.Log("Player2");
            _moveDirection = new Vector3(Input.GetAxisRaw("Player2Horizontal"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player2Jump") != 0;
        }
    }

    private void FixedUpdate()
    {
        //Add movement force
        Vector3 force = _moveDirection * _acceleration * Time.fixedDeltaTime;
        _rigidbody.AddForce(force, ForceMode.VelocityChange);

        //Clamp velocity to max speed
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -_maxSpeed, +_maxSpeed);

        //Ground Check
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheckPosition, _groundCheckRadius).Length > 1;

        if(_jumpInput && _isGrounded)
        {
            //Calculate the force necessary to get jumpHeight unity units off the ground
            float jumpForce = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _groundCheckPosition, _groundCheckRadius);
    }
#endif
}
