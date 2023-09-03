using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGameManager : MonoBehaviour
{
    [HideInInspector]
    public QuizGameCollectionManager quizColManager;
    [HideInInspector]
    public QuizSoundManager soundManager;
    //[HideInInspector]
    public List<GameObject> sceneList = new List<GameObject>();
    [HideInInspector]
    public int selectStage;
    [HideInInspector]
    public int totalValue = 5;
    [HideInInspector]
    public int answerValue;
    [HideInInspector]
    public int currentValue;

    [SerializeField, HideInInspector]
    private GameObject sceneCredit;

    private QuizSaveDataManager saveDataManager;
    private QuizGameManager manager;
    private QuizSaveData saveData;


    private void Awake()
    {
        manager = this;

        saveDataManager = manager.GetComponent<QuizSaveDataManager>();
    }

    void Start()
    {
        saveData = saveDataManager.Load();

        ChangeScene(0);
    }

    public void ChangeScene(int sceneNum = 0)
    {
        int index = 0;
        foreach(GameObject scene in sceneList)
        {
            if(sceneNum == index && !scene.activeSelf)
            {
                scene.SetActive(true);
            }
            else if(sceneNum != index && scene.activeSelf)
            {
                scene.SetActive(false);
            }
            index++;
        }
    }

    public void ChangeCreditScene(bool isActive = true)
    {
        sceneCredit.SetActive(isActive);
        soundManager.PushWindowSE(isActive);
    }

    public void Init()
    {
        currentValue = 0;
        answerValue = 0;
    }

    public void UpdateUnlockData(int value)
    {
        saveData.unLockStageValue = value; 
        saveDataManager.Save(saveData);
    }

    public int GetUnLockValue()
    {
        return saveData.unLockStageValue;
    }

    public void PushBtnPrivacy()
    {
        string strURL = quizColManager.strPrivacyPolicyURL;
        Application.OpenURL(strURL);
    }
}
