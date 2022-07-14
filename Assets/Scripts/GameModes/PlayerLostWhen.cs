using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostWhen : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private GameTimer _gameTimer;

    [Header("Lost when")]
    [SerializeField] private bool _touchedGrey;
    [SerializeField] private bool _zeroTime;

    private void Start()
    {
        if (_touchedGrey)
            _playerBehaviour.PlayerTouchedGrey += () => _levelController.Lost();
        if (_zeroTime)
            _gameTimer.TimerAtZero += () =>
            {
                _playerBehaviour.PlayerTouchedGrey.Invoke();
                new CoroutinesPack().WaitThenDo(1f,
                () => _levelController.Lost());

            };
    }
}
