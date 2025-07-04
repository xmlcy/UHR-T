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
            int ret = PXR_MotionTracking.GetBodyTrackingData(ref bdi, ref bd);

            int euler_index = 0;
            int pos_index = 0;
            int rot_index = 0;
            picoLcmData.bodyData msg = new picoLcmData.bodyData();


            //原始数据
            for (int i = 0; i < 24; i++)
            {
                Quaternion rotation1 = new Quaternion((float)bd.roleDatas[i].localPose.RotQx, (float)bd.roleDatas[i].localPose.RotQy, (float)bd.roleDatas[i].localPose.RotQz, (float)bd.roleDatas[i].localPose.RotQw);
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

                msg.pos[pos_index * 3] = (float)bd.roleDatas[i].localPose.PosX;
                msg.pos[pos_index * 3 + 1] = (float)bd.roleDatas[i].localPose.PosY;
                msg.pos[pos_index * 3 + 2] = (float)bd.roleDatas[i].localPose.PosZ;
                pos_index = pos_index + 1;

                msg.euler[euler_index * 3] = (float)euler1.x;
                msg.euler[euler_index * 3 + 1] = (float)euler1.y;
                msg.euler[euler_index * 3 + 2] = (float)euler1.z;
                euler_index = euler_index + 1;

                msg.rot[rot_index * 4] = (float)bd.roleDatas[i].localPose.RotQx;
                msg.rot[rot_index * 4 + 1] = (float)bd.roleDatas[i].localPose.RotQy;
                msg.rot[rot_index * 4 + 2] = (float)bd.roleDatas[i].localPose.RotQz;
                msg.rot[rot_index * 4 + 3] = (float)bd.roleDatas[i].localPose.RotQw;
                rot_index = rot_index + 1;

            }

            MyLCM._instance.bodyPublish(msg);
        }
    }

}
