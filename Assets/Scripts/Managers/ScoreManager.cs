using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameManager
{
    [SerializeField] UIController UIController;
    public ManagerStatus status { get; private set; }
    public int currentScore { get; private set; }

    public int scoreStep = 1;
    public int bonusStep;
    private int elementInRow = 0;

    public void StartUp()
    {
        Debug.Log("Score manager started...");

        currentScore = 0;

        status = ManagerStatus.Started;
    }

    public void ClearScore()
    {
        currentScore = 0;
        elementInRow = 0;
    }

    public void IncreasScore()
    {
        //plus score step
        currentScore += scoreStep;
        //if in row then
        if (elementInRow > 2)
        {
            IncreasScoreBonus();
        }

        elementInRow += 1;
        //UIController.OnScoreUpdate();
    }

    public void FailScore()
    {
        elementInRow = 0;
        bonusStep = 1;
        Debug.Log("Fail!");
        //UIController.OnScoreUpdate();
    }

    private void IncreasScoreBonus()
    {
        bonusStep = 1;
        // change bonus score on different row result
        if (elementInRow == 3)
        {
            bonusStep = 2;
        }

        if (elementInRow > 3 && elementInRow < 7)
        {
            bonusStep = 3;
        }

        if (elementInRow > 6 && elementInRow < 10)
        {
            bonusStep = 4;
        }

        if (elementInRow > 9)
        {
            bonusStep = 5;
        }

        currentScore += scoreStep * bonusStep;
    }

}
