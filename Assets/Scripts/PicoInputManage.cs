using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR;
using LCM;
using LCM.Examples;

public class PicoInputManage : MonoBehaviour
{
    private double mDisplayTime;
    private InputDevice headsetDevice;

    // Start is called before the first frame update
    void Start()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.Head, devices);

        if (devices.Count > 0)
        {
            headsetDevice = devices[0];
        }
        MyLCM._instance.initLCM(); 
    }

    // Update is called once per frame
    void Update()
    {
        picoLcmData.picoData msg = new picoLcmData.picoData();

        mDisplayTime = PXR_System.GetPredictedDisplayTime();
        var rotl = PXR_Input.GetControllerPredictRotation(PXR_Input.Controller.LeftController, mDisplayTime);
        var posl = PXR_Input.GetControllerPredictPosition(PXR_Input.Controller.LeftController, mDisplayTime);
        var rotr = PXR_Input.GetControllerPredictRotation(PXR_Input.Controller.RightController, mDisplayTime);
        var posr = PXR_Input.GetControllerPredictPosition(PXR_Input.Controller.RightController, mDisplayTime);

        Vector2 primary2DAxisRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primary2DAxisRight);

        Vector2 primary2DAxisLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primary2DAxisLeft);

        bool primary2DAxisClickRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClickRight);

        bool primary2DAxisClickLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClickLeft);

        bool triggerButtonValueLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerButtonValueLeft);
        float triggerValueLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerValueLeft);

        bool triggerButtonValueRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerButtonValueRight);
        float triggerValueRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerValueRight);

        bool gripButtonValueLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripButtonValueLeft);
        float gripValueLeft;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out gripValueLeft);

        bool gripButtonValueRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripButtonValueRight);
        float gripValueRight;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out gripValueRight);

        bool primaryButtonX;
        int priX = 0;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonX);
        if (primaryButtonX)
        {
            priX = 1;
        }

        bool primaryButtonA;
        int priA = 0;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonA);
        if (primaryButtonA)
        {
            priA = 1;
        }

        bool primaryButtonY;
        int priY = 0;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out primaryButtonY);
        if (primaryButtonY)
        {
            priY = 1;
        }

        bool primaryButtonB;
        int priB = 0;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out primaryButtonB);
        if (primaryButtonB)
        {
            priB = 1;
        }

        msg.primary2DAxisRight_x = primary2DAxisRight.x;
        msg.primary2DAxisRight_y = primary2DAxisRight.y;
        msg.primary2DAxisLeft_x = primary2DAxisLeft.x;
        msg.primary2DAxisLeft_y = primary2DAxisLeft.y;
        msg.primary2DAxisClickRight = primary2DAxisClickRight;
        msg.primary2DAxisClickLeft = primary2DAxisClickLeft;
        msg.triggerButtonValueLeft = triggerButtonValueLeft;
        msg.triggerButtonValueRight = triggerButtonValueRight;
        msg.triggerValueLeft = triggerValueLeft;
        msg.triggerValueRight = triggerValueRight;
        msg.gripButtonValueLeft = gripButtonValueLeft;
        msg.gripButtonValueRight = gripButtonValueRight;
        msg.gripValueLeft = gripValueLeft;
        msg.gripValueRight = gripValueRight;
        msg.primaryButtonA = primaryButtonA;
        msg.primaryButtonB = primaryButtonB;
        msg.primaryButtonX = primaryButtonX;
        msg.primaryButtonY = primaryButtonY;

        Vector3 euler2 = rotl.eulerAngles;
        if (euler2.x >= 180)
        {
            euler2.x -= 360;
        }
        if (euler2.y >= 180)
        {
            euler2.y -= 360;
        }
        if (euler2.z >= 180)
        {
            euler2.z -= 360;
        }
        Vector3 euler3 = rotr.eulerAngles;
        if (euler3.x >= 180)
        {
            euler3.x -= 360;
        }
        if (euler3.y >= 180)
        {
            euler3.y -= 360;
        }
        if (euler3.z >= 180)
        {
            euler3.z -= 360;
        }

        msg.hand_pos[0] = posl.x;
        msg.hand_pos[1] = posl.y;
        msg.hand_pos[2] = posl.z;
        msg.hand_pos[3] = posr.x;
        msg.hand_pos[4] = posr.y;
        msg.hand_pos[5] = posr.z;

        msg.hand_euler[0] = euler2.x;
        msg.hand_euler[1] = euler2.y;
        msg.hand_euler[2] = euler2.z;
        msg.hand_euler[3] = euler3.x;
        msg.hand_euler[4] = euler3.y;
        msg.hand_euler[5] = euler3.z;

        msg.hand_rot[0] = rotl.x;
        msg.hand_rot[1] = rotl.y;
        msg.hand_rot[2] = rotl.z;
        msg.hand_rot[3] = rotl.w;
        msg.hand_rot[4] = rotr.x;
        msg.hand_rot[5] = rotr.y;
        msg.hand_rot[6] = rotr.z;
        msg.hand_rot[7] = rotr.w;

        if (headsetDevice.isValid) {
            if (headsetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position)) {
                msg.head_pos[0] = position.x;
                msg.head_pos[1] = position.y;
                msg.head_pos[2] = position.z;
                transform.position = position;
            }
            if (headsetDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation)) {
                msg.head_euler[0] = rotation.eulerAngles.x;
                msg.head_euler[1] = rotation.eulerAngles.y;
                msg.head_euler[2] = rotation.eulerAngles.z;
                msg.head_rot[0] = rotation.w;
                msg.head_rot[1] = rotation.y;
                msg.head_rot[2] = rotation.z;
                msg.head_rot[3] = rotation.x;
                transform.rotation = rotation;
            }
        }

        MyLCM._instance.picoPublish(msg);
    }
}
