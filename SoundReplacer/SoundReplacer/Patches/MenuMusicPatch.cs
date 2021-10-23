using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace SoundReplacer.Patches
{
    public class MenuMusicPatch
    {
        private static AudioClip _originalMenuMusicClip;
        
        private static AudioClip _lastMenuMusicClip;
        private static string _lastMusicSelected;

        [HarmonyPatch(typeof(SongPreviewPlayer))]
        [HarmonyPatch("Start", MethodType.Normal)]
        public class SongPreviewPlayerPatch
        {
            public static void Prefix(ref AudioClip ____defaultAudioClip)
            {
                if (_originalMenuMusicClip == null)
                {
                    _originalMenuMusicClip = ____defaultAudioClip;
                }

                if (Plugin.CurrentConfig.MenuMusic == "None")
                {
                    ____defaultAudioClip = SoundLoader.GetEmptyClip();
                }
                else if (Plugin.CurrentConfig.MenuMusic == "Default")
                {
                    ____defaultAudioClip = _originalMenuMusicClip;
                }
                else
                {
                    if (_lastMusicSelected == Plugin.CurrentConfig.MenuMusic)
                    {
                        ____defaultAudioClip = _lastMenuMusicClip;
                    }
                    else
                    {
                        _lastMusicSelected = Plugin.CurrentConfig.MenuMusic;
                        _lastMenuMusicClip = SoundLoader.LoadAudioClip(_lastMusicSelected);
                        ____defaultAudioClip = _lastMenuMusicClip;
                    }
                }
            }
        }
    }
}
