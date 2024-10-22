using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;

public class SeeThroughManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 开启透视
        PXR_Manager.EnableVideoSeeThrough = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
