using System;
using qjklw.Data;
using UnityEngine;

namespace qjklw
{
    [Serializable]
    public class PlayerPhysics
    {
        private Player player;
        private PlayerPhysicsData data;
        
        public PlayerPhysics(Player player) {
            this.player = player;
            data = player.PlayerSO.PhysicsData;
        }

        public void Initialize() {
            data.downVelocity = Vector3.zero;
        }

        public void Update() {
            ApplyGravity();
            UpdateDownForce();
        }

        

        #region Main Methods

        private void UpdateDownForce() {
            data.downVelocity.y -= data.Gravity * Time.deltaTime;
            if (player.PlayerController.isGrounded) {
                data.downVelocity.y = data.Gravity > 0f ? -2f : 0f;
            }
        }
        
        private void ApplyGravity() {
            player.PlayerController.Move(data.downVelocity * Time.deltaTime);
        }
        
        #endregion
    }
}