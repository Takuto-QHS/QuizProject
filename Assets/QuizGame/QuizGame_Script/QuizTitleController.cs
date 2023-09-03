using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTitleController : MonoBehaviour
{
    [HideInInspector]
    public QuizGameManager qGameManager;
    [HideInInspector]
    public Text txtGameTitle;
    [HideInInspector]
    public GameObject stageListBox;
    [HideInInspector]
    public Button instantiateBtnStage;
    [HideInInspector]
    public Sprite lockIcon;
    
    private List<Button> btnList = new List<Button>();
    
    void OnEnable()
    {
        qGameManager.soundManager.StartBGM(0);

        if(btnList.Count != 0)
        {
            UpdateBtnListLock();
        }
    }

    void Start()
    {
        UpdateTxtTitle();
        InstantiateListBtn();
        UpdateBtnListLock();
    }

    void UpdateTxtTitle()
    {
        if(txtGameTitle != null)
        {
            txtGameTitle.text = qGameManager.quizColManager.strGameTitle;
        }
    }

    public void PushStartBtn(int indexStage = 1)
    {
        qGameManager.soundManager.PushBtnSE();
        qGameManager.Init();
        qGameManager.selectStage = indexStage;
        qGameManager.totalValue = qGameManager.quizColManager.listStage[qGameManager.selectStage - 1].listQuiz.Count;
        qGameManager.ChangeScene(1);
        qGameManager.soundManager.StartBGM(1);
    }

    public void PushGameEndBtn()
    {
        qGameManager.soundManager.PushBtnSE();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    void InstantiateListBtn()
    {
        int stageCount = qGameManager.quizColManager.listStage.Count;
        for (int i = 1; i <= stageCount; i++)
        {
            Button btn = Instantiate(instantiateBtnStage, stageListBox.transform);
            int x = i; // iを引数に渡すとLoop終了後の値が全てに入る為
            btn.onClick.AddListener(() => PushStartBtn(x));
            btn.GetComponentInChildren<Text>().text = "ステージ " + i;

            btnList.Add(btn);
        }
    }

    void UpdateBtnListLock()
    {
        int i = 1;
        foreach (Button btn in btnList)
        {
            /* GetComponentsInChildrenだと自分のImageも取得するので
             * 自分のImageは弾いて子にImageがある場合imageにセット */
            Image image = null;
            foreach (Image child in btn.GetComponentsInChildren<Image>())
            {
                // 自分自身の場合は処理をスキップ
                if (child.gameObject == btn.gameObject) continue;

                image = child;
            }

            if (qGameManager.GetUnLockValue() < i)
            {
                btn.interactable = false;

                if (image == null)
                {
                    GameObject obj = Instantiate(new GameObject(), btn.transform);
                    obj.AddComponent<Image>().sprite = lockIcon;
                }
            }
            else
            {
                btn.interactable = true;

                if (image != null)
                {
                    Destroy(image.gameObject);
                }
            }
            i++;
        }
    }
}
