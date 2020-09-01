using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //get handle to charactercontroller
    private CharacterController _controller;
    private Vector3 _direction;
    private Vector3 _velocity;

    private Camera _mainCamera;

    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if(_controller == null)
        {
            Debug.LogError("CharacterController is NULL.");
        }

        _mainCamera = Camera.main;

        if(_mainCamera == null)
        {
            Debug.LogError("_mainCamera is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //x mouse
        //y mouse

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //apply mouseX to player rotation y == look left/right
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);

        //look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //apply mouseY to camera x rotation value.
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    void CalculateMovement()
    {
        //wsad keys for movement
        //input system (horizontal and vertical input)
        //direction = vector to move
        //velocity = direction * speed

        if (_controller.isGrounded == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontalInput, 0, verticalInput);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        //if jump ....velocity = new velocity.y

        //controller.move(velocity * Time.deltatime)

        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
