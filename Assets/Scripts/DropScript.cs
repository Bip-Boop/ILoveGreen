using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private ObjectPooling _objectPooling;
    [SerializeField] private IdleOscillation _idleOscillation;

    [SerializeField] private AnimationCurve _scaleDownCurve;
    [SerializeField] private float _scaleDownSpeed;

    private AnimationCoroutines _animationCoroutines;

    private void Start()
    {
        _animationCoroutines = new AnimationCoroutines();
    }

    private bool _firstTime = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _firstTime && !_levelController.IsLevelEnded)
        {
            var choice = _objectPooling.GetFromPool("GreenParticles").GetComponent<ParticleSystem>();
            choice.gameObject.transform.position = transform.position;
            choice.Play();

            AudioManager.Instance.PlaySFX("Droplet");

            _firstTime = false;
            _levelController.DropsCollected++;

            if (_idleOscillation != null)
                _idleOscillation.IsIdling = false;

            if (_scaleDownSpeed != 0f)
                StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(transform, _scaleDownCurve, _scaleDownSpeed,
                    () => gameObject.SetActive(false)));
        }
    }
}
