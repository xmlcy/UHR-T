using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR;
using LCM;
using LCM.Examples;

public class BodyTrackingManage : MonoBehaviour
{
    private Transform bodytrackers;
    private int bodytrackingSum = 24;

    private BodyTrackingGetDataInfo bdi = new BodyTrackingGetDataInfo();
    private BodyTrackingData bd = new BodyTrackingData();
    private BodyTrackerResult m_BodyTrackerResult;
    private double mDisplayTime;

    // Start is called before the first frame update
    void Start()
    {
        // 检查当前是否为全身动捕模式，且是否已连接两个体感追踪器
        PXR_MotionTracking.CheckMotionTrackerModeAndNumber(MotionTrackerMode.BodyTracking, MotionTrackerNum.TWO);
        int trackerCalibState = 0;
        PXR_Input.GetMotionTrackerCalibState(ref trackerCalibState);
        if (trackerCalibState != 1)
        {
            PXR_MotionTracking.StartMotionTrackerCalibApp();
        }

        MyLCM._instance.initLCM();
    }


    // Update is called once per frame
    void Update()
    {

        // 获取当前的追踪模式
        MotionTrackerMode trackingMode = PXR_MotionTracking.GetMotionTrackerMode();

        // 更新人体追踪位姿数据
        if (trackingMode == MotionTrackerMode.BodyTracking)
        {
            // 获取各个身体节点的位置和方向数据
            // int ret = PXR_MotionTracking.GetBodyTrackingData(ref bdi, ref bd);
            mDisplayTime = PXR_System.GetPredictedDisplayTime();
            var state = PXR_Input.GetBodyTrackingPose(mDisplayTime, ref m_BodyTrackerResult);

            // if (state == 1)
            {
                int euler_index = 0;
                int pos_index = 0;
                int rot_index = 0;
                picoLcmData.bodyData msg = new picoLcmData.bodyData();


                //原始数据
                for (int i = 0; i < 24; i++)
                {
                    Quaternion rotation1 = new Quaternion((float)m_BodyTrackerResult.trackingdata[i].localpose.RotQx, (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQy, (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQz, (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQw);
                    Vector3 euler1 = rotation1.eulerAngles;
                    if (euler1.x >= 180)
                    {
                        euler1.x -= 360;
                    }
                    if (euler1.y >= 180)
                    {
                        euler1.y -= 360;
                    }
                    if (euler1.z >= 180)
                    {
                        euler1.z -= 360;
                    }

                    msg.pos[pos_index * 3] = (float)m_BodyTrackerResult.trackingdata[i].localpose.PosX;
                    msg.pos[pos_index * 3 + 1] = (float)m_BodyTrackerResult.trackingdata[i].localpose.PosY;
                    msg.pos[pos_index * 3 + 2] = (float)m_BodyTrackerResult.trackingdata[i].localpose.PosZ;
                    pos_index += 1;

                    msg.euler[euler_index * 3] = (float)euler1.x;
                    msg.euler[euler_index * 3 + 1] = (float)euler1.y;
                    msg.euler[euler_index * 3 + 2] = (float)euler1.z;
                    euler_index += 1;

                    msg.rot[rot_index * 4] = (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQx;
                    msg.rot[rot_index * 4 + 1] = (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQy;
                    msg.rot[rot_index * 4 + 2] = (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQz;
                    msg.rot[rot_index * 4 + 3] = (float)m_BodyTrackerResult.trackingdata[i].localpose.RotQw;
                    rot_index += 1;

                }

                MyLCM._instance.bodyPublish(msg);
                            
            }
        }
    }

}
