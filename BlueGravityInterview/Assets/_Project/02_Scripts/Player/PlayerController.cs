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
        private PlayerInteraction _playerInteraction;

        [SerializeField]
        private PlayerAnimation _playerAnimation;
        
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

        private void OnInteract()
        {
            _playerInteraction.Interact();
        }
        
        private void FixedUpdate()
        {
            _playerMovement.Move(movementVector);
            _playerInteraction.CheckInteractions();
            
            _playerAnimation.Move(movementVector);
        }
    }
}