using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovementMP : MonoBehaviour
{
    private CharacterController _controller;
    PhotonView view;
    Animator _animator;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;
    private float _rotationX;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    [SerializeField]
    private float _gravityValue = -9.81f;

    [SerializeField]
    private Camera _followCamera;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        if (!view.IsMine)
        {
            Destroy(_followCamera);
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementInput = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
            Vector3 movementDirection = movementInput.normalized;

            _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

            // Rotate view through mouse movement
            float mouseX = Input.GetAxis("Mouse X") * 0.5f;
            _rotationX += mouseX;
            transform.Rotate(Vector3.up, _rotationX * _rotationSpeed * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
                _animator.SetBool("Walk", true);
            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                _animator.SetBool("Walk", false);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);

        }
    }


}
