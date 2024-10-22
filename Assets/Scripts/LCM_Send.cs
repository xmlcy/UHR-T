using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using LCM;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.UI;

namespace LCM.Examples
{
    class MyLCM : MonoBehaviour
    {
        public static MyLCM _instance;
        public LCM.LCM LCM1;
        // public Text ipStateText;

        private void Awake()
        {
            _instance = this;
        }

        public void initLCM()
        {
            LCM1 = new LCM.LCM("udpm://239.255.76.67:7667?ttl=255");
            //ipStateText.text = "GetLcmUrl" + "udpm://239.255.76.67:7667?ttl=255";
        }
        public void facePublish(faceLcmData.faceData msg)
        {
            LCM1.Publish("faceData", msg);
        }
    }
}




