using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOscillation : MonoBehaviour
{
    public bool IsIdling;

    [SerializeField] private Vector3 _deviation;
    [SerializeField] private float _speed;

    private IEnumerator Idle()
    {
        Vector3 start = transform.position;
        for (float t = 0f; IsIdling; t += Time.deltaTime * _speed)
        {
            transform.position = start + _deviation * Mathf.Sin(t);

            if (t >= Mathf.PI * 2f)
                t = 0f;

            yield return 0;
        }
    }


    private void Start()
    {
        StartCoroutine(Idle());
    }

}
