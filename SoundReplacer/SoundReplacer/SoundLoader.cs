using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SoundReplacer
{
    internal static class SoundLoader
    {
        public static List<string> GlobalSoundList = new List<string>();
        
        private static AudioClip _cachedEmpty;

        public static void GetSoundLists()
        {
            GlobalSoundList.Add("None");
            GlobalSoundList.Add("Default");

            var folderPath = Environment.CurrentDirectory + "\\UserData\\SoundReplacer";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".ogg")
                    continue;

                GlobalSoundList.Add(fileInfo.Name);
            }
        }

        public static string GetFullPath(string name)
        {
            var path = Environment.CurrentDirectory + "\\UserData\\SoundReplacer\\" + name;
            var fileInfo = new FileInfo(path);
            return fileInfo.FullName;
        }

        public static AudioClip LoadAudioClip(string name)
        {
            var fullPath = GetFullPath(name);
            var fileUrl = "file:///" + fullPath;
            var request = UnityWebRequestMultimedia.GetAudioClip(fileUrl, AudioType.OGGVORBIS);
            
            AudioClip loadedAudio = null;
            var task = request.SendWebRequest();
            
            // while I would normally kill people for this
            // we are loading a local file, so it should be
            // basically instant success or error
            while (!task.isDone) { }

            if (request.isNetworkError)
                Plugin.Log.Error("Failed to load audio: " + request.error);
            else
                loadedAudio = DownloadHandlerAudioClip.GetContent(request);

            return loadedAudio;
        }

        public static AudioClip GetEmptyClip()
        {
            if (_cachedEmpty != null)
                return _cachedEmpty;

            _cachedEmpty = AudioClip.Create("Empty", 10, 1, 44100 * 2, false);
            return _cachedEmpty;
        }
    }
}
