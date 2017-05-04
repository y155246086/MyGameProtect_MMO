#region 程序集 Common.dll, v1.0.0.0
// D:\BaiduYunDownload\暗黑战神CODE_7182H5ucf8l8q4KL\暗黑战神CODE\暗黑战神\client\Assets\Scripts\libs\Common.dll
#endregion

using System;

namespace Mogo.Util
{
    public static class Events
    {
        public static readonly string Unkown;

        public static class AIEvent
        {
            public static readonly string DummyStiffEnd;
            public static readonly string DummyThink;
            public static readonly string ProcessBossDie;
            public static readonly string SomeOneDie;
            public static readonly string WarnOtherSpawnPointEntities;
        }

        public static class ArenaEvent
        {
            public static readonly string AddArenaTimes;
            public static readonly string Challenge;
            public static readonly string ClearArenaCD;
            public static readonly string EnterArena;
            public static readonly string GetArenaReward;
            public static readonly string GetArenaRewardInfo;
            public static readonly string RefreshArenaData;
            public static readonly string RefreshRevenge;
            public static readonly string RefreshStrong;
            public static readonly string RefreshWeak;
            public static readonly string TabSwitch;
        }

        public static class AssistantEvent
        {
            public static readonly string ClientDragMarkResp;
            public static readonly string ClientDragSkillResp;
            public static readonly string LevelUpMarkResp;
            public static readonly string LevelUpSkillResp;
            public static readonly string MintmarkGridDragBegin;
            public static readonly string MintmarkGridDragOutside;
            public static readonly string MintmarkGridDragToBodyGrid;
            public static readonly string PropRefreshResp;
            public static readonly string SkillGridDragBegin;
            public static readonly string SkillGridDragOutside;
            public static readonly string SkillGridDragToBodyGrid;
        }

        public static class CampaignEvent
        {
            public static readonly string CrystalAttacked;
            public static readonly string ExitCampaign;
            public static readonly string FlushPlayerBlood;
            public static readonly string GetCampaignLastTime;
            public static readonly string GetCampaignLeftTimes;
            public static readonly string JoinCampaign;
            public static readonly string LeaveCampaign;
            public static readonly string MatchCampaign;
            public static readonly string RemovePlayerMessage;
            public static readonly string SetPlayerMessage;
        }

        public static class ChallengeUIEvent
        {
            public static readonly string CollectChallengeState;
            public static readonly string Enter;
            public static readonly string FlushChallengeUIGridSortedResult;
            public static readonly string GetOgreMustDieTime;
            public static readonly string ReceiveChallengeUIGridMessage;
        }

        public static class ComboEvent
        {
            public static readonly string AddCombo;
            public static readonly string ResetCombo;
        }

        public static class CommandEvent
        {
            public static readonly string CommandEnd;
        }

        public static class ComposeManagerEvent
        {
            public static readonly string SwitchToCompose;
        }

        public static class DailyTaskEvent
        {
            public static readonly string DailyTaskJumpToOtherUI;
            public static readonly string GetDailyEventData;
            public static readonly string GetDailyEventReward;
            public static readonly string OpenDailyTaskUI;
            public static readonly string ShowDailyEvent;
        }

        public static class DiamondToGoldEvent
        {
            public static readonly string GoldMetallurgy;
        }

        public static class DirecterEvent
        {
            public static readonly string DirActive;
        }

        public static class EnergyEvent
        {
            public static readonly string BuyEnergy;
            public static readonly string UpdateVipLevel;
        }

        public static class EquipmentEvent
        {
            public static readonly string SetEquipmentUICloseValueZ;
        }

        public static class FoggyAbyssEvent
        {
            public static readonly string FoggyAbyssClose;
            public static readonly string FoggyAbyssOpen;
        }

        public static class FrameWorkEvent
        {
            public static readonly string AOIDelEvtity;
            public static readonly string AOINewEntity;
            public static readonly string BaseLogin;
            public static readonly string CheckDef;
            public static readonly string DefuseLogin;
            public static readonly string EntityAttached;
            public static readonly string EntityCellAttached;
            public static readonly string Login;
            public static readonly string ReConnectKey;
            public static readonly string ReConnectRefuse;
        }

        public static class FSMMotionEvent
        {
            public static readonly string OnAttackingEnd = "OnAttackingEnd";
            public static readonly string OnHit = "OnHit";
            public static readonly string OnHitAnimEnd = "OnHitAnimEnd";
            public static readonly string OnPrepareEnd = "OnPrepareEnd";
            public static readonly string OnRollEnd = "OnRollEnd";
        }

        public static class GearEvent
        {
            public static readonly string ChestBroken;
            public static readonly string CongealMagma;
            public static readonly string CrockBroken;
            public static readonly string Damage;
            public static readonly string DownloadAllGear;
            public static readonly string FlushGearState;
            public static readonly string LiftEnter;
            public static readonly string LoadEnd;
            public static readonly string MotorHandleEnd;
            public static readonly string PathPointTrigger;
            public static readonly string SetGearDisable;
            public static readonly string SetGearEnable;
            public static readonly string SetGearEventDisable;
            public static readonly string SetGearEventEnable;
            public static readonly string SetGearEventStateOne;
            public static readonly string SetGearEventStateTwo;
            public static readonly string SetGearStateOne;
            public static readonly string SetGearStateTwo;
            public static readonly string SpawnPointDead;
            public static readonly string SwitchLightMapFog;
            public static readonly string Teleport;
            public static readonly string TrapBegin;
            public static readonly string TrapEnd;
            public static readonly string UploadAllGear;
        }

        public static class IAPConsumeEvent
        {
            public static readonly string OpenIAPConsumeUI;
        }

        public static class InstanceEvent
        {
            public static readonly string AddFriendDegree;
            public static readonly string BeforeInstanceLoaded;
            public static readonly string EnterRandomMission;
            public static readonly string GetBossChestRewardReq;
            public static readonly string GetChestReward;
            public static readonly string GetCurrentReward;
            public static readonly string GetMercenaryInfo;
            public static readonly string GetSweepMissionList;
            public static readonly string GetSweepTimes;
            public static readonly string InstanceLoaded;
            public static readonly string InstanceSelected;
            public static readonly string InstanceUnLoaded;
            public static readonly string MissionStart;
            public static readonly string NotReborn;
            public static readonly string Reborn;
            public static readonly string ResetMission;
            public static readonly string ReturnHome;
            public static readonly string SpawnPointStart;
            public static readonly string StopAutoFight;
            public static readonly string SweepMission;
            public static readonly string UpdateEnterableMissions;
            public static readonly string UpdateMap;
            public static readonly string UpdateMissionMessage;
            public static readonly string UpdateMissionStars;
            public static readonly string UpdateMissionTimes;
            public static readonly string UploadMaxCombo;
            public static readonly string WinReturnHome;
        }

        public static class InstanceUIEvent
        {
            public static readonly string AutoFlipCard;
            public static readonly string AutoFlipRestCard;
            public static readonly string CheckMissionTimes;
            public static readonly string EnterFoggyAbyss;
            public static readonly string FlipCard;
            public static readonly string FlipRestCard;
            public static readonly string GetBossChestRewardGotMessage;
            public static readonly string GetChestRewardGotMessage;
            public static readonly string GetDrops;
            public static readonly string GetFoggyAbyssMessage;
            public static readonly string ShowCard;
            public static readonly string ShowResetMissionWindow;
            public static readonly string UpdateBossChestMessage;
            public static readonly string UpdateChestMessage;
            public static readonly string UpdateLevelEnable;
            public static readonly string UpdateLevelRecord;
            public static readonly string UpdateLevelStar;
            public static readonly string UpdateLevelTime;
            public static readonly string UpdateMapName;
            public static readonly string UpdateMercenaryButton;
            public static readonly string UpdateMissionEnable;
            public static readonly string UpdateMissionName;
            public static readonly string UpdateMissionStar;
        }

        public static class LocalServerEvent
        {
            public static readonly string ExitMission;
            public static readonly string SummonToken;
        }

        public static class LogicSoundEvent
        {
            public static readonly string OnHitYelling = "OnHitYelling";
        }

        public static class MFUIManagerEvent
        {
            public static readonly string SwitchUIWithLoad;
        }

        public static class MogoGlobleUIManagerEvent
        {
            public static readonly string ShowWaitingTip;
        }

        public static class MogoUIManagerEvent
        {
            public static readonly string SetCurrentUI;
            public static readonly string ShowDiamondToGoldUI;
            public static readonly string ShowEnergyUI;
            public static readonly string ShowInstanceMissionChooseUI;
            public static readonly string SwitchStrenthUI;
            public static readonly string SwitchToMarket;
        }

        public static class MonsterEvent
        {
            public static readonly string TowerDamage;
        }

        public static class NetworkEvent
        {
            public static readonly string Connect;
            public static readonly string OnClose;
            public static readonly string OnConnected;
            public static readonly string OnDataRecv;
        }

        public static class NormalMainUIEvent
        {
            public static readonly string HideArenaIconTip;
            public static readonly string HideChallegeIconTip;
            public static readonly string ShowArenaIconTip;
            public static readonly string ShowChallegeIconTip;
            public static readonly string ShowMallConsumeIconTip;
        }

        public static class NormalMainUIViewManagerEvent
        {
            public static readonly string PVEPLAYICONUP;
            public static readonly string PVPPLAYICONUP;
        }

        public static class NPCEvent
        {
            public static readonly string FrushIcon;
            public static readonly string TalkEnd;
            public static readonly string TurnToPlayer;
        }

        public static class OccupyTowerEvent
        {
            public static readonly string ExitOccupyTower;
            public static readonly string GetOccupyTowerStatePoint;
            public static readonly string JoinOccupyTower;
            public static readonly string LeaveOccupyTower;
            public static readonly string SetOccupyTowerUIScorePoint;
        }

        public static class OperationEvent
        {
            public static readonly string AchievementGetReward;
            public static readonly string AchievementShareToGetDiamond;
            public static readonly string Charge;
            public static readonly string ChargeGetReward;
            public static readonly string CheckEventOpen;
            public static readonly string CheckFirstShow;
            public static readonly string EventGetReward;
            public static readonly string EventShareToGetDiamond;
            public static readonly string EventTimesUp;
            public static readonly string FlushCharge;
            public static readonly string GetAchievementMessage;
            public static readonly string GetActivityMessage;
            public static readonly string GetAllActivity;
            public static readonly string GetChargeRewardMessage;
            public static readonly string GetLoginMarket;
            public static readonly string GetLoginMessage;
            public static readonly string LogInBuy;
            public static readonly string LogInGetReward;
        }

        public static class OtherEvent
        {
            public static readonly string BossDie;
            public static readonly string CallTeammate;
            public static readonly string ChangeDummyRate;
            public static readonly string Charge;
            public static readonly string CheckCharge;
            public static readonly string ClientGM;
            public static readonly string DiamondMine;
            public static readonly string MainCameraComplete;
            public static readonly string MapIdChanged;
            public static readonly string OnChangeWeapon = "OnChangeWeapon";
            public static readonly string OnEvent1;
            public static readonly string OnEvent2;
            public static readonly string OnEvent3;
            public static readonly string OnThink;
            public static readonly string ResetDummyRate;
            public static readonly string SecondPast;
            public static readonly string Withdraw;
        }

        public static class RewardEvent
        {
            public static readonly string ChargeReward;
            public static readonly string ElfDiamond;
            public static readonly string GetChargeReward;
            public static readonly string GetLoginReward;
            public static readonly string LoginReward;
            public static readonly string OpenReward;
            public static readonly string SelectReward;
            public static readonly string WingIcon;
        }

        public static class RuneEvent
        {
            public static readonly string AutoCombine;
            public static readonly string AutoPickUp;
            public static readonly string ChangeIndex;
            public static readonly string ChangePosi;
            public static readonly string CloseDragon;
            public static readonly string FullRefresh;
            public static readonly string GameMoneyRefresh;
            public static readonly string GetBodyRunes;
            public static readonly string GetRuneBag;
            public static readonly string PutDown;
            public static readonly string PutOn;
            public static readonly string RMBRefresh;
            public static readonly string ShowTips;
            public static readonly string UseRune;
        }

        public static class SanctuaryEvent
        {
            public static readonly string BuyExtraTime;
            public static readonly string CanBuyExtraTime;
            public static readonly string EnterSanctuary;
            public static readonly string QuerySanctuaryInfo;
            public static readonly string RefreshMyInfo;
            public static readonly string RefreshRank;
        }

        public static class SpellEvent
        {
            public static readonly string OpenView;
            public static readonly string SelectGroup;
            public static readonly string SelectLevel;
            public static readonly string Study;
        }

        public static class StoryEvent
        {
            public static readonly string CGBegin;
            public static readonly string CGEnd;
        }

        public static class TaskEvent
        {
            public static readonly string AcceptNewTask;
            public static readonly string AcceptTask;
            public static readonly string CheckNpcInRange;
            public static readonly string CloseToNPC;
            public static readonly string GoToNextTask;
            public static readonly string GuideDone;
            public static readonly string LeaveFromNPC;
            public static readonly string LevelWin;
            public static readonly string NPCInSight;
            public static readonly string NPCSetSign;
            public static readonly string ShowRewardEnd;
            public static readonly string TalkEnd;
        }

        public static class TowerEvent
        {
            public static readonly string ClearCD;
            public static readonly string CreateDoor;
            public static readonly string EnterMap;
            public static readonly string FinishSingle;
            public static readonly string GetInfo;
            public static readonly string NormalSweep;
            public static readonly string SweepAll;
            public static readonly string VIPSweep;
        }

        public static class UIAccountEvent
        {
            public static readonly string OnChangeServer;
            public static readonly string OnChooseServer;
            public static readonly string OnCreateCharacter;
            public static readonly string OnDelCharacter;
            public static readonly string OnGetRandomName;
            public static readonly string OnLogin;
            public static readonly string OnStartGame;
        }

        public static class UIBattleEvent
        {
            public static readonly string OnFlushBossBlood;
            public static readonly string OnFlushMercenaryBlood;
            public static readonly string OnNormalAttack;
            public static readonly string OnPowerChargeComplete;
            public static readonly string OnPowerChargeInterrupt;
            public static readonly string OnPowerChargeStart;
            public static readonly string OnResetPowerCharge;
            public static readonly string OnSpellOneAttack;
            public static readonly string OnSpellThreeAttack;
            public static readonly string OnSpellTwoAttack;
            public static readonly string OnSpellXPAttack;
            public static readonly string OnSpriteSkill;
            public static readonly string OnUseItem;
        }

        public static class WingEvent
        {
            public static readonly string Active;
            public static readonly string Buy;
            public static readonly string Close;
            public static readonly string ClosePreview;
            public static readonly string CommonWing;
            public static readonly string MagicWing;
            public static readonly string Open;
            public static readonly string OpenBuy;
            public static readonly string OpenTip;
            public static readonly string OpenUpgrade;
            public static readonly string PutOn;
            public static readonly string TipBuyClick;
            public static readonly string Undo;
            public static readonly string UnLock;
            public static readonly string Upgrade;
        }
    }
}
