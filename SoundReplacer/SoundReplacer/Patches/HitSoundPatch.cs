using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace SoundReplacer.Patches
{
    public class HitSoundPatch
    {
        private static List<AudioClip> _originalBadSounds;
        private static List<AudioClip> _originalGoodLongSounds;
        private static List<AudioClip> _originalGoodShortSounds;

        private static AudioClip[] _lastBadAudioClips;
        private static string _lastBadSelected;

        private static AudioClip[] _lastGoodAudioClips;
        private static string _lastGoodSelected;

        [HarmonyPatch(typeof(NoteCutSoundEffect))]
        [HarmonyPatch("Awake", MethodType.Normal)]
        public class BadCutSoundPatch
        {
            public static void Prefix(ref AudioClip[] ____badCutSoundEffectAudioClips)
            {
                if (_originalBadSounds == null)
                {
                    _originalBadSounds = new List<AudioClip>();
                    _originalBadSounds.AddRange(____badCutSoundEffectAudioClips);
                }

                if (Plugin.CurrentConfig.BadHitSound == "None")
                {
                    ____badCutSoundEffectAudioClips = new AudioClip[] { SoundLoader.GetEmptyClip() };
                } else if (Plugin.CurrentConfig.BadHitSound == "Default")
                {
                    ____badCutSoundEffectAudioClips = _originalBadSounds.ToArray();
                }
                else
                {
                    if (_lastBadSelected == Plugin.CurrentConfig.BadHitSound)
                    {
                        ____badCutSoundEffectAudioClips = _lastBadAudioClips;
                    }
                    else
                    {
                        _lastBadSelected = Plugin.CurrentConfig.BadHitSound;
                        _lastBadAudioClips = new AudioClip[] { SoundLoader.LoadAudioClip(_lastBadSelected) };
                        ____badCutSoundEffectAudioClips = _lastBadAudioClips;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(NoteCutSoundEffectManager))]
        [HarmonyPatch("Start", MethodType.Normal)]
        public class HitSoundsPatch
        {
            public static void Prefix(ref AudioClip[] ____longCutEffectsAudioClips, ref AudioClip[] ____shortCutEffectsAudioClips)
            {
                if (_originalGoodLongSounds == null)
                {
                    _originalGoodLongSounds = new List<AudioClip>();
                    _originalGoodLongSounds.AddRange(____longCutEffectsAudioClips);
                }

                if (_originalGoodShortSounds == null)
                {
                    _originalGoodShortSounds = new List<AudioClip>();
                    _originalGoodShortSounds.AddRange(____shortCutEffectsAudioClips);
                }

                if (Plugin.CurrentConfig.GoodHitSound == "None")
                {
                    ____longCutEffectsAudioClips = new AudioClip[] { SoundLoader.GetEmptyClip() };
                    ____shortCutEffectsAudioClips = new AudioClip[] { SoundLoader.GetEmptyClip() };
                }
                else if (Plugin.CurrentConfig.GoodHitSound == "Default")
                {
                    ____shortCutEffectsAudioClips = _originalGoodShortSounds.ToArray();
                    ____longCutEffectsAudioClips = _originalGoodLongSounds.ToArray();
                }
                else
                {
                    if (_lastGoodSelected == Plugin.CurrentConfig.GoodHitSound)
                    {
                        ____shortCutEffectsAudioClips = _lastGoodAudioClips;
                        ____longCutEffectsAudioClips = _lastGoodAudioClips;
                    }
                    else
                    {
                        _lastGoodSelected = Plugin.CurrentConfig.GoodHitSound;
                        _lastGoodAudioClips = new AudioClip[] { SoundLoader.LoadAudioClip(_lastGoodSelected) };
                        ____shortCutEffectsAudioClips = _lastGoodAudioClips;
                        ____longCutEffectsAudioClips = _lastGoodAudioClips;
                    }
                }
            }
        }
    }
}
