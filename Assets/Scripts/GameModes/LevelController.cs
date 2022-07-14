using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int DropsCollected { get; set; }
    public bool IsLevelEnded { get; private set; }

    [SerializeField] private PlayerController _playerController;

    private bool _firstCall = true;
    public void Lost()
    {
        if (!_firstCall) return;
        LevelEnded();
        _playerController.GetComponent<PlayerBehaviour>().IsInPlayMode = false;
        StartCoroutine(
        new CoroutinesPack().WaitThenDo(1.5f, () =>
        {
            print("Lose panel");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }));


    }

    public void Won()
    {
        if (!_firstCall) return;
        LevelEnded();

        CoroutinesPack cp = new CoroutinesPack();
        StartCoroutine(cp.WaitThenDo(0.5f, () => _playerController.GetComponent<PlayerBehaviour>().IsInPlayMode = false));
        StartCoroutine(cp.WaitThenDo(2f, () =>
        {

            LevelsProgressManager.Instance.RegisterLevelComplete(DropsCollected);
            LevelsProgressManager.Instance.LoadNextLevelAccordingToProgress();
        }));


    }

    private void LevelEnded()
    {
        _firstCall = false;
        IsLevelEnded = true;
        _playerController.CanControl = false;


        print(DropsCollected + " drops collected");
        //Save collected drops
    }
}
