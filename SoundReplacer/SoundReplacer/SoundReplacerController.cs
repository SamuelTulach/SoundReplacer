using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;

namespace SoundReplacer
{
    public class SoundReplacerController : MonoBehaviour
    {
        public static SoundReplacerController Instance { get; private set; }

        private ReplacerFlowCoordinator _flowCoordinator;

        private void Awake()
        {
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            Instance = this;

            MenuButtons.instance.RegisterButton(new MenuButton("SoundReplacer", "Setup SoundReplacer here!", MenuButtonPressed, true));
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        private void MenuButtonPressed()
        {
            if (_flowCoordinator == null)
                _flowCoordinator = BeatSaberMarkupLanguage.BeatSaberUI.CreateFlowCoordinator<ReplacerFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinatorOrAskForTutorial(_flowCoordinator);
        }
    }
}
