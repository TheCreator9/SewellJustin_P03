using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMovement : MonoBehaviour
{

    public CharacterController _controller;

    public float _speed;
    public float _gravity = -9.81f;
    public float _jumpHeight;

    public float _turnTime = 0.1f;
    float _turnVelocity;

    Vector3 _velocity;

    private bool _isRunning;

    public bool _grounded = false;
    public float _groundCheckDistance;
    private float _groundCheckBuffer = 0.1f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _speed *= 2;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, _turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _controller.Move(direction * _speed * Time.deltaTime);
            _velocity.y += _gravity * Time.deltaTime;

        }

        groundCheck();

        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        }

    }

    public void groundCheck ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5f))
        {
            _grounded = true;
            Debug.Log(_grounded);
        }

        else
        {
            _grounded = false;
            Debug.Log(_grounded);
        }
    }
}
