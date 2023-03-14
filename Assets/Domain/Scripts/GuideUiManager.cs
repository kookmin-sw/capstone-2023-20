using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using static System.Net.Mime.MediaTypeNames;

public class GuideUiManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject SubPanel;
    public TMP_Text MainTxt;
    public TMP_Text SubTxt;
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
    public void SetMainPanelText()
    {
        maintxt = maintxt.Replace("\\n", "\n");
        MainTxt.SetText(maintxt.ToString());
    }
    public void SetSubPanelText()
    {
        subtxt = subtxt.Replace("\\n", "\n");
        SubTxt.SetText(subtxt.ToString());
    }
    public void SetPanelText()
    {
        SetMainPanelText();
        SetSubPanelText();
    }
    public void ActiveMainPanel()
    {
        SetMainPanelText();
        MainPanel.SetActive(true);
    }

}
