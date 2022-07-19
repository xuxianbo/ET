using UnityEngine;

namespace ET.Client
{
    public static class UnitFactory
    {
        public static Unit CreateUnit(Scene currentScene, long id, int configId)
        {
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.AddChildWithId<Unit, int>(id, configId);
            unitComponent.Add(unit);

            return unit;
        }

        public static Unit CreatePlayerHero(Scene currentScene, UnitInfo unitInfo)
        {
            Unit unit = CreateUnit(currentScene, unitInfo.UnitId, unitInfo.ConfigId);

            unit.AddComponent<NumericComponent>();
            unit.AddComponent<MoveComponent>();
            unit.AddComponent<StackFsmComponent>();

            unit.AddComponent<NP_SyncComponent>();
            unit.AddComponent<NP_RuntimeTreeManager>();

            //增加Buff管理组件
            unit.AddComponent<BuffManagerComponent>();
            unit.AddComponent<SkillCanvasManagerComponent>();
            unit.AddComponent<B2S_RoleCastComponent, RoleCamp, RoleTag>(unitInfo.RoleCamp, RoleTag.Hero);

            unit.AddComponent<CommonAttackComponent_Logic>();
            unit.AddComponent<CastDamageComponent>();
            unit.AddComponent<ReceiveDamageComponent>();

            unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
            unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);

            Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate_Logic()
            {
                UnitConfigId = unitInfo.ConfigId,
                UnitName = "还是我的Darius"
            });

            return unit;
        }

        /// <summary>
        /// 创建碰撞体
        /// </summary>
        /// <param name="room">归属的房间</param>
        /// <param name="belongToUnit">归属的Unit</param>
        /// <param name="colliderDataConfigId">碰撞体数据表Id</param>
        /// <param name="collisionRelationDataConfigId">碰撞关系数据表Id</param>
        /// <param name="colliderNPBehaveTreeIdInExcel">碰撞体的行为树Id</param>
        /// <returns></returns>
        public static Unit CreateSpecialColliderUnit(Scene currentScene, CreateColliderArgs createColliderArgs)
        {
            //为碰撞体新建一个Unit
            Unit b2sColliderEntity =
                CreateUnit(currentScene, IdGenerater.Instance.GenerateUnitId(currentScene.Zone), 0);

            b2sColliderEntity.AddComponent<NP_SyncComponent>();
            b2sColliderEntity.AddComponent<B2S_ColliderComponent, CreateColliderArgs>(createColliderArgs);
            b2sColliderEntity.AddComponent<NP_RuntimeTreeManager>();
            b2sColliderEntity.AddComponent<SkillCanvasManagerComponent>();

            // // 根据传过来的行为树Id来给这个碰撞Unit加上行为树(如果有的话)
            // // 如果不加行为树，那么在碰撞体发生碰撞时将会直接传递给BelongToUnit进行处理
            // NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(b2sColliderEntity,
            //         SkillCanvasConfigCategory.Instance.Get(colliderNPBehaveTreeIdInExcel).NPBehaveId,
            //         SkillCanvasConfigCategory.Instance.Get(colliderNPBehaveTreeIdInExcel).BelongToSkillId)
            //     .Start();

            return b2sColliderEntity;
        }

        public class CreateColliderArgs : IReference
        {
            public string PrefabABPath;
            
            public int NP_TreeConfigId;

            public Unit BelontToUnit;

            /// <summary>
            /// 将要发生碰撞事件的Tag
            /// </summary>
            public RoleTag TargetCollsionRoleTag;

            /// <summary>
            /// 将要发生碰撞事件的Cast
            /// </summary>
            public RoleCast TargetCollsionRoleCast;

            /// <summary>
            /// 将要发生碰撞事件的Camp
            /// </summary>
            public RoleCamp TargetCollsionRoleCamp;

            public bool FollowUnit;

            public Vector3 Offset;

            public Vector3 TargetPos;

            public float Angle;

            /// <summary>
            /// 碰撞开始时黑板键
            /// </summary>
            public string OnTriggerEnter;

            /// <summary>
            /// 碰撞持续时黑板键
            /// </summary>
            public string OnTriggerStay;

            /// <summary>
            /// 碰撞结束时黑板键
            /// </summary>
            public string OnTriggerExit;

            public CreateColliderArgs Init(string prefabABPath, int npTreeConfigId, Unit belongToUnit, float angle,
                bool followUnit,
                RoleCast targetCollsionRoleCast, RoleCamp targetCollsionRoleCamp, RoleTag targetCollsionRoleTag,
                Vector3 offset, Vector3 targetPos)
            {
                BelontToUnit = belongToUnit;
                Angle = angle;
                FollowUnit = followUnit;
                TargetCollsionRoleCamp = targetCollsionRoleCamp;
                TargetCollsionRoleTag = targetCollsionRoleTag;
                TargetCollsionRoleCast = targetCollsionRoleCast;
                Offset = offset;
                TargetPos = targetPos;
                return this;
            }

            public void Clear()
            {
                BelontToUnit = null;
                TargetCollsionRoleCamp = RoleCamp.Player;
                TargetCollsionRoleTag = RoleTag.Hero;
                TargetCollsionRoleCast = RoleCast.Friendly;
                FollowUnit = false;
                Offset = Vector3.zero;
                TargetPos = Vector3.zero;

                Angle = 0;
                OnTriggerEnter = string.Empty;
                OnTriggerStay = string.Empty;
                OnTriggerExit = string.Empty;
            }
        }
    }
}