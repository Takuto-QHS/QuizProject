using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuizGameCollectionManager : MonoBehaviour
{
    /* 各お題情報 */
    [System.Serializable]
    public struct QuizInfoStruct
    {
        [MultilineAttribute(1)]
        public string question;             // 質問文(改行機能有り)
        [Space(10)]
        public List<string> listSelectSentence; // 選択文
        public int answerNum;               // 正解番号
        [MultilineAttribute(1)]
        public string explanation;          // 説明(改行機能有り)
    }

    /* 出題数用List */
    [System.Serializable]
    public struct StageInfoStruct
    {
        public List<QuizInfoStruct> listQuiz;
    }

    /* ゲームタイトル */
    public string strGameTitle;
    /* プライバシーポリシーURL */
    public string strPrivacyPolicyURL;
    /* Stage数用List */
    [HideInInspector]
    public List<StageInfoStruct> listStage = new List<StageInfoStruct>();
    /* CSVファイル */
    public TextAsset csvFile;

    private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

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

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine().Replace("\\n", "\n"); // 一行ずつ読み込み+改行コード置換
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }
}
