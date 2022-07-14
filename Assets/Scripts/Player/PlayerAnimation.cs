using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private ObjectPooling _objectPooling;

    [System.Serializable] public struct AnimControl
    {
        public AnimationCurve curve;
        public float speed;
        public bool playerAffected;
    }

    [SerializeField] private AnimControl _scalingForGreen;
    [SerializeField] private AnimControl _scalingForGrey;

    [SerializeField] private AnimControl _hueForGreen;

    [SerializeField] private AnimControl _colorTransitionForGrey;
    [SerializeField] private Color _transitionGreyColor;

    [SerializeField] private AnimControl _hueForWin;
    [SerializeField] private AnimControl _scalingForWin;

    [SerializeField] private Transform _player;

    private AnimationCoroutines _animationCoroutines;

    void Start()
    {
        _animationCoroutines = new AnimationCoroutines();
        _playerBehaviour.PlayerTouchedGreen += PlayerTouchGreenAnimation;
        _playerBehaviour.PlayerTouchedGrey += PlayerTouchGreyAnimation;
    }

    public void PlayerWonAnimation()
    {
        //Particles
        var choice = _objectPooling.GetFromPool("GreenParticles").GetComponent<ParticleSystem>();
        choice.gameObject.transform.position = _player.position;
        choice.Play();

        //Scaling
        if (!(_scalingForWin.playerAffected || _scalingForGrey.playerAffected))
        {
            _scalingForWin.playerAffected = true;
            StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(_player,
                _scalingForWin.curve, _scalingForWin.speed, () => _scalingForWin.playerAffected = false));
        }

        if (!_hueForWin.playerAffected)
        {
            _hueForWin.playerAffected = true;
            StartCoroutine(_animationCoroutines.ColorHueCoroutine(_player.GetComponent<SpriteRenderer>(),
                _hueForWin.curve, _hueForWin.speed, () => _hueForWin.playerAffected = false));
        }
    }

    private void PlayerTouchGreenAnimation()
    {
        //Particles
        var choice = _objectPooling.GetFromPool("GreenParticles").GetComponent<ParticleSystem>();
        choice.gameObject.transform.position = _player.position;
        choice.Play();

        //Scaling
        if (!(_scalingForGreen.playerAffected || _scalingForGrey.playerAffected))
        {
            _scalingForGreen.playerAffected = true;
            StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(_player,
                _scalingForGreen.curve, _scalingForGreen.speed, () => _scalingForGreen.playerAffected = false));
        }

        if (!_hueForGreen.playerAffected)
        {
            _hueForGreen.playerAffected = true;
            StartCoroutine(_animationCoroutines.ColorHueCoroutine(_player.GetComponent<SpriteRenderer>(),
                _hueForGreen.curve, _hueForGreen.speed, () => _hueForGreen.playerAffected = false));
        }
    }

    private void PlayerTouchGreyAnimation()
    {
        var choice = _objectPooling.GetFromPool("GreyParticles").GetComponent<ParticleSystem>();
        choice.gameObject.transform.position = _player.position;
        choice.Play();

        if (!_colorTransitionForGrey.playerAffected)
        {
            _colorTransitionForGrey.playerAffected = true;
            StartCoroutine(_animationCoroutines.SmoothColorTransition(_player.GetComponent<SpriteRenderer>(),
                _transitionGreyColor, _colorTransitionForGrey.curve, _colorTransitionForGrey.speed));
        }

        if (!_scalingForGrey.playerAffected)
        {
            _scalingForGrey.playerAffected = true;
            StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(_player,
                _scalingForGrey.curve, _scalingForGrey.speed, () => _scalingForGrey.playerAffected = false));
        }
    }
}
