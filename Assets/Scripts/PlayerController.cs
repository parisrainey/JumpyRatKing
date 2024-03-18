using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10000), Tooltip("Player speed")]
     private float _speed = 50;

    [SerializeField, Range(0, 2500), Tooltip("How much vertical force applied when jumping")]
    private float _jumpForce = 1000;

    private Rigidbody _rigidbody;
    private bool _isGrounded = false;

    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(0, value);
    }

    private void Awake()
    {
        //Get reference to rigidbody
        _rigidbody = GetComponent<Rigidbody>();
        //Checks if statement is true, if not break program and return message
        Debug.Assert(_rigidbody != null, "Rigidbody is null!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float jumpInput = 0;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            jumpInput = 1;
            _isGrounded = false;
        }
        _rigidbody.AddForce(Vector3.right * moveInput * _speed * Time.deltaTime);
        _rigidbody.AddForce(Vector3.up * jumpInput * _speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

}
