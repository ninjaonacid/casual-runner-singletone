using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] private float _playerSpeed = 1;
        [SerializeField] private float _jumpForce = 5;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _touchSpeedModifier = 0.1f;
        private float _velocity;
        private bool _isGrounded = true;

        private IInputService _input;
        private Animator _animator;

        [Header("Collider for resize")]
        private BoxCollider _playerCollider;
        private float _colScaleX = 1;
        private float _colScaleY = 1.65f;
        private float _colScaleZ = 0.5f;


        void Start()
        {
            _input = Game.InputService;
            _animator = gameObject.GetComponent<Animator>();
            _playerCollider = gameObject.GetComponent<BoxCollider>();
            _playerCollider.size = new Vector3(_colScaleX, _colScaleY, _colScaleZ);

        }

        void Update()
        {
  
            Bounds();

            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
            {
                PlayerMovement();
            }
            
            if (GameManager.Instance.CurrentState == GameManager.GameState.StartGame)
            {
                PressStart();
            }

        }

        private void PlayerMovement()
        {
            MoveForward();
            MoveHorizontal();
            JumpGravity();


            if (_input.JumpButtonPressed() && _isGrounded)
            {
                _velocity = _jumpForce;
                _isGrounded = false;
                _animator.SetTrigger("Jump");
                _animator.SetBool("isSlide", false);

            }
            if (!_isGrounded)
            {
                transform.Translate(new Vector3(0, _velocity, 0) * Time.deltaTime);
            }

            if (_input.SlideButtonPressed())
            {
                _animator.SetBool("isSlide", true);
            }

        }


        private void JumpGravity() =>
                _velocity += _gravity * Time.deltaTime;
        

        private void MoveHorizontal() =>
            _input.Move(this.transform, _touchSpeedModifier);
        

        private void MoveForward()
        {
            _animator.SetBool("isRun", true);
            transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime);
        }

        private void Bounds()
        {

            if (transform.position.x > LevelBounds.xBoundRight)
            {
                transform.position = new Vector3(LevelBounds.xBoundRight,
                                                    transform.position.y,
                                                    transform.position.z);
            }

            if (transform.position.x < LevelBounds.xBoundLeft)
            {
                transform.position = new Vector3(LevelBounds.xBoundLeft,
                                                    transform.position.y,
                                                    transform.position.z);
            }

            if(transform.position.y < LevelBounds.yBoundBottom)
            {
                _isGrounded = true;
                _animator.ResetTrigger("Jump");
                transform.position = new Vector3(transform.position.x,
                                                LevelBounds.yBoundBottom,
                                                  transform.position.z);
            }
        }

        private void PressStart()
        {
            if (_input.StartButtonPressed())
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
                    break;


                case "Finish":
                    GameManager.Instance.CurrentState = GameManager.GameState.Finish;
                    _animator.SetBool("isRun", false);
                    break;

                case "Obstacle":
                    GameManager.Instance.CurrentState = GameManager.GameState.Dead;
                    gameObject.SetActive(false);
                    break;
            }
        }




    }
}