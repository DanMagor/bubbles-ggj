using System;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        
        public static List<Photon.Realtime.Player> Players;
        private CinemachineVirtualCamera camera;
        private void Awake()
        {
                DontDestroyOnLoad(this);
                camera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        
        
        
}