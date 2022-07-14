using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private ObjectPooling _objectPooling;
    [SerializeField] private CollectThemAll _collectThemAll;
    [SerializeField] private BreatheScript _breatheScript;

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

            if (_collectThemAll.ToCollect == 1)
                AudioManager.Instance.PlaySFX("StarChord", 0.5f);
            else
                AudioManager.Instance.PlaySFX("Star", 0.5f);

            _collectThemAll.ToCollect--;
            _firstTime = false;

            if (_breatheScript != null)
                _breatheScript.IsBreathing = false;

            if (_scaleDownSpeed != 0f)
                StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(transform, _scaleDownCurve, _scaleDownSpeed,
                    () => gameObject.SetActive(false)));
        }
    }
}
