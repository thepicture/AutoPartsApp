using System;

namespace AutoPartsApp.Services
{
    public interface INavigationService<TTarget>
    {
        TTarget CurrentTarget { get; }
        event Action Navigated;
        void NavigateBack();
        bool CanNavigateBack();
        void Navigate<TWhere>() where TWhere : TTarget;
        void NavigateWithParameter<TWhere, TParam>(TParam param) where TWhere : TTarget;
        void NavigateToRoot();
    }
}
