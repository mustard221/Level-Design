using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteraction : MonoBehaviour
{
    public UnityEvent enteredTrigger, exitedTrigger; //johns code from class. turns the object off once interacted!!

    private void OnTriggerEnter2D(Collider2D other)
    {
        enteredTrigger.Invoke();
        ScoreManager.Instance.AddScore(100);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        exitedTrigger.Invoke();
    }
}