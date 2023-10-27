using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _smoothTime =0.5f;

        private Vector2 _currentVelocity;

        private bool _canMove = true;
    
        private Transform _trs;

        private void Awake()
        {
            _trs = transform;
        }

        public void Move(Vector2 direction)
        {
            if(!_canMove) return;
            
            Vector2 targetPosition = _trs.TransformPoint(direction);
            var position = _trs.position;
            Vector2 pos = Vector2.SmoothDamp(position, targetPosition, ref _currentVelocity, _smoothTime * Time.deltaTime);
            _rb.velocity = (pos - (Vector2)position) / Time.deltaTime;
        }
    }
}