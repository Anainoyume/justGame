namespace qjklw.CustomType
{
    public struct Transition
    {
        public float CrossFadeTime;
        public float OffsetTime;

        public Transition(float crossFadeTime, float offsetTime) {
            CrossFadeTime = crossFadeTime;
            OffsetTime = offsetTime;
        }
    }
}