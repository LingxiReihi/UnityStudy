using UnityEngine;

namespace Mirror.Examples.Pong
{
    /// <summary>
    /// Player 类，负责处理玩家的移动。
    /// 该类继承自 NetworkBehaviour，允许在多人环境中处理网络交互。
    /// </summary>
    public class Player : NetworkBehaviour
    {
        /// <summary>
        /// 玩家的移动速度。
        /// </summary>
        public float speed = 30;

        /// <summary>
        /// 玩家的 Rigidbody2D 组件，用于处理与物理相关的行为。
        /// </summary>
        public Rigidbody2D rigidbody2d;

        /// <summary>
        /// 固定更新方法，以固定的时间间隔调用。
        /// 此方法主要用于更新与物理相关的组件，如 Rigidbody，以确保与物理引擎同步。
        /// </summary>
        void FixedUpdate()
        {
            // 只允许本地玩家控制球拍。
            // 不控制其他玩家的球拍
            if (isLocalPlayer)
#if UNITY_6000_0_OR_NEWER
                // 设置 Rigidbody2D 组件的线性速度，使玩家可以根据键盘输入移动。
                rigidbody2d.linearVelocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
#else
                // 设置 Rigidbody2D 组件的速度，使玩家可以根据键盘输入移动。
                // 注意：此处使用的 API 根据 Unity 版本不同而不同。
                rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
#endif
        }
    }
}
