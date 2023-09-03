using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizResultController : MonoBehaviour
{
    [HideInInspector]
    public QuizGameManager qGameManager;

    [HideInInspector]
    public Text txtTotal;
    [HideInInspector]
    public Text txtAnswerValue;
    [HideInInspector]
    public GameObject objNextBtn;
    
    void OnEnable()
    {
        qGameManager.soundManager.StopBGM();
        qGameManager.soundManager.FinishSE();

        UpdateResultBox();

        if (qGameManager.answerValue >= qGameManager.totalValue)
        {
            UpdateNextBtn(true);

            if (qGameManager.GetUnLockValue() == qGameManager.selectStage)
            {
                int value = qGameManager.GetUnLockValue() + 1;
                qGameManager.UpdateUnlockData(value);
            }
        }
        else
        {
            UpdateNextBtn(false);
        }

    }

    void UpdateResultBox()
    {
        txtTotal.text = qGameManager.totalValue.ToString() + "問中";
        txtAnswerValue.text = qGameManager.answerValue.ToString() + "問正解";
    }

    public void PushTitle()
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.ChangeScene(0);
    }

    public void PushRetry()
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.Init();
        qGameManager.soundManager.StartBGM(1);
        qGameManager.ChangeScene(1);
    }

    public void PushNext()
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.Init();
        qGameManager.selectStage = qGameManager.selectStage + 1;
        qGameManager.totalValue = qGameManager.quizColManager.listStage[qGameManager.selectStage - 1].listQuiz.Count;
        qGameManager.ChangeScene(1);
        qGameManager.soundManager.StartBGM(1);
    }

    void DisplayNextBtn()
    {
        int stageCount = qGameManager.quizColManager.listStage.Count;
        bool isActive = qGameManager.selectStage < stageCount;  // 最終ステージでは出さない為
        objNextBtn.SetActive(isActive);
    }
    
    void UpdateNextBtn(bool isActive)
    {
        if(isActive)
        {
            DisplayNextBtn();
        }
        else
        {
            objNextBtn.SetActive(false);
        }
    }
}
