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
    private float _velocity;
    private bool _isGrounded = true;
    private float _horizontalInput;
    private Animator _animator;

    [Header("Collider for resize")]
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


    private void PlayerMovement()
    {

        /// Move player straigth forward and play run animation

        _animator.SetBool("isRun", true);
        transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime);
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _playerSideSpeed * _horizontalInput *
                                                   Time.deltaTime, Space.World);

        /// Touch control with finger
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


    private void Jump()
    {
        _velocity += _gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_isGrounded)
            {
                JumpLogic();

            }
        }
        if (SwipeManager.Instance.SwipeUp)
        {
            if (_isGrounded)
            {
                JumpLogic();

            }
        }

        /// Check for ground and prevent player for fall through floor
        if (transform.position.y < 0)
        {
            _isGrounded = true;
            _animator.ResetTrigger("Jump");
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (!_isGrounded)
        {
            transform.Translate(new Vector3(0, _velocity, 0) * Time.deltaTime);

        }
       


    }

    private void JumpLogic()
    {
        _velocity = _jumpForce;
        _isGrounded = false;
        _animator.SetTrigger("Jump");
        _animator.SetBool("isSlide", false);
    }

    private void Slide()
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

    private void Bounds()
    {
        
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

    private void TouchStart()
    {
        if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.W))
        {
            GameManager.Instance.CurrentState = GameManager.GameState.Playing;
        }
    }
    /// Call method from animation event
    private void StartSlide()
    {
        _playerCollider.size /= 3f; // Half collider for slide
    }
    /// Call method from animation event
    private void EndSlide()
    {
        _playerCollider.size = new Vector3(_colScaleX, _colScaleY, _colScaleZ);
        _animator.SetBool("isSlide", false);
    }


    private void OnTriggerEnter(Collider other)
    {
        
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

            case "Obstacle":
                GameManager.Instance.CurrentState = GameManager.GameState.Dead;
                this.gameObject.SetActive(false);
                break;
        }
    }


    

}
