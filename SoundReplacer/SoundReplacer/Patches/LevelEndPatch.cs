using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace SoundReplacer.Patches
{
    public class LevelEndPatch
    {
        private static AudioClip _lastSuccessClip;
        private static string _lastSuccessSelected;

        private static AudioClip _lastFailClip;
        private static string _lastFailSelected;

        [HarmonyPatch(typeof(ResultsViewController))]
        [HarmonyPatch("DidActivate", MethodType.Normal)]
        public class DidActivatePatch
        {
            public static void Prefix(bool addedToHierarchy, ref SongPreviewPlayer ____songPreviewPlayer, ref LevelCompletionResults ____levelCompletionResults)
            {
                if (!addedToHierarchy)
                    return;
                
                if (____levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
                {
                    if (!(Plugin.CurrentConfig.SuccessSound == "Default" ||
                          Plugin.CurrentConfig.SuccessSound == "None"))
                    {
                        AudioClip desiredSuccessClip;

                        if (_lastSuccessSelected == Plugin.CurrentConfig.SuccessSound)
                        {
                            desiredSuccessClip = _lastSuccessClip;
                        }
                        else
                        {
                            _lastSuccessSelected = Plugin.CurrentConfig.SuccessSound;
                            _lastSuccessClip = SoundLoader.LoadAudioClip(_lastSuccessSelected);
                            desiredSuccessClip = _lastSuccessClip;
                        }

                        ____songPreviewPlayer.CrossfadeTo(desiredSuccessClip, 0f, 0f, Math.Min(desiredSuccessClip.length, 20.0f));
                    }
                }

                if (____levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
                {
                    if (!(Plugin.CurrentConfig.FailSound == "Default" ||
                          Plugin.CurrentConfig.FailSound == "None"))
                    {
                        AudioClip desiredFailClip;

                        if (_lastFailSelected == Plugin.CurrentConfig.FailSound)
                        {
                            desiredFailClip = _lastFailClip;
                        }
                        else
                        {
                            _lastFailSelected = Plugin.CurrentConfig.FailSound;
                            _lastFailClip = SoundLoader.LoadAudioClip(_lastFailSelected);
                            desiredFailClip = _lastFailClip;
                        }

                        ____songPreviewPlayer.CrossfadeTo(desiredFailClip, 0f, 0f, Math.Min(desiredFailClip.length, 20.0f));
                    }
                }
            }
        }
    }
}

