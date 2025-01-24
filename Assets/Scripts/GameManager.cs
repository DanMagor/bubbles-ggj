using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static int numPlayers = 0;
    private void Awake()
    {
        
        DontDestroyOnLoad(this);
        
    }
}