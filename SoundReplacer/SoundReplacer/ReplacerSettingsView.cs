using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SoundReplacer
{
    internal class ReplacerSettingsView : BSMLResourceViewController
    {
        public override string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

        [UIValue("enabled")]
        protected bool SettingsEnabled
        {
            get => Plugin.CurrentConfig.Enabled;
            set => Plugin.CurrentConfig.Enabled = value;
        }

        [UIValue("good-hitsound-list")]
        public List<object> SettingsGoodHitSoundList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("good-hitsound")]
        protected string SettingCurrentGoodHitSound
        {
            get => Plugin.CurrentConfig.GoodHitSound;
            set => Plugin.CurrentConfig.GoodHitSound = value;
        }

        [UIValue("bad-hitsound-list")]
        public List<object> SettingsBadHitSoundList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("bad-hitsound")]
        protected string SettingCurrentBadHitSound
        {
            get => Plugin.CurrentConfig.BadHitSound;
            set => Plugin.CurrentConfig.BadHitSound = value;
        }

        [UIValue("menu-music-list")]
        public List<object> SettingsMenuMusicList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("menu-music")]
        protected string SettingCurrentMenuMusic
        {
            get => Plugin.CurrentConfig.MenuMusic;
            set
            {
                Plugin.CurrentConfig.MenuMusic = value;
                Helper.RefreshMenuMusic();
            }
        }

        [UIValue("click-sound-list")]
        public List<object> SettingsClickSoundList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("click-sound")]
        protected string SettingCurrentClickSound
        {
            get => Plugin.CurrentConfig.ClickSound;
            set
            {
                Plugin.CurrentConfig.ClickSound = value;
                Helper.RefreshClickSounds();
            }
        }

        [UIValue("success-sound-list")]
        public List<object> SettingsSuccessSoundList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("success-sound")]
        protected string SettingCurrentSuccessSound
        {
            get => Plugin.CurrentConfig.SuccessSound;
            set => Plugin.CurrentConfig.SuccessSound = value;
        }

        [UIValue("fail-sound-list")]
        public List<object> SettingsSuccessFailList = new List<object>(SoundLoader.GlobalSoundList);

        [UIValue("fail-sound")]
        protected string SettingCurrentFailSound
        {
            get => Plugin.CurrentConfig.FailSound;
            set => Plugin.CurrentConfig.FailSound = value;
        }
    }
}
