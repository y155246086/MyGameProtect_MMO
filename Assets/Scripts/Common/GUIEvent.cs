using UnityEngine;
using System.Collections;

public class GUIEvent{

    /// <summary>
    /// 资源更新过程的消息
    /// </summary>
    public static readonly string RESOURCE_UPDATE_MESSAGE = "RESOURCE_UPDATE_MESSAGE";
    /// <summary>
    /// 加载场景进度
    /// </summary>
    public static readonly string LOAD_SCENE_PROGRESS = "LOAD_SCENE_PROGRESS";
    //停止摇杆
    public static string STOP_JOYSTICK_TURN = "STOP_JOYSTICK_TURN";
    //开始摇杆
    public static string START_JOYSTICK_TURN = "START_JOYSTICK_TURN";
}
