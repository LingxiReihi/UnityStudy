using UnityEngine;

namespace Mirror.Examples.Pong
{
    public class Ball : NetworkBehaviour
    {
        public float speed = 30; // 球的速度
        public Rigidbody2D rigidbody2d; // 球的刚体组件

        public override void OnStartServer()
        {
            base.OnStartServer();

            // 仅在服务器上模拟球的物理行为
            rigidbody2d.simulated = true;

            // 从左侧玩家发球
#if UNITY_6000_0_OR_NEWER
            rigidbody2d.linearVelocity = Vector2.right * speed; // 设置线速度
#else
            rigidbody2d.velocity = Vector2.right * speed; // 设置速度
#endif
        }

        float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
            // ASCII 艺术：
            // ||  1 <- 在球拍顶部
            // ||
            // ||  0 <- 在球拍中间
            // ||
            // || -1 <- 在球拍底部
            return (ballPos.y - racketPos.y) / racketHeight; // 计算击球因子
        }

        // 仅在服务器上调用此方法
        [ServerCallback]
        void OnCollisionEnter2D(Collision2D col)
        {
            // 注意：'col' 包含碰撞信息。如果
            // 球与球拍发生碰撞，则：
            //   col.gameObject 是球拍
            //   col.transform.position 是球拍的位置
            //   col.collider 是球拍的碰撞器

            // 是否击中了球拍？如果是，则需要计算击球因子
            if (col.transform.GetComponent<Player>())
            {
                // 通过击球因子计算 y 方向
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // 通过相反的碰撞计算 x 方向
                float x = col.relativeVelocity.x > 0 ? 1 : -1;

                // 计算方向，并通过 .normalized 使长度为 1
                Vector2 dir = new Vector2(x, y).normalized;

                // 设置速度为 dir * speed
#if UNITY_6000_0_OR_NEWER
                rigidbody2d.linearVelocity = dir * speed; // 设置线速度
#else
                rigidbody2d.velocity = dir * speed; // 设置速度
#endif
            }
        }
    }
}