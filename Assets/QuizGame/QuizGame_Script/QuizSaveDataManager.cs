using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizSaveData
{
    public int unLockStageValue = 1;
}

public class QuizSaveDataManager : MonoBehaviour
{
    string filePath;
    QuizSaveData save;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".savedata.json"; save = new QuizSaveData();

    }

    public void Save(QuizSaveData _save)
    {
        save = _save;

        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public QuizSaveData Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<QuizSaveData>(data);
        }
        return save;
    }
}
