using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace BattleFramework.Data{
    [System.Serializable]
    public class SysConfigConst {
        public const string bulletSpeed="bulletSpeed";//炮弹速度
        public const string DestroyerTowerSpeed="DestroyerTowerSpeed";//驱逐舰炮弹速度
        public const string BattleTowerSpeed="BattleTowerSpeed";//战列舰炮弹速度
        public const string CruiserTowerSpeed="CruiserTowerSpeed";//巡洋舰炮弹速度
        public const string DestroyerTowerProjectileFactor="DestroyerTowerProjectileFactor";//驱逐舰炮弹的曲线高度，越大曲线约明显
        public const string BattleTowerProjectileFactor="BattleTowerProjectileFactor";//战列舰炮弹的曲线高度，越大曲线约明显
        public const string CruiserTowerProjectileFactor="CruiserTowerProjectileFactor";//巡洋舰炮弹的曲线高度，越大曲线约明显
        public const string bulletProjectileFactor="bulletProjectileFactor";//炮弹的曲线高度，越大曲线约明显
        public const string planeBulletSpeed="planeBulletSpeed";//战斗机子弹移动速度
        public const string planeBombSpeed="planeBombSpeed";//轰炸机炸弹移动速度
        public const string antiBulletSpeed="antiBulletSpeed";//防空炮子弹移动速度
        public const string antiInterval ="antiInterval ";//防空炮发射间隔
        public const string torpedoSpeed="torpedoSpeed";//鱼雷速度
        public const string planeSpeed="planeSpeed";//飞机速度
        public const string shipSpeed="shipSpeed";//舰船速度
        public const string shipRandomSpeed="shipRandomSpeed";
        public const string maxCameraRotate="maxCameraRotate";
        public const string minCameraRoate="minCameraRoate";
        public const string maxCameraDistance="maxCameraDistance";
        public const string minCameraDistance="minCameraDistance";
        public const string defaultCameraDistance ="defaultCameraDistance ";
        public const string cameraZoomSpeed="cameraZoomSpeed";
        public const string cameraRotateSpeed="cameraRotateSpeed";
        public const string cameraMinTilt ="cameraMinTilt ";
        public const string cameraMaxTilt ="cameraMaxTilt ";
        public const string defaultTowerHitEffect="defaultTowerHitEffect";
        public const string defaultTowerUnHitEffect="defaultTowerUnHitEffect";
        public const string defaultTorpedoHitEffect="defaultTorpedoHitEffect";
        public const string defaultTorpedoUnHitEffect="defaultTorpedoUnHitEffect";
        public const string bigProjectileEffect="bigProjectileEffect";
        public const string defaultProjectileEffect="defaultProjectileEffect";
        public const string antiFirePulse="antiFirePulse";
        public const string planeBombEffect="planeBombEffect";
        public const string planeBulletEffect="planeBulletEffect";
        public const string torpedoEffect="torpedoEffect";
        public const string planeFlameEffect="planeFlameEffect";
        public const string planeExplosionEffect="planeExplosionEffect";
        public const string startFighterPlaneFlyDuring="startFighterPlaneFlyDuring";//航空战飞机飞行的时间，时间到后消失
        public const string startBombPlaneDelay="startBombPlaneDelay";
        public const string startPlaneSpeed="startPlaneSpeed";
        public const string startBomberAttackCount="startBomberAttackCount";//开场轰炸机丢弹数量
        public const string startBomberAttackInterval="startBomberAttackInterval";//开场轰炸机丢弹间隔
        public const string searchEnemyTime ="searchEnemyTime ";
        public const string viewPlaneBeforeTime ="viewPlaneBeforeTime ";
        public const string viewPlaneCamera ="viewPlaneCamera ";
        public const string bomberCameraTilt ="bomberCameraTilt ";
        public const string bomberCameraDistance="bomberCameraDistance";
        public const string bomberCameraRoate="bomberCameraRoate";
        public const string searchEnemyBeforeTime="searchEnemyBeforeTime";
        public const string shipForwardDistance ="shipForwardDistance ";
        public const string shipRightDistance0 ="shipRightDistance0 ";
        public const string shipRightDistance1 ="shipRightDistance1 ";
        public const string hunZhanTime ="hunZhanTime ";//混战时间
        public const string yeZhanTime ="yeZhanTime ";//夜战时间-从战斗开始
        public const string shipMoveIntervalTime ="shipMoveIntervalTime ";//船开始转向的时间间隔
    }
}
