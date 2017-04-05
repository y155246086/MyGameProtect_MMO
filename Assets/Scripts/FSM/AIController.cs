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
    public class AIController : AdvanceFSM
    {
        [SerializeField]
        private int health;
        //距离设定
        protected float chaseDistance = 20f;//追逐距离
        protected float attackDistance = 5f;//攻击距离
        protected float arriveDistance = 1f;//到达距离
        public SkillManager skillManager;
        public PropertyManager propertyManager;
        public MonsterData data;
        public Vector3 BornPoint;//出生点
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
        protected override void Initialize()
        {
            base.Initialize();
            propertyManager = new PropertyManager();
            health = 100;
            GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
            playerTransfrom = objPlayer.transform;
            if (playerTransfrom == null)
            {
                Debug.LogError("找不到玩家");
            }
            ConstructFSM();
        }
        /// <summary>
        /// 状态机初始化
        /// </summary>
        private void ConstructFSM()
        {
            AddFSMState(FSMStateType.Patroling, new PatrolBase(this.transform));
            AddFSMState(FSMStateType.Chasing, new ChaseBase(this.transform));
            AddFSMState(FSMStateType.Attacking, new AttackBase(this.transform));
            AddFSMState(FSMStateType.Dead, new DeadBase(this.transform));
        }
        protected override void FSMUpdate()
        {
            base.FSMUpdate();
            CurrentState.OnUpdate(playerTransfrom);
        }
        protected override void FSMFixedUpdate()
        {
            base.FSMFixedUpdate();
            if (propertyManager.GetPropertyValue(PropertyType.HP)<= 0)
            {
                ChangeState(FSMStateType.Dead);
                return;
            }
            CurrentState.OnFiexedUpdate(playerTransfrom);
        }
    }
}

