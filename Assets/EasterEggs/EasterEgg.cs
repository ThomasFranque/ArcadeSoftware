using System;
using System.Collections;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] private bool _hasLifetime = false;
    [ConditionalHide("_hasLifetime", false)]
    [SerializeField] private float _lifetime = 12.0f;
    [Tooltip("Will randomize everything if box is unchecked.")]
    [SerializeField] private bool _customizeValues = true;

    #region Move
    [Header("---MOVING---")]
    [ConditionalHide("_customizeValues", false)]
    [SerializeField] private bool _moveForward = false;
    [ConditionalHide("_moveForward", false)]
    [SerializeField] private float _moveSpeed = 1.0f;
    [ConditionalHide("_moveForward", false)]
    [SerializeField] private bool _ignoreRotation = false;
    [ConditionalHide("_moveForward", false)]
    [SerializeField] private bool _moveOverTime = false;
    [ConditionalHide("_moveOverTime", false)]
    [SerializeField] private float _increaseMoveSpeed = 1.0f;
    #endregion

    #region Rotate
    [Header("---ROTATING---")]
    [ConditionalHide("_customizeValues", false)]
    [SerializeField] private bool _rotate = false;
    [ConditionalHide("_rotate", false)]
    [SerializeField] private float _rotateSpeed = 100.0f;
    [ConditionalHide("_rotate", false)]
    [SerializeField] private bool _rotateLeft = false;
    [ConditionalHide("_rotate", false)]
    [SerializeField] private bool _rotationOverTime = false;
    [ConditionalHide("_rotationOverTime", false)]
    [SerializeField] private float _increaseRotationSpeed = 1.0f;
    private Vector3 _rotationDir;
    #endregion

    #region Shrink
    [Header("---SHRINKING---")]
    [ConditionalHide("_customizeValues", false)]
    [SerializeField] private bool _shrink = false;
    [ConditionalHide("_shrink", false)]
    [SerializeField] private float _shrinkSpeed = .1f;
    [ConditionalHide("_shrink", false)]
    [SerializeField] private bool _stopAtZero = true;
    [ConditionalHide("_shrink", false)]
    [SerializeField] private bool _shrinkOverTime = false;
    [ConditionalHide("_shrinkOverTime", false)]
    [SerializeField] private float _increaseShrinkSpeed = .2f;
    #endregion

    private bool FiftyFiftyChance => UnityEngine.Random.Range(0.0f, 1.1f) >= 0.5 ? true : false;

    private void Awake()
    {
        if (!_customizeValues)
            Randomize();

        if (_moveForward) UpdateBehaviour += MoveForward;
        if (_rotate)
        {
            _rotationDir = _rotateLeft ? Vector3.forward : -Vector3.forward;
            UpdateBehaviour += Rotate;
        }
        if (_shrink) UpdateBehaviour += Shrink;

        if (_hasLifetime)
            StartCoroutine(CDestroyObj());
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
        transform.Rotate(_rotationDir * (_rotateSpeed * Time.deltaTime));
        if (_rotationOverTime) _rotateSpeed += Time.deltaTime * _increaseRotationSpeed;
    }

    private void Shrink()
    {
        // TODO: Shrink
        Vector3 newScale = transform.localScale - (Vector3.one * Time.deltaTime * _shrinkSpeed);

        if (newScale.x < 0) newScale = Vector3.zero;

        transform.localScale = newScale;

        if (_shrinkOverTime) _shrinkSpeed += Time.deltaTime * _increaseShrinkSpeed;
    }

    private void Randomize()
    {
        _moveForward = FiftyFiftyChance;
        _ignoreRotation = FiftyFiftyChance;
        _rotate = FiftyFiftyChance;
        _shrink = FiftyFiftyChance;

        _moveSpeed = UnityEngine.Random.Range(0.5f, 5.0f);
        _rotateSpeed = UnityEngine.Random.Range(50.0f, 200.0f);
        _shrinkSpeed = UnityEngine.Random.Range(0.01f, 0.5f);
    }

    private IEnumerator CDestroyObj()
    {
        yield return new WaitForSeconds(_lifetime);
        OnDeath();
        Destroy(gameObject);
    }

    protected void AddActionOnDeath(Action a)
    {
        Death += a;
    }

    private void OnDeath()
    {
        Death?.Invoke();
    }

    private Action UpdateBehaviour;
    private Action Death;

}
