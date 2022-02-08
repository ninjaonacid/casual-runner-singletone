using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [Header("Set in inspector")]
  [SerializeField] private float _playerSpeed = 1;
  [SerializeField] private float _playerSideSpeed = 30;
  [SerializeField] private float _jumpForce = 5;
  [SerializeField] private float _gravity = -9.81f;
  [SerializeField] private float _touchSpeedModifier = 0.1f;
    private Touch _touch;
    float velocity;
    private bool _isGrounded = true;
    private float _horizontalInput;
    private Animator _animator;
    private BoxCollider _playerCollider;
    private float _colScaleX = 1;
    private float _colScaleY = 1.65f;
    private float _colScaleZ = 0.5f;





    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerCollider = gameObject.GetComponent<BoxCollider>();
        _playerCollider.size = new Vector3(_colScaleX, _colScaleY, _colScaleZ);
        
        GameManager.Instance.CurrentState = GameManager.GameState.StartLevel;
        
    }


    void  Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            PlayerMovement();
            Jump();
            Slide();
        }
        Bounds();
        if (GameManager.Instance.CurrentState == GameManager.GameState.StartLevel)
        {
            TouchStart();
        }

    }


    void PlayerMovement()
    {

        // Включаем анимацию бега и двигаем персонажа все время прямо

        _animator.SetBool("isRun", true);
        transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime);
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _playerSideSpeed * _horizontalInput *
                                                   Time.deltaTime, Space.World);

        if(Input.touchCount > 0)
        {
            
            _touch = Input.GetTouch(0);
            if(_touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + _touch.deltaPosition.x *
                    _touchSpeedModifier, transform.position.y, transform.position.z);
            }
        }

    }



    void Jump()
    {
        velocity += _gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_isGrounded)
            {
                velocity = _jumpForce;
                _isGrounded = false;
                _animator.SetTrigger("Jump");
                _animator.SetBool("isSlide", false);

            }
        }
        if (SwipeManager.Instance.SwipeUp)
        {
            if (_isGrounded)
            {
                velocity = _jumpForce;
                _isGrounded = false;
                _animator.SetTrigger("Jump");
                _animator.SetBool("isSlide", false);

            }
        }

        // Проверяем на земле персонаж или нет, и фиксируем его позицию по оси y;
        // Не даем персонажу провалиться вниз
        if (transform.position.y < 0)
        {
            _isGrounded = true;
            _animator.ResetTrigger("Jump");
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (!_isGrounded)
        {
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        }
       


    }

    void Slide()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {

            _animator.SetBool("isSlide", true);


        }

        if (SwipeManager.Instance.SwipeDown)
        {
            _animator.SetBool("isSlide", true);

            Debug.Log("TAAAAAAAAP");
        }

    }

    void Bounds()
    {
        // Не даем персонажу выйти за пределы обозначеных границ                  
        if (this.transform.position.x > LevelBounds.xBoundRight)
        {
            transform.position = new Vector3(LevelBounds.xBoundRight,
                                                transform.position.y,
                                                transform.position.z);
        }

        if (this.transform.position.x < LevelBounds.xBoundLeft)
        {
            transform.position = new Vector3(LevelBounds.xBoundLeft,
                                                transform.position.y,
                                                transform.position.z);
        }
    }

    void TouchStart()
    {
        if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.W))
        {
            GameManager.Instance.CurrentState = GameManager.GameState.Playing;
        }
    }
    // Вызываю из animation event
    void StartSlide()
    {
        _playerCollider.size /= 3f; // уменьшаю коллайдер для прохождения под препятствием
    }
    // Вызываю из animation event
    void EndSlide()
    {
        _playerCollider.size = new Vector3(_colScaleX, _colScaleY, _colScaleZ);
        _animator.SetBool("isSlide", false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.CurrentState = GameManager.GameState.Dead;
            this.gameObject.SetActive(false); 
        }

        switch (other.gameObject.tag)
        {
            case "Pickup":
                Destroy(other.gameObject);
                ScoreManager.Instance.AddToScore();
                break;


            case "Finish":
                GameManager.Instance.CurrentState = GameManager.GameState.Finish;
                _animator.SetBool("isRun", false);
                break;
        }
    }




}
