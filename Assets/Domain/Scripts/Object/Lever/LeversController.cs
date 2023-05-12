using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LeversController : MonoBehaviour
{
    // Start is called before the first frame update
    public lever[] levers;
    public int OrderNumber = 0;
    private int ClearNumber;
    void Start()
    {
        //levers = GameObject.Find("Shield Metall");
        ClearNumber = levers.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverDoorTagChange()
    {
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject lever in cups)
        {
            lever.tag = "EventObj";
        }
    }
        
    public void Initiate()
    {
        OrderNumber = 0;
        List<int> numbers = new List<int>() { 0, 1, 2, 3 };
        List<int> pickedNumbers = new List<int>();

        foreach (lever lever in levers)
        {
            lever.SwichOff1s();
            lever.ImageInActive();
        }

        //레버마다 넘버 랜덤으로 부여
        foreach (lever lever in levers)
        {
            int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
            int pickedNumber = numbers[randomIndex];
            numbers.RemoveAt(randomIndex);
            pickedNumbers.Add(pickedNumber);

            lever.number = pickedNumber;

        }
        // 레버 넘버순으로 재정렬
        levers = levers.OrderBy(o => o.number).ToArray();;
        levers[0].ImageActive();
    }
    public void NumberCheck(int num)
    {
        if (num != OrderNumber)
        {
            //게임 초기화
            Debug.Log("순서 틀림");
            Initiate();

        }
        else if (num == ClearNumber)
        {
            Debug.Log("clear");
        }

        else
        {
            levers[OrderNumber].ImageInActive();
            OrderNumber++;
            levers[OrderNumber].ImageActive();
        }
    }
}
