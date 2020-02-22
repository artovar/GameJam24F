using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimerController : MonoBehaviour, IObserver
{

    [SerializeField]
    private MyEvent.EventType typeToReact = MyEvent.EventType.baseType;

    private Image timerBar;

    private float initialTime;
    private float elapsedTime;

    private void Awake()
    {
        timerBar = GetComponent<Image>();
    }

    private void Update()
    {
        if(elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            timerBar.fillAmount = elapsedTime / initialTime;
        }
        else
        {
            elapsedTime = 0;
            timerBar.fillAmount = 0;
        }
    }

    public void onNotify(MyEvent _event)
    {
        if(_event.myType == typeToReact)
        {
            StartTimer(_event.sender.gameObject);
        }
    }

    private void StartTimer(GameObject sender)
    {
        initialTime = sender.GetComponent<IPowerUp>().GetTime();
        elapsedTime = initialTime;
        timerBar.fillAmount = 1;
        StartCoroutine(TimerCoroutine(initialTime));
    }

    private IEnumerator TimerCoroutine(float time)
    {
        
        yield return new WaitUntil(() => timerBar.fillAmount == 0);
        timerBar.fillAmount = 0;
    }
}
