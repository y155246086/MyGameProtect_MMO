using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using Mogo.Util;

public class EntityMyself : EntityPlayer
{
    //private ResourceData data;
    
    private AvatarModelData data;
    public CharacterController character;
    public static float preSkillTime;
    public EntityMyself()
    {
        spriteType = SpriteType.Myself;
    }
    protected override void OnCreateModel()
    {
        data = AvatarModelData.GetByID(1);
        if (data == null)
        {
            Debuger.LogError("数据Error null" + serverInfo.dataId);
            return;
        }
        gameObject = Res.ResourceManager.Instance.Instantiate<GameObject>(GameCommonUtils.GetResourceData(data.prefabName).resourcePath);

        transform = gameObject.transform;
        transform.tag = "Player";
        transform.gameObject.layer = 11;

        ActorMyself ap = gameObject.AddComponent<ActorMyself>();
        this.Motor = gameObject.AddComponent<MotorMyself>();
        this.Motor.theEntity = this;
        ap.theEntity = this;
        animator = gameObject.GetComponent<Animator>();
        this.Actor = ap;
        UpdatePosition();

        animator.applyRootMotion = false;

        
        gameObject.AddComponent<DontDestroyMe>();
        //ap.InitEquipment();
        ap.Equip(104001);
        ap.Equip(104002);

        if(data.scale>0)
        {
            ap.transform.localScale = Vector3.one*data.scale;
        }

        GameObject light = Res.ResourceManager.Instance.Instantiate<GameObject>("Gear/RoundLight");
        light.transform.parent = gameObject.transform;
        light.transform.localPosition = new Vector3(0, 1, 0);

        character = ap.GetComponent<CharacterController>();

    }
    protected override void OnEnterWorld()
    {
        GameWorld.thePlayer = this;
        battleManager = new PlayerBattleManager(this,skillManager);
        skillManager.AddSkill(3);
        skillManager.AddSkill(4);

        Mogo.Util.EventDispatcher.AddEventListener(GUIEvent.STOP_JOYSTICK_TURN, OnStopJoystickTurn);
        Mogo.Util.EventDispatcher.AddEventListener(GUIEvent.START_JOYSTICK_TURN, OnStartJoystickTurn);
    }

    private void OnStartJoystickTurn()
    {
        (Actor as ActorMyself).enableStick = true;
    }

    private void OnStopJoystickTurn()
    {
        (Actor as ActorMyself).enableStick = false;
    }
    protected override void OnLeaveWorld()
    {
        skillManager.Clear();
    }
    private void Stand()
    {
        if (GameWorld.inCity)
        {
            TimerHeap.AddTimer(500, 0, SetAction, -1);
        }
        //motor.enableStick = true;
    }
}
