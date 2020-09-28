using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    //reference to the charactercontroller
    private CharacterController _controller;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Transform _player;
    private Health _playerHealth;
    private float _nextAttack = -1;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;
    [SerializeField]
    private float _attackDelay = 1.5f;
    
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private float _gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();

        if(_controller == null)
        {
            Debug.LogError("_controller is NULL.");
        }

        if(_player == null)
        {
            Debug.LogError("_player is NULL.");
        }

        if(_playerHealth == null)
        {
            Debug.LogError("Player components are NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Chase:
                FollowPlayer();
                break;
        }
    }

    void FollowPlayer()
    {
        //check if grounded
        if (_controller.isGrounded == true)
        {
            //calculate direction = destination (player or target) - source(this.transform)
            //calculate velocity = direction * speed
            //transform.LookAt(_player);
            _direction = _player.position - this.transform.position;
            _direction.Normalize();
            _velocity = _direction * _speed;

            Quaternion rotation = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = rotation;
        }

        //subtract gravity
        _velocity.y -= _gravity;

        //move to velocity
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                //attack player
                _playerHealth.Damage(10);
            }

            //cooldown
            _nextAttack = Time.time + _attackDelay;
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }
}
