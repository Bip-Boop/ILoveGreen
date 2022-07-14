using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsProgressManager : MonoBehaviour
{
    [System.Serializable] struct Stage
    {
        public List<string> scenesNames;
        public int completeToMoveToTheNextStage;
    }

    public static LevelsProgressManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int CurrentStage { get; set; }
    [SerializeField] private Stage[] _stages;
    [SerializeField] private Stage[] _progressStages;

    public void ResetProgress()
    {
        _progressStages = new Stage[_stages.Length];

        for (int i = 0; i < _stages.Length; i++)
        {
            _progressStages[i].scenesNames = new List<string>(_stages[i].scenesNames);
            _progressStages[i].completeToMoveToTheNextStage = _stages[i].completeToMoveToTheNextStage;
        }
        
    }

    public void RegisterLevelComplete(int dropsCollected)
    {
        _progressStages[CurrentStage].completeToMoveToTheNextStage--;
        if (_progressStages[CurrentStage].completeToMoveToTheNextStage == 0 && CurrentStage +1 < _progressStages.Length)
        {
            CurrentStage++;
        }
    }

    public void LoadNextLevelAccordingToProgress()
    {
        
        string chosenLevel = _progressStages[CurrentStage].scenesNames[Random.Range
            (0, _progressStages[CurrentStage].scenesNames.Count)];

        if (CurrentStage != _progressStages.Length - 1)
            _progressStages[CurrentStage].scenesNames.Remove(chosenLevel);

        UnityEngine.SceneManagement.SceneManager.LoadScene(chosenLevel);
    }
}
