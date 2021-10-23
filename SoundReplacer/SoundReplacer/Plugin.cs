using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace SoundReplacer
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static PluginConfig CurrentConfig { get; private set; }

        [Init]
        public void Init(Config config, IPALogger logger)
        {
            Instance = this;
            Log = logger;
            CurrentConfig = config.Generated<PluginConfig>();

            SoundLoader.GetSoundLists();
            var harmony = new Harmony("com.otiosum.BeatSaber.SoundReplacer");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnStart]
        public void OnApplicationStart()
        {
            new GameObject("SoundReplacerController").AddComponent<SoundReplacerController>();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            /**/
        }
    }
}
