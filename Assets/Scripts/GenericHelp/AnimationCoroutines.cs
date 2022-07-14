using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationCoroutines
{
    //Changes (multiplies) scale of target according to the curve
    public IEnumerator ScaleAnimationCoroutine(Transform target, AnimationCurve curve, float speed)
    {
        Vector3 start = target.localScale;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            target.localScale = curve.Evaluate(t) * start;
            yield return 0;
        }

        target.localScale = start * curve.Evaluate(1f);
    }

    public IEnumerator ScaleAnimationCoroutine(Transform target, AnimationCurve curve, float speed, Action end)
    {
        Vector3 start = target.localScale;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            target.localScale = curve.Evaluate(t) * start;
            yield return 0;
        }

        target.localScale = start * curve.Evaluate(1f);
        end();
    }

    //Moves object smoothly
    public IEnumerator SmoothMoveCoroutine(Transform target, Vector3 moveTo, AnimationCurve curve, float speed)
    {
        Vector3 start = target.position;
        Vector3 delta = moveTo - start;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            target.position = start + curve.Evaluate(t) * delta;
            yield return 0;
        }

        target.position = moveTo;
    }

    public IEnumerator SmoothMoveCoroutine(Transform target, Vector3 moveTo, AnimationCurve curve, float speed, Action end)
    {
        Vector3 start = target.position;
        Vector3 delta = moveTo - start;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            target.position = start + curve.Evaluate(t) * delta;
            yield return 0;
        }

        target.position = moveTo;
        end();
    }

    //Changes object's hue smoothly
    public IEnumerator ColorHueCoroutine(SpriteRenderer target, AnimationCurve curve, float speed)
    {
        Color start = target.color;
        Color setColor = start;
        float coef = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            coef = curve.Evaluate(t);
            setColor.r = start.r * coef;
            setColor.g = start.g * coef;
            setColor.b = start.b * coef;

            target.color = setColor;
            yield return 0;
        }

        coef = curve.Evaluate(1f);
        setColor.r = start.r * coef;
        setColor.g = start.g * coef;
        setColor.b = start.b * coef;

        target.color = setColor;
    }

    public IEnumerator ColorHueCoroutine(SpriteRenderer target, AnimationCurve curve, float speed, Action end)
    {
        Color start = target.color;
        Color setColor = start;
        float coef = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            coef = curve.Evaluate(t);
            setColor.r = start.r * coef;
            setColor.g = start.g * coef;
            setColor.b = start.b * coef;

            target.color = setColor;
            yield return 0;
        }

        coef = curve.Evaluate(1f);
        setColor.r = start.r * coef;
        setColor.g = start.g * coef;
        setColor.b = start.b * coef;

        target.color = setColor;
        end();
    }

    //Creates smooth color transition
    public IEnumerator SmoothColorTransition(SpriteRenderer target, Color transitionTo, AnimationCurve curve, float speed)
    {
        Color start = target.color;
        Color setColor = start;
        Color delta = new Color(transitionTo.r - start.r, transitionTo.g - start.g, transitionTo.b - start.b, transitionTo.a - start.a);

        float coef = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            coef = curve.Evaluate(t);
            setColor.r = start.r + delta.r * coef;
            setColor.g = start.g + delta.g * coef;
            setColor.b = start.b + delta.b * coef;
            setColor.a = start.a + delta.a * coef;

            target.color = setColor;
            yield return 0;
        }

        coef = curve.Evaluate(1f);
        setColor.r = start.r + delta.r * coef;
        setColor.g = start.g + delta.g * coef;
        setColor.b = start.b + delta.b * coef;
        setColor.a = start.a + delta.a * coef;

        target.color = setColor;
    }

    public IEnumerator SmoothColorTransition(SpriteRenderer target, Color transitionTo, AnimationCurve curve, float speed, Action end)
    {
        Color start = target.color;
        Color setColor = start;
        Color delta = new Color(transitionTo.r - start.r, transitionTo.g - start.g, transitionTo.b - start.b, transitionTo.a - start.a);

        float coef = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            coef = curve.Evaluate(t);
            setColor.r = start.r + delta.r * coef;
            setColor.g = start.g + delta.g * coef;
            setColor.b = start.b + delta.b * coef;
            setColor.a = start.a + delta.a * coef;

            target.color = setColor;
            yield return 0;
        }

        coef = curve.Evaluate(1f);
        setColor.r = start.r + delta.r * coef;
        setColor.g = start.g + delta.g * coef;
        setColor.b = start.b + delta.b * coef;
        setColor.a = start.a + delta.a * coef;

        target.color = setColor;
        end();
    }

    public IEnumerator SmoothRotateAroundZCoroutine(Transform target, float deg, AnimationCurve curve, float speed)
    {
        float start = target.rotation.z;
        float delta = deg - start;
        Vector3 setAngle = target.eulerAngles;

        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            setAngle.z = (start + delta * curve.Evaluate(t));
            target.eulerAngles = setAngle;
            yield return 0;
        }

        setAngle.z = start + delta * curve.Evaluate(1f);
        target.eulerAngles = setAngle;
    }

    public IEnumerator SmoothRotateAroundZCoroutine(Transform target, float deg, AnimationCurve curve, float speed, Action end)
    {
        float start = target.rotation.z;
        float delta = deg - start;
        Vector3 setAngle = target.eulerAngles;

        for (float t = 0f; t < 1f; t += Time.deltaTime * speed)
        {
            setAngle.z = (start + delta * curve.Evaluate(t));
            target.eulerAngles = setAngle;
            yield return 0;
        }

        setAngle.z = start + delta * curve.Evaluate(1f);
        target.eulerAngles = setAngle;

        end();
    }
}
