using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Currency;
using UnityEngine;
using UnityEngine.InputSystem;
using InventorySystem;
using Items;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance => _instance;
        public Inventory Inventory => _inventory;

        private static PlayerController _instance;
        
        [SerializeField]
        private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerInteraction _playerInteraction;

        [SerializeField]
        private PlayerAnimation _playerAnimation;

        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private CurrencyController _currencyController;
        
        [SerializeField]
        private CinemachineVirtualCamera _vCam;

        private Vector2 movementVector;
        private float movementX;
        private float movementY;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

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

        public void AddItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Common:
                    _inventory.AddItem(item);
                    break;
                case ItemType.Currency:
                    _currencyController.AddCurrency(item.Amount);
                    break;
            }
        }
    }
}