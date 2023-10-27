using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement _playerMovement;

        [SerializeField]
        private CinemachineVirtualCamera _vCam;

        private float movementX;
        private float movementY;
        private Vector2 movementVector;
        
        private void OnMove(InputValue movementValue)
        {
            movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(movementVector);
        }
    }
}