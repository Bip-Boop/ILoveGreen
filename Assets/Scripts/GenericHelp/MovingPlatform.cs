using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatform : MonoBehaviour
{
    [Serializable]
    struct Motion
    {
        public Vector3 moveTo;
        public AnimationCurve animationCurve;
        public float speed;
    }

    [Serializable]
    struct Rotation
    {
        public float deg;
        public AnimationCurve animationCurve;
        public float speed;
    }

    [Serializable]
    struct Resization
    {
        public AnimationCurve animationCurve;
        public float speed;
    }

    [Serializable]
    struct Move
    {
        public float predelay;
        public Motion motion;
        public Rotation rotation;
        public Resization resization;
    }


    [SerializeField] private Move[] _moves;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _moveOnStart;

    private AnimationCoroutines _animationCoroutines;

    private void Start()
    {
        _animationCoroutines = new AnimationCoroutines();

        if (_moveOnStart)
            StartCoroutine(ActionEvaluation());
    }

    private IEnumerator ActionEvaluation ()
    {
        do
        {
            foreach (var move in _moves)
            {
                bool motionEnded = false;
                bool rotationEnded = false;
                bool resizationEnded = false;
                yield return new WaitForSeconds(move.predelay);

                if (move.motion.speed != 0)
                StartCoroutine(_animationCoroutines.SmoothMoveCoroutine(
                    transform, move.motion.moveTo, move.motion.animationCurve, move.motion.speed,
                    () => motionEnded = true));

                if (move.rotation.speed != 0)
                    StartCoroutine(_animationCoroutines.SmoothRotateAroundZCoroutine(
                        transform, move.rotation.deg, move.rotation.animationCurve, move.rotation.speed,
                        () => rotationEnded = true));

                if (move.resization.speed != 0)
                    StartCoroutine(_animationCoroutines.ScaleAnimationCoroutine(
                        transform, move.resization.animationCurve, move.rotation.speed,
                        () => resizationEnded = true));



                yield return new WaitUntil(() => motionEnded && rotationEnded && resizationEnded);
            }
        }
        while (_loop);
    }

}
