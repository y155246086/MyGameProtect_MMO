using UnityEngine;
using System.Collections;

public class EasyTouchControlsPanel : IViewBase {

    private ETCJoystick joystick;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.BackgroundLayer;
        joystick = Find<ETCJoystick>("PlayerJoystick");
    }

    protected override void OnShow(params object[] args)
    {
        
    }
    protected override void AddEventListener()
    {
        Mogo.Util.EventDispatcher.AddEventListener(GUIEvent.STOP_JOYSTICK_TURN, OnStopJoystickTurn);
        Mogo.Util.EventDispatcher.AddEventListener(GUIEvent.START_JOYSTICK_TURN, OnStartJoystickTurn);
    }

    private void OnStopJoystickTurn()
    {
        joystick.isTurnAndMove = false;
    }
    private void OnStartJoystickTurn()
    {
        joystick.isTurnAndMove = true;
    }
    protected override void RemoveEventListener()
    {
        Mogo.Util.EventDispatcher.RemoveEventListener(GUIEvent.STOP_JOYSTICK_TURN, OnStopJoystickTurn);
        Mogo.Util.EventDispatcher.RemoveEventListener(GUIEvent.START_JOYSTICK_TURN, OnStartJoystickTurn);
    }
    protected override void OnHide()
    {
        OnStartJoystickTurn();
    }
    protected override void OnDestory()
    {

    }
	
}
