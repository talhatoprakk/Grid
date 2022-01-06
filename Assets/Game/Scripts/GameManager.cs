using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    
    public void StartButton() 
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}