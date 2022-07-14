using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectThemAll : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private int _toCollect;

    public int ToCollect {
        get
        {
            return _toCollect;
        }
        set
        {
            _toCollect = value;

            if (_toCollect == 0 && !_levelController.IsLevelEnded)
            {
                _levelController.Won();
                _playerAnimation.PlayerWonAnimation();
            }
        }
    }
}
