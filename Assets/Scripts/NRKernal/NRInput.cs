namespace NRKernal
{
    // ReSharper disable once InconsistentNaming
    public partial class NRInput
    {
        public static bool IsTouching()
        {
            return IsTouching(m_DomainHand);
        }

        private static bool IsTouching(ControllerHandEnum hand)
        {
            // return GetControllerState(hand).isTouching; // BUG always true on emulator
            return GetTouch(hand).sqrMagnitude > float.Epsilon * float.Epsilon;
        }
    }
}