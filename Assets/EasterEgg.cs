using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EasterEgg : MonoBehaviour
{
    #region Move
    [Header("Movement")]
    [SerializeField] private bool _moveForward = false;
    [ConditionalHide("_moveForward", true)]
    [SerializeField] private float _moveSpeed = 1.0f;
    [ConditionalHide("_moveForward", true)]
    [SerializeField] private bool _ignoreRotation = false; 
    [ConditionalHide("_moveForward", true)]
    [SerializeField] private bool _moveOverTime = false;
    [ConditionalHide("_moveOverTime", true)]
    [SerializeField] private float _increaseMoveSpeed = 1.0f;
    #endregion

    #region Rotate
    [Header("Rotation")]
    [SerializeField] private bool _rotate = false;
    [ConditionalHide("_rotate", true)]
    [SerializeField]private float _rotateSpeed = 100.0f;
    [ConditionalHide("_rotate", true)]
    [SerializeField] private bool _rotateLeft = false;
    [ConditionalHide("_rotate", true)]
    [SerializeField] private bool _rotationOverTime = false;
    [ConditionalHide("_rotationOverTime", true)]
    [SerializeField] private float _increaseRotationSpeed = 1.0f;
    private Vector3 _rotationDir;
    #endregion
    
    #region Shrink
    [Header("Shrinking")]
    [SerializeField] private bool _shrink = false; 
    [ConditionalHide("_shrink", true)]
    [SerializeField] private float _shrinkSpeed = .1f;
    [ConditionalHide("_shrink", true)]
    [SerializeField] private bool _stopAtZero = true;
    [ConditionalHide("_shrink", true)]
    [SerializeField] private bool _shrinkOverTime = false;
    [ConditionalHide("_shrinkOverTime", true)]
    [SerializeField] private float _increaseShrinkSpeed = .2f;
    #endregion

    private void Awake()
    {
        if (_moveForward) UpdateBehaviour += MoveForward;
        if (_rotate)
        {
            _rotationDir = _rotateLeft ? Vector3.forward : -Vector3.forward;
            UpdateBehaviour += Rotate;
        } 
        if (_shrink) UpdateBehaviour += Shrink;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour?.Invoke();
    }

    private void MoveForward()
    {
        Vector3 dir = _ignoreRotation ? Vector3.right : transform.right;
        Vector3 newPos = transform.position + (dir * _moveSpeed * Time.deltaTime);
        transform.position = newPos;
        if (_moveOverTime) _moveSpeed += Time.deltaTime * _increaseMoveSpeed;
    }

    private void Rotate()
    {
        transform.Rotate(_rotationDir * (_rotateSpeed * Time.deltaTime ));
        if (_rotationOverTime) _rotateSpeed += Time.deltaTime * _increaseRotationSpeed;
    }

    private void Shrink()
    {
        // TODO: Shrink
        Vector3 newScale = transform.localScale - (Vector3.one * Time.deltaTime * _shrinkSpeed);

        if (newScale.x < 0)  newScale = Vector3.zero;

        transform.localScale = newScale;

        if (_shrinkOverTime) _shrinkSpeed += Time.deltaTime * _increaseShrinkSpeed;
    }

    private Action UpdateBehaviour;
}
