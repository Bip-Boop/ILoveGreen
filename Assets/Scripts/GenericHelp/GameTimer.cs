using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    enum TimerBehaviur {GoUp, GoDown}

    [SerializeField] private Text _timerText;

    [SerializeField] private TimerBehaviur _timerBehaviur;
    [SerializeField] private int _startTime;
    [SerializeField] private bool _startOnAwake;

    public System.Action TimerAtZero;
    public int CurrenValue { get; private set; } = -1;

    private void Awake()
    {
        if (_startOnAwake)
            StartTimer();
    }

    public void StartTimer()
    {
        StopAllCoroutines();
        _timerText.text = _startTime.ToString();
        CurrenValue = _startTime;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        if (_timerBehaviur == TimerBehaviur.GoUp)
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                CurrenValue += 1;
                _timerText.text = CurrenValue.ToString();
            }
        }
        else
        {
            while (CurrenValue > 0)
            {
                yield return new WaitForSeconds(1f);
                CurrenValue -= 1;
                _timerText.text = CurrenValue.ToString();
            }
            if (TimerAtZero != null)
                TimerAtZero.Invoke();
        }
    }
}
