using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private Transform _visual;

    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 _originalSize;

    private void Awake()
    {
        _originalSize = _visual.localScale;
    }

    public void Move(Vector2 moveDirection)
    {
        if (moveDirection.x != 0)
        {
            float sizeX = moveDirection.x < 0 ? -_originalSize.x : _originalSize.x;
            _visual.localScale = new Vector3(sizeX,_originalSize.y,_originalSize.z);
        }
        
        _anim.SetFloat(Speed, moveDirection.magnitude);
    }
}