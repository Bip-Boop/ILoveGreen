using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheScript : MonoBehaviour
{
    [SerializeField] private float _averageSize;
    [SerializeField] private float _speed;
    [SerializeField] private float _amplitude;

    public bool IsBreathing;

    void Start()
    {
        StartCoroutine(BreatheCoroutine());
    }

    private IEnumerator BreatheCoroutine()
    {
        float t = 0f;
        while (IsBreathing)
        {
            transform.localScale = Vector3.one * (_averageSize + Mathf.Sin(t) * _amplitude);
            t += Time.deltaTime * _speed;
            yield return 0;
        }
    }

}
