using System;
using UnityEngine;
using UnityEngine.Events;

public class Watcher : MonoBehaviour
{
    public event Action AlarmOn;
    public event Action AlarmOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AlarmOn?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AlarmOff?.Invoke();
    }
}
