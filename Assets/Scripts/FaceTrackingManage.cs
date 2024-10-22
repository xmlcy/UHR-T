using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using LCM;
using LCM.Examples;

public class FaceTrackingManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 当前应用需要面部追踪能力
        TrackingStateCode trackingState;
        trackingState = (TrackingStateCode)PXR_MotionTracking.WantFaceTrackingService();

        // 查询当前设备是否支持面部追踪
        //bool supported = false;
        //int supportedCount = 0;
        //FaceTrackingMode faceTrackingMode = FaceTrackingMode.PXR_FTM_FACE_LIPS_BS;
        //trackingState = (TrackingStateCode)PXR_MotionTracking.GetFaceTrackingSupported(ref supported, ref supportedCount, ref faceTrackingMode);

        // 开始面部追踪
        FaceTrackingStartInfo info = new FaceTrackingStartInfo();
        info.mode = FaceTrackingMode.PXR_FTM_FACE;
        trackingState = (TrackingStateCode)PXR_MotionTracking.StartFaceTracking(ref info);

        // 获取面部追踪状态
        bool tracking = false;
        FaceTrackingState faceTrackingState = new FaceTrackingState();
        trackingState = (TrackingStateCode)PXR_MotionTracking.GetFaceTrackingState(ref tracking, ref faceTrackingState);

        MyLCM._instance.initLCM();
    }

    unsafe FaceTrackingData GetFaceTrackingData()
    {
        // 获取面部追踪数据
        TrackingStateCode trackingState;
        FaceTrackingDataGetInfo info = new FaceTrackingDataGetInfo();
        info.displayTime = 0;
        info.flags = FaceTrackingDataGetFlags.PXR_FACE_DEFAULT;
        FaceTrackingData faceTrackingData = new FaceTrackingData();
        float* b = stackalloc float[72]; // 数组长度必须为 72， 否则会报错
        faceTrackingData.blendShapeWeight = b;
        trackingState = (TrackingStateCode)PXR_MotionTracking.GetFaceTrackingData(ref info, ref faceTrackingData);

        return faceTrackingData;
    }

    // Update is called once per frame
    void Update()
    {
        picoLcmData.faceData msg = new picoLcmData.faceData();
        unsafe {
            for (int i = 0; i < 72; i++) {
                msg.bs[i] = GetFaceTrackingData().blendShapeWeight[i]; 
            }
        }
        MyLCM._instance.facePublish(msg);
    }

}
