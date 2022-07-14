using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
    public System.Action PlayerTouchedGrey, PlayerTouchedGreen;

    public bool IsInPlayMode { get; set; } = true;

    private bool _firstTime = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsInPlayMode) return;
        string colTag = collision.gameObject.tag;

        if (colTag == "Grey" && _firstTime)
        {
            if (PlayerTouchedGrey != null)
                PlayerTouchedGrey.Invoke();

            AudioManager.Instance.PlaySFX("Grey", 0.5f);
            _firstTime = false;
        }
        else if (colTag == "Green")
        {
            float greenish = collision.gameObject.GetComponent<SpriteRenderer>().color.g;
            PushAway(greenish * 5f, collision.transform);
            AudioManager.Instance.PlaySFX("NaturalKick", 0.25f);
            AudioManager.Instance.PlaySFX("Bonk", 0.5f);

            if (PlayerTouchedGreen != null)
                PlayerTouchedGreen.Invoke();
        } 

    }

    private void PushAway(float power, Transform col)
    {
        var spriteBoundsExtents = col.GetComponent<SpriteRenderer>().bounds.extents;
        float rightX = col.position.x + spriteBoundsExtents.x / 2f;
        float leftX = col.position.x - spriteBoundsExtents.x / 2f;
        float upperY = col.position.y + spriteBoundsExtents.y / 2f;
        float downY = col.position.y - spriteBoundsExtents.y / 2f;
        Vector2 direction = Vector2.zero;

        if (_playerRb.transform.position.y >= upperY)
        {
            direction = Vector2.up;
        }
        else if (_playerRb.transform.position.y < downY)
        {
            direction = Vector2.down;
        }
        else if (_playerRb.transform.position.x <= leftX)
        {
            direction = Vector2.left;
        }
        else if (_playerRb.transform.position.x > rightX)
        {
            direction = Vector2.right;
        }

        _playerRb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
