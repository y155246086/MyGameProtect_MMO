using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Pathfinding.RVO.RVOSimulator))]
    [RequireComponent(typeof(Pathfinding.FunnelModifier))]
    public class AIController : SpriteBase
    {
        //距离设定
        protected float chaseDistance = 20f;//追逐距离
        protected float attackDistance = 5f;//攻击距离
        protected float arriveDistance = 1f;//到达距离
        public SkillManager skillManager;
        public MonsterData data;
        protected AdvanceFSM fsm;
        /// <summary>
        /// 追逐距离
        /// </summary>
        public virtual float ChaseDistance
        {
            get { return chaseDistance; }
        }
        /// <summary>
        /// 攻击距离
        /// </summary>
        public virtual float AttackDistance
        {
            get { return attackDistance; }
        }
        /// <summary>
        /// 到达距离
        /// </summary>
        public virtual float ArriveDistance
        {
            get { return arriveDistance; }
        }
        public Transform playerTransfrom
        {
            get
            {
                GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
                if (objPlayer == null)
                {
                    Debug.LogError("找不到玩家");
                }
                return objPlayer.transform;
            }
        }
      
        // Use this for initialization

        protected override void Initialize()
        {
            base.Initialize();
            spriteType = SpriteType.Monster;
            fsm = new AdvanceFSM(this);
            
            ConstructFSM();
        }
        /// <summary>
        /// 状态机初始化
        /// </summary>
        private void ConstructFSM()
        {
            fsm.AddFSMState(FSMStateType.Patroling, new PatrolBase(this.transform));
            fsm.AddFSMState(FSMStateType.Chasing, new ChaseBase(this.transform));
            fsm.AddFSMState(FSMStateType.Attacking, new AttackBase(this.transform));
            fsm.AddFSMState(FSMStateType.Dead, new DeadBase(this.transform));
        }
        protected override void FSMUpdate()
        {
            base.FSMUpdate();
            fsm.FSMUpdate();
        }
        protected override void FSMFixedUpdate()
        {
            base.FSMFixedUpdate();
            if (propertyManager.GetPropertyValue(PropertyType.HP) <= 0)
            {
                fsm.ChangeState(FSMStateType.Dead);
                return;
            }
            fsm.FSMFixedUpdate();
        }
        
        
        public void ChangeState(FSMStateType fSMStateType)
        {
            fsm.ChangeState(fSMStateType);
        }
    }
}

