using UnityEngine;
using UnityEngine.Events;

public  class EventManager  : MonoBehaviour
{
    public static readonly UnityEvent OnButtonClick = new UnityEvent();
}
