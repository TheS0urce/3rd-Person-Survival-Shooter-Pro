﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //get handle to charactercontroller
    private CharacterController _controller;
    private Vector3 _direction;
    private Vector3 _velocity;

    [Header("Controller Settings")]
    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    [Header("Camera Settings")]
    [SerializeField]
    private float _cameraSensitivityX = 10.0f;
    [SerializeField]
    private float _cameraSensitivityY = 1.0f;

    private Camera _mainCamera;


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

        //lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CameraController();

        //hit ESC
        //Unlock cursor
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
            _velocity = transform.TransformDirection(_velocity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if jump ....velocity = new velocity.y
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;
        //controller.move(velocity * Time.deltatime)
        _controller.Move(_velocity * Time.deltaTime);
    }

    void CameraController()
    {
        //x mouse
        //y mouse

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //apply mouseX to player rotation y == look left/right
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);

        //look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivityY;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //apply mouseY to camera x rotation value.
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _cameraSensitivityX;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0.0f, 18.7f);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }
}
