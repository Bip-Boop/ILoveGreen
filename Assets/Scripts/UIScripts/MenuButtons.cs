using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        LevelsProgressManager.Instance.ResetProgress();
        LevelsProgressManager.Instance.CurrentStage = 0;
        LevelsProgressManager.Instance.LoadNextLevelAccordingToProgress();
    }
}
