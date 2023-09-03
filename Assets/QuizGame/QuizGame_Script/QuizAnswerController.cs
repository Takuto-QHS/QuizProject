using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAnswerController : MonoBehaviour
{
    public enum TypeAnswer
    {
        A,
        I,
        U,
        E
    }
    public struct QuizAnswerControlStruct
    {
        public int answerNum;               // 正答ナンバー
        public string answerSelectTxt;      // 正答選択文

        [MultilineAttribute(1)]
        public string explanation;          // 解説文
    }

    [HideInInspector]
    public QuizGameManager qGameManager;

    [SerializeField, HideInInspector]
    Text txtAnswer;
    [SerializeField, HideInInspector]
    Text txtExplanationTItle;
    [SerializeField, HideInInspector]
    Text txtExplanation;
    [SerializeField, HideInInspector]
    Button BtnNext;

    private QuizGameCollectionManager.StageInfoStruct stageInfo;
    private QuizGameCollectionManager.QuizInfoStruct quizInfo;
    private QuizAnswerControlStruct answerStruct;

    private void OnEnable()
    {
        SetAnswerStruct();
        UpdateTxt();

        BtnNext.onClick.RemoveAllListeners();
        BtnNext.onClick.AddListener(PushBtnNext);
    }

    void SetAnswerStruct()
    {
        int selectStage = qGameManager.selectStage;
        stageInfo = qGameManager.quizColManager.listStage[selectStage - 1];
        quizInfo = stageInfo.listQuiz[qGameManager.currentValue];

        answerStruct.answerNum = quizInfo.answerNum;
        answerStruct.answerSelectTxt = quizInfo.listSelectSentence[answerStruct.answerNum];
        answerStruct.explanation = quizInfo.explanation;
    }

    void UpdateTxt()
    {
        txtAnswer.text = GetAnswerString(answerStruct.answerNum);
        txtExplanationTItle.text = answerStruct.answerSelectTxt;
        txtExplanation.text = answerStruct.explanation;
    }

    string GetAnswerString(int index)
    {
        string answer = null;
        switch((index))
        {
            case (int)TypeAnswer.A:
                answer = "ア";
                break;
            case (int)TypeAnswer.I:
                answer = "イ";
                break;
            case (int)TypeAnswer.U:
                answer = "ウ";
                break;
            case (int)TypeAnswer.E:
                answer = "エ";
                break;
        }
        return answer;
    }

    void PushBtnNext()
    {
        qGameManager.soundManager.PushBtnSE();

        qGameManager.currentValue++;
        if (qGameManager.currentValue == qGameManager.totalValue)
        {
            qGameManager.ChangeScene(3);
        }
        else
        {
            qGameManager.ChangeScene(1);
        }
    }
}
