using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float _controlImpulsePower;
    [SerializeField] private float _controlForcePower;

    public bool CanControl { get; set; } = true;

    private void Start()
    {
        TouchInput instance = TouchInput.Instance;
        instance.GamePlayContinuousTouch += TouchForce;
        instance.GamePlayTouchBegan += TouchImpulse;
    }

    private void TouchForce (Vector3 position)
    {
        if (!CanControl) return;
        if (position.x <= 0) // left
        {
            _playerRb.AddForce(Vector2.left * _controlForcePower, ForceMode2D.Force);
        }
        else // right
        {
            _playerRb.AddForce(Vector2.right * _controlForcePower, ForceMode2D.Force);
        }
    }

    Vector2 lastDirectoin = Vector2.zero;
    private void TouchImpulse(Vector3 position)
    {
        if (!CanControl) return;

        if (position.x <= 0 && lastDirectoin != Vector2.left) // left
        {
            lastDirectoin = Vector2.left;
            _playerRb.AddForce(Vector2.left * _controlImpulsePower, ForceMode2D.Impulse);
        }
        else if (position.x >= 0 && lastDirectoin != Vector2.right)
        {
            lastDirectoin = Vector2.right;
            _playerRb.AddForce(Vector2.right * _controlImpulsePower, ForceMode2D.Impulse);
        }
    }

}
