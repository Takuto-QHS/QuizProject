using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizSoundManager : MonoBehaviour
{
    //[SerializeField]
    private AudioSource audioSourceBGM;
    [SerializeField]
    private AudioClip[] clipBGM;
    //[SerializeField]
    private AudioSource audioSourceSE;
    [SerializeField]
    private AudioClip[] clipSE;

    void Awake()
    {
        /* BGMAudioSourceの生成＆設定 */
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceBGM.loop = true;
        audioSourceBGM.volume = 0.3f;

        /* BGMAudioSourceの生成 */
        audioSourceSE = gameObject.AddComponent<AudioSource>();
    }

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void StartBGM(int index = 0)
    {
        audioSourceBGM.clip = clipBGM[index];
        audioSourceBGM.Play();
    }

    public void FinishSE()
    {
        audioSourceSE.clip = clipSE[0];
        audioSourceSE.Play();
    }

    public void CorrectSE()
    {
        audioSourceSE.clip = clipSE[1];
        audioSourceSE.Play();
    }

    public void IncorrectSE()
    {
        audioSourceSE.clip = clipSE[2];
        audioSourceSE.Play();
    }

    public void PushBtnSE()
    {
        audioSourceSE.clip = clipSE[3];
        audioSourceSE.Play();
    }

    public void PushWindowSE(bool isOpen)
    {
        int value = isOpen ? 4 : 5;
        audioSourceSE.clip = clipSE[value];
        audioSourceSE.Play();
    }
}