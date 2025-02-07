using UnityEngine;

namespace qjklw.Data
{
    public class PlayerGroundData
    {
        public bool EnableRotate { get; set; } = true;
        public Vector2 MovementInput { get; set; }
        public Vector3 MovementDirection { get; set; }
        public float TargetRotationAngle { get; set; }
        public float RotationPassTime { get; set; }
        public float RotationTime { get; set; }
        
        public Vector3 CentripetalDirection { get; set; }

        private float rotationVelocityRef;
        public ref float RotationVelocityRef => ref rotationVelocityRef;
        
        public bool ShouldWalk { get; set; }
        public bool ShouldSprint { get; set; }
        
    }
}