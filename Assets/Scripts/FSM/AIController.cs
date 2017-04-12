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
        [SerializeField]
        private int health;
        
        //距离设定
        protected float chaseDistance = 20f;//追逐距离
        protected float attackDistance = 5f;//攻击距离
        protected float arriveDistance = 1f;//到达距离
        public SkillManager skillManager;
        public MonsterData data;
        public Vector3 BornPoint;//出生点
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
            
            health = 100;
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
        
        //public void Play(string parameterName, bool value)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    ar.SetBool(parameterName, value);

        //}
        //public void Play(string parameterName, float value)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    ar.SetFloat(parameterName, value);
        //}
        //public void Play(string parameterName, int value)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    ar.SetInteger(parameterName, value);
        //}
        //public void Play(string parameterName)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    ar.SetTrigger(parameterName);
        //}
        //public bool GetBool(string parameterName)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    return ar.GetBool(parameterName);
        //}
        //public int GetInt(string parameterName)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    return ar.GetInteger(parameterName);
        //}
        //public float GetFloat(string parameterName)
        //{
        //    var ar = this.GetComponent<UnityEngine.Animator>();
        //    return ar.GetFloat(parameterName);
        //}
        public void ChangeState(FSMStateType fSMStateType)
        {
            fsm.ChangeState(fSMStateType);
        }
    }
}

