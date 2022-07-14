using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWhenInteract : MonoBehaviour
{
    private AnimationCoroutines _animationCoroutines;

    [SerializeField] AnimationCurve _scaleCurve;
    [SerializeField] float _speed;

    [SerializeField] Transform _visualSprite;

    private bool _isBusy;

    private void Start()
    {
        _animationCoroutines = new AnimationCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_isBusy)
        {
            _isBusy = true;
            StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(_visualSprite, _scaleCurve, _speed,
                () => _isBusy = false));
        }
    }
}
