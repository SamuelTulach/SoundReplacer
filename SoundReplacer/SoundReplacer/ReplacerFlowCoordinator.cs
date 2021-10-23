using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;

namespace SoundReplacer
{
    internal class ReplacerFlowCoordinator : FlowCoordinator
    {
        private ReplacerSettingsView _settingsView;

        public void Awake()
        {
            if (_settingsView == null)
                _settingsView = BeatSaberUI.CreateViewController<ReplacerSettingsView>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation) 
                return;

            SetTitle("SoundReplacer");
            showBackButton = true;
            ProvideInitialViewControllers(_settingsView);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            var mainFlow = BeatSaberUI.MainFlowCoordinator;
            mainFlow.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }
    }
}