public enum GlobalEvents : int
{
    ChangeHp = 1, //角色血量改变
    ChangeLevel,  //角色等级改变
    ChangeForce,
    Death,        //角色死亡
    GetItem,      //角色获得道具
    GetTask,      
    FinishTask,
    OpenFunction,
    GetActivity,
    EnterInstance,
    LeaveInstance,
    OpenGUI,
    ButtonClick,
    SkillAvailable,
    TimeLimitEvent,
    Achiement,
    ArenicCredit
}
static public class UIEvent
{
    public readonly static string AddInstance = "";
    public readonly static string FllowTarget = "FllowTarget";
    public readonly static string QuitGame = "QuitGame";
    //拾取到物品
    public static string PICK_UP_ITEM = "PickUpItem";
    //AI寻路到达位置
    public static string AiTargetReached = "AiTargetReached";
    public static string ROLE_DEAD = "RoleDead";
    public static string GAME_SCENE_INITED = "GAME_SCENE_INITED";
}