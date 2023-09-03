using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizQuestionController : MonoBehaviour
{
    [System.Serializable]
    public struct BtnSelectStruct
    {
        public GameObject btnObjParent;
        public Button btnObject;
        public Text txtSelect;
    }
    [System.Serializable]
    public struct QuizQuestionControlStruct
    {
        public int answerValue;             // 正答数
        public int totalValue;              // 全出題数

        public int currentValue;            // 今の出題番号
        [TextArea]
        public string question;             // 質問文
        public List<string> listSelectSentence; // 選択文
        public int answerNum;               // 正解番号
    }
    private QuizQuestionControlStruct questionControlStruct;

    [HideInInspector]
    public QuizGameManager qGameManager;
    [SerializeField, HideInInspector]
    private List<BtnSelectStruct> btnSelectList = new List<BtnSelectStruct>();

    [SerializeField, HideInInspector]
    private Text txtAnswer;
    [SerializeField, HideInInspector]
    private Text txtTotal;
    [SerializeField, HideInInspector]
    private Text txtCurrent;
    [SerializeField, HideInInspector]
    private Text txtQuestion;


    void SetStruct()
    {
        questionControlStruct.answerValue = qGameManager.answerValue;
        questionControlStruct.totalValue = qGameManager.quizColManager.listStage[qGameManager.selectStage - 1].listQuiz.Count;
        questionControlStruct.currentValue = qGameManager.currentValue + 1;

        QuizGameCollectionManager.QuizInfoStruct listQuizInfo;
        listQuizInfo = qGameManager.quizColManager.listStage[qGameManager.selectStage - 1].listQuiz[questionControlStruct.currentValue - 1];
        questionControlStruct.question = listQuizInfo.question;
        questionControlStruct.listSelectSentence = listQuizInfo.listSelectSentence;
        questionControlStruct.answerNum = listQuizInfo.answerNum;
    }

    private void OnEnable()
    {
        SetStruct();
        InitBtnSelectList();

        UpdateUpperText();
        UpdateQuestionBox();
        UpdateQuestionBtnBox();
    }

    void UpdateUpperText()
    {
        txtAnswer.text = questionControlStruct.answerValue.ToString();
        txtTotal.text = questionControlStruct.totalValue.ToString();
    }

    void UpdateQuestionBox()
    {
        int currentIndex = questionControlStruct.currentValue;
        txtCurrent.text = "第" + currentIndex.ToString() + "問";
        txtQuestion.text = questionControlStruct.question;
    }

    void UpdateQuestionBtnBox()
    {
        int index = 0;
        foreach (BtnSelectStruct select in btnSelectList)
        {
            if(index == questionControlStruct.listSelectSentence.Count)
            {
                break;
            }

            select.btnObjParent.SetActive(true);
            select.btnObject.onClick.RemoveAllListeners();

            if (index == questionControlStruct.answerNum)
            {
                select.btnObject.onClick.AddListener(() => PushBtnTrue());
            }
            else
            {
                select.btnObject.onClick.AddListener(() => PushBtnFalse());
            }

            select.txtSelect.text = questionControlStruct.listSelectSentence[index];

            index++;
        }
    }

    void PushBtnTrue()
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.answerValue++;
        qGameManager.ChangeScene(2);
        qGameManager.soundManager.CorrectSE();
    }

    void PushBtnFalse()
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.ChangeScene(2);
        qGameManager.soundManager.IncorrectSE();
    }

    void InitBtnSelectList()
    {
        foreach (BtnSelectStruct select in btnSelectList)
        {
            select.btnObjParent.SetActive(false);
        }
    }
}
