using UnityEngine;
using UnityEngine.UI;
using System;
 
public class GameStart : MonoBehaviour
{
    //发送的消息变量
    private string msg = null;
    public RawImage rawImage;
    public Texture2D texture;
 
    void Start()
    {
        //连接服务器。
        NetManager.M_Instance.Connect("ws://192.168.50.252:8090");   //本机地址
        // texture = new Texture2D(1, 1);
        rawImage = this.GetComponent<RawImage> ();
        // rawImage.texture = null;
    }

    private void DisplayImageFromBase64StringOnMainThread(string base64String)
    {
        Debug.Log(base64String);
        if (base64String != null) {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            if (imageBytes != null) {
                texture.LoadImage(imageBytes);

            // 假设rawImageToDisplay是你用来显示图像的RawImage对象
                rawImage.texture = texture;
                Debug.Log("贴图：" + rawImage.texture);
            }
        }
    }

    void Update()
    {
        string data = NetManager.M_Instance.getData();
        DisplayImageFromBase64StringOnMainThread(data);
    }
 
    //绘制UI
    // private void OnGUI()
    // {
    //     //绘制输入框，以及获取输入框中的内容
    //     //PS：第二参数必须是msg，否则在我们输入后，虽然msg可以获得到输入内容，但马上就被第二参数在下一帧重新覆盖。
    //     msg = GUI.TextField(new Rect(10, 10, 100, 20), msg);
 
    //     //绘制按钮，以及按下发送信息按钮，发送信息
    //     if (GUI.Button(new Rect(120, 10, 80, 20), "发送信息") && msg != null)
    //     {
    //         NetManager.M_Instance.Send(msg);
    //     }
 
    //     //绘制按钮，以及按下断开连接按钮，发送断开连接请求
    //     if (GUI.Button(new Rect(210, 10, 80, 20), "断开连接"))
    //     {
    //         Debug.Log("向服务器请求断开连接......");
    //         NetManager.M_Instance.CloseClientWebSocket();
    //     }
        
    // }
}