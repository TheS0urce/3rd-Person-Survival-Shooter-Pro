using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //reference to the charactercontroller
    private CharacterController _controller;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Transform _player;

    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private float _gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if(_controller == null)
        {
            Debug.LogError("_controller is NULL.");
        }

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if(_player == null)
        {
            Debug.LogError("_player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if grounded
        if(_controller.isGrounded == true)
        {
            //calculate direction = destination (player or target) - source(this.transform)
            //calculate velocity = direction * speed
            transform.LookAt(_player);
            _direction = _player.position - this.transform.position;
            _direction.Normalize();
            _velocity = _direction * _speed;
        }

        //subtract gravity
        _velocity.y -= _gravity;

        //move to velocity
        _controller.Move(_velocity * Time.deltaTime);
    }
}
