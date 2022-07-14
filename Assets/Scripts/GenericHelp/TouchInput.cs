using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    public static TouchInput Instance;

    public Action<Vector3> GamePlayTouchBegan;
    public Action<Vector3> GamePlayContinuousTouch;


    private Camera _MainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _MainCamera = Camera.main;
    }


    void Update()
    {
        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            //only last touch matters
            Touch touch = Input.GetTouch(touchCount - 1);
            Vector3 touchPos = _MainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began : GamePlayTouchBegan.Invoke(touchPos);break;
                case TouchPhase.Stationary: GamePlayContinuousTouch.Invoke(touchPos);break;
            }

        }
        
    }
}
