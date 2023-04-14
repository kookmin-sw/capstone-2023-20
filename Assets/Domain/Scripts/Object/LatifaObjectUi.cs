using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatifaObjectUi : MonoBehaviour
{
    public GameObject latifa;
    public GuideUiManager UiManager;
    public Timeline[] Timelines;
    public Timeline Both;
    public Timeline Main;
    public Timeline Sub;

    public string maintxt;
    public string subtxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        latifa = GameObject.FindGameObjectWithTag("Latifa");
        UiManager = latifa.GetComponentInChildren<GuideUiManager>();
        Timelines = latifa.GetComponentsInChildren<Timeline>();
        if (Timelines.Length > 0)
        {
            Debug.Log(Timelines[1]);
            // 원하는 스크립트를 선택하여 사용
            Both = Timelines[0];
            Main = Timelines[1];
            Sub = Timelines[2];
        }

        //Transform MainContoller = latifa.transform.Find("MainContloller");
        //Transform SubContoller = latifa.transform.Find("SubContloller");
        //MainTimeline = MainContoller.GetComponent<Timeline>();
        //SubTimeline = SubContoller.GetComponent<Timeline>();
    }
    //public void SetMainPanelText(string txt)
    //{
    //    maintxt = txt;
    //}
    //public void SetSubPanelText(string txt)
    //{
    //    subtxt = txt;
    //}
    public void SetGuideUi()
    {
        UiManager.SetMainPanelText(maintxt);
        UiManager.SetSubPanelText(subtxt);

    }
    public void TimeLineActive()
    {
        Both.Play();
    }
}
