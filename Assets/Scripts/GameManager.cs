using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        
        public static List<Photon.Realtime.Player> Players;
        private void Awake()
        {
                DontDestroyOnLoad(this);
        }

        
        
}