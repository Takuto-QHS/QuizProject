using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuizGameCollectionManager : MonoBehaviour
{
    /* �e������ */
    [System.Serializable]
    public struct QuizInfoStruct
    {
        [MultilineAttribute(1)]
        public string question;             // ���╶(���s�@�\�L��)
        [Space(10)]
        public List<string> listSelectSentence; // �I��
        public int answerNum;               // ����ԍ�
        [MultilineAttribute(1)]
        public string explanation;          // ����(���s�@�\�L��)
    }

    /* �o�萔�pList */
    [System.Serializable]
    public struct StageInfoStruct
    {
        public List<QuizInfoStruct> listQuiz;
    }

    /* �Q�[���^�C�g�� */
    public string strGameTitle;
    /* �v���C�o�V�[�|���V�[URL */
    public string strPrivacyPolicyURL;
    /* Stage���pList */
    [HideInInspector]
    public List<StageInfoStruct> listStage = new List<StageInfoStruct>();
    /* CSV�t�@�C�� */
    public TextAsset csvFile;

    private List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;

    void Start()
    {
        CSVReader();

        for(int i = 1; i < csvDatas.Count; i++)
        {
            int stageNum = int.Parse(csvDatas[i][0]);
            if (listStage.Count < stageNum)
            {
                StageInfoStruct stageInfoStruct = new StageInfoStruct();
                stageInfoStruct.listQuiz = new List<QuizInfoStruct>();
                listStage.Add(stageInfoStruct);
            }

            QuizInfoStruct quizData = new QuizInfoStruct();
            quizData.question = csvDatas[i][1];
            quizData.listSelectSentence = new List<string>();
            for (int x = 2; x <= 5; x++)
            {
                if (csvDatas[i][x] != null && csvDatas[i][x].Length != 0)
                {
                    quizData.listSelectSentence.Add(csvDatas[i][x]);
                }
            }
            quizData.answerNum = int.Parse(csvDatas[i][6]);
            quizData.explanation = csvDatas[i][7];

            listStage[stageNum - 1].listQuiz.Add(quizData);
        }
    }

    void CSVReader()
    {
        StringReader reader = new StringReader(csvFile.text);

        if(reader == null)
        {
            Debug.LogError("csvFile is Null");
        }

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine().Replace("\\n", "\n"); // ��s���ǂݍ���+���s�R�[�h�u��
            csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }
    }
}
