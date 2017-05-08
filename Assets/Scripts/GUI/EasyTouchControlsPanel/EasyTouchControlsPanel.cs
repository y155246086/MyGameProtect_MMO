using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyTouchControlsPanel : IViewBase {

    private ETCJoystick joystick;
    private CDController skillCDController01;
    private CDController skillCDController02;
    protected override void OnStart()
    {
        uiLayer = UIPanelLayers.BackgroundLayer;
        joystick = Find<ETCJoystick>("PlayerJoystick");
        skillCDController01 = panelObj.AddComponent<CDController>();
        PlayerSkillManager skillManager = GameWorld.thePlayer.skillManager as PlayerSkillManager;
        skillCDController01.SetCDContent(Find<Image>("Skill1/ButtonAttack1/CDMask"), Find<Text>("Skill1/ButtonAttack1/CDText"), skillManager.GetSpellOneID(), skillManager);
        skillCDController01.IsRun = true;


        skillCDController02 = panelObj.AddComponent<CDController>();
        skillCDController02.SetCDContent(Find<Image>("Skill2/ButtonAttack2/CDMask"), Find<Text>("Skill2/ButtonAttack2/CDText"), skillManager.GetSpellTwoID(), skillManager);
        skillCDController02.IsRun = true;
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
        skillCDController01.IsRun = false;
    }
    protected override void OnDestory()
    {

    }
	
}
