using ICities;
using PropPrecision.Redirection;

namespace PropPrecision
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            Redirector<PropInstanceDetour>.Deploy();
        }

        public override void OnReleased()
        {
            base.OnReleased();
            Redirector<PropInstanceDetour>.Revert();
        }
    }
}