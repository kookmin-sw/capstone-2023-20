using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleScene1 : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject taichi;
    public GameObject lafita;
    public GameObject LafitaModel;
    public GameObject timeline;
    public Text text;
    public Text name;
    int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        lafita.SetActive(false);
        timeline = GameObject.Find("TimeLines");
        LafitaModel = GameObject.Find("Lafita");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickCount == 0)
            {
                text.text = "안녕하세요.";
                clickCount++;
            }
            else if (clickCount == 1)
            {
                text.text = "반갑습니다.";
                clickCount++;
            }
            else if (clickCount == 2)
            {
                name.text = "라피타";
                lafita.SetActive(true);
                text.text = "가볼까.......";
                clickCount++;
            }
            else if (clickCount == 3)
            {
                talkPanel.SetActive(false);
                timeline.transform.Find("TimeLine2").gameObject.SetActive(true);
                LafitaModel.transform.Find("Lafita_idle").gameObject.SetActive(false);
                LafitaModel.transform.Find("Lafita_walk").gameObject.SetActive(true);

            }
        }
    }
}
