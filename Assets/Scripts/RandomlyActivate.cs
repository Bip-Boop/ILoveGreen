using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyActivate : MonoBehaviour
{
    [Range(0f,1f), SerializeField] private float _chanceOfActivation;

    private void Start()
    {
        if (Random.value < _chanceOfActivation)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
