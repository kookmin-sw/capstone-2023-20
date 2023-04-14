using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : MonoBehaviour
{
    public UnityEvent Event;
    public UnityEvent OncePlayEvent;

    int PlayCount = 0;
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
        if(PlayCount == 0)
        {
            OncePlayEvent.Invoke();
            PlayCount++;
        }
        Event.Invoke();
    }


}
