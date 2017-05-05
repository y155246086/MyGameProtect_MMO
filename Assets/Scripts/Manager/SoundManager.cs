using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using Mogo.Util;
namespace Mogo
{
    public class SoundManager
    {
        #region 枚举和常量

        public enum PlayMusicMode
        {
            Single,
            Repeat,
            Order,
            RepeatOrder,
            Random
        }

        public static string defaultSoundSourceName = "SoundSource";
        public static string defaultMusicSourceName = "MusicSource";
        public static string defaultEnvironmentSoundSourceName = "EnvironmentSource";

        public static uint defaultBackgroundInterval = 1000;

        #endregion

        #region 参数

        protected static AudioListener listener;

        protected static AudioSource defaultSoundSource;
        protected static AudioSource defaultMusicSource;
        protected static AudioSource defaultEnvironmentSoundSource;

        public static Dictionary<int, AudioClip> audioClipBuffer = new Dictionary<int, AudioClip>();

        private static float soundVolume;
        public static float SoundVolume
        {
            get { return soundVolume; }
            set
            {
                soundVolume = value > 0.95 ? 1 : (value < 0.05) ? 0 : value;
            }
        }

        private static float musicVolume = 0.2f;
        public static float MusicVolume
        {
            get { return musicVolume; }
            set
            {
                musicVolume = value > 0.95 ? 1 : (value < 0.05) ? 0 : value;
                if (defaultMusicSource != null)
                    defaultMusicSource.volume = value;
            }
        }

        protected static uint nextPlayMusicTimer;

        protected static PlayMusicMode musicMode;
        protected static List<int> backgroundMusicOrder = new List<int>();
        protected static int orderIndex;
        protected static int curMusic = -1;

        #endregion

        #region 初始化

        static SoundManager()
        {
            Debuger.LogError("ConstructSoundManager");
        }

        public static void Init()
        {
            Debuger.LogError("InitSoundManager");
            GameObject.DontDestroyOnLoad(GameObject.Find("Driver"));
            defaultSoundSource = GameObject.Find("Driver").transform.FindChild(defaultSoundSourceName).gameObject.GetComponent<AudioSource>();
            defaultMusicSource = GameObject.Find("Driver").transform.FindChild(defaultMusicSourceName).gameObject.GetComponent<AudioSource>();

            audioClipBuffer = new Dictionary<int, AudioClip>();

            musicMode = PlayMusicMode.Repeat;
            backgroundMusicOrder = new List<int>();
            orderIndex = 0;

            SoundVolume = 1;
            MusicVolume = 0.2f;
            AddListeners();
        }

        public static void AddListeners()
        {
            #region 音量

            EventDispatcher.AddEventListener<float>(SettingEvent.MotitySoundVolume, MotitySoundVolume);
            EventDispatcher.AddEventListener<float>(SettingEvent.MotityMusicVolume, MotityMusicVolume);
            EventDispatcher.AddEventListener(SettingEvent.SaveVolume, SaveVolume);

            #endregion


            #region 音乐

            EventDispatcher.AddEventListener<int>(SettingEvent.BuildSoundEnvironment, BuildSoundEnvironment);


            EventDispatcher.AddEventListener<int, bool>(SettingEvent.PlayBackGroundMusic, ChangeBackgroundMusic);
            EventDispatcher.AddEventListener<int, PlayMusicMode>(SettingEvent.ChangeMusic, ChangeMusic);


            #endregion
        }

        public static void RemoveListeners()
        {
            #region 音量

            EventDispatcher.RemoveEventListener<float>(SettingEvent.MotitySoundVolume, MotitySoundVolume);
            EventDispatcher.RemoveEventListener<float>(SettingEvent.MotityMusicVolume, MotityMusicVolume);
            EventDispatcher.RemoveEventListener(SettingEvent.SaveVolume, SaveVolume);

            #endregion


            #region 音乐

            EventDispatcher.RemoveEventListener<int>(SettingEvent.BuildSoundEnvironment, BuildSoundEnvironment);

            EventDispatcher.RemoveEventListener<int, PlayMusicMode>(SettingEvent.ChangeMusic, ChangeMusic);
            EventDispatcher.RemoveEventListener<int, bool>(SettingEvent.PlayBackGroundMusic, ChangeBackgroundMusic);

            #endregion
        }

        #endregion

        #region 切场景重新设置环境

        public static void BuildSoundEnvironment(int missionID)
        {

            if (listener == null)
                listener = GameObject.Find("Driver").GetComponent<AudioListener>();

            ChangeBackgroundMusic(missionID);
        }

        public static void ChangeBackgroundMusic(int missionID, bool isInstance = false)
        {
            if (listener != null)
            {
                if (defaultMusicSource == null)
                {
                    defaultMusicSource = GameObject.Find("Driver").transform.FindChild(defaultMusicSourceName).gameObject.GetComponent<AudioSource>();
                    if (defaultMusicSource == null)
                        defaultMusicSource = GameObject.Find("Driver").transform.FindChild(defaultMusicSourceName).gameObject.AddComponent<AudioSource>();
                }
                PlayBackgroundMusic(missionID, PlayMusicMode.Repeat);
            }
        }

        #endregion

        #region 修改音量

        private static void MotitySoundVolume(float theVolume)
        {
            SoundVolume = theVolume;
        }

        private static void MotityMusicVolume(float theVolume)
        {
            MusicVolume = theVolume * 0.7f;
        }

        private static void SaveVolume()
        {
            Debuger.LogError("SaveVolume: " + soundVolume + " " + musicVolume);
        }

        #endregion

        #region 播放声音

        #region 播放全局声音

        static public AudioSource MyPlaySound(AudioSource defaultSource, string sourceName, AudioClip clip)
        {
            return MyPlaySound(defaultSource, sourceName, clip, 1f, 1f);
        }

        public static AudioSource MyPlaySound(AudioSource defaultSource, string sourceName, AudioClip clip, float volume)
        {
            return MyPlaySound(defaultSource, sourceName, clip, volume, 1f);
        }

        public static AudioSource MyPlaySound(AudioSource defaultSource, string sourceName, AudioClip clip, float volume, float pitch)
        {
            if (clip != null)
            {
                if (listener == null)
                {
                    listener = GameObject.Find("Driver").GetComponent<AudioListener>();

                    if (listener == null)
                        listener = GameObject.Find("Driver").AddComponent<AudioListener>();
                }

                if (listener != null)
                {
                    AudioSource source = defaultSource;
                    if (source == null)
                    {
                        defaultSource = GameObject.Find("Driver").transform.FindChild(sourceName).gameObject.AddComponent<AudioSource>();
                        source = defaultSource;
                    }

                    source.volume = volume;
                    source.pitch = pitch;
                    source.PlayOneShot(clip);
                    return source;
                }
            }
            return null;
        }

        #endregion

        #region 播放场景物件音效
        public static AudioClip GameObjectPlaySound(string path, GameObject go, bool isLoop = false, bool is3DAudio = false)
        {
            if (path != null && path.Length > 0)
            {
                AudioClip clip = Resources.Load<AudioClip>(path);
                if (clip != null)
                {
                    GameObjectPlaySound(clip, go, isLoop, is3DAudio);
                }
                else
                {
                    Debug.LogError("声音资源有问题：" + path);
                }
                return clip;
            }
            return null;
        }
        public static void GameObjectPlaySound(AudioClip clip, GameObject go, bool isLoop = false, bool is3DAudio = false)
        {
            if (clip == null) return;
            AudioSource gameObjectAudioSource = go.GetComponent<AudioSource>();
            if (gameObjectAudioSource == null)
                gameObjectAudioSource = go.AddComponent<AudioSource>();
            else if (gameObjectAudioSource.isPlaying)
                gameObjectAudioSource.Stop();
            gameObjectAudioSource.spatialBlend = is3DAudio ? 1 : 0;
            gameObjectAudioSource.playOnAwake = false;
            PlaySoundOnSourceByObject(gameObjectAudioSource, clip, isLoop);
        }

        public static void PlaySoundOnSourceByObject(AudioSource gameObjectAudioSource, UnityEngine.Object clipObject, bool isLoop = false)
        {
            if (clipObject is AudioClip)
            {
                gameObjectAudioSource.clip = clipObject as AudioClip;
                //gameObjectAudioSource.volume = SoundVolume;
                gameObjectAudioSource.loop = isLoop;
                gameObjectAudioSource.playOnAwake = false;
                gameObjectAudioSource.Play();
                return;
            }

            var clip = (clipObject as GameObject).GetComponent<AudioSource>().clip;
            if (clip != null)
            {
                gameObjectAudioSource.clip = clip;
                //gameObjectAudioSource.volume = SoundVolume;
                gameObjectAudioSource.loop = isLoop;
                gameObjectAudioSource.playOnAwake = false;
                gameObjectAudioSource.Play();
            }
        }

        public static void StopGameObjectPlaySound(GameObject go)
        {
            AudioSource source = go.GetComponent<AudioSource>();
            if (source == null)
                return;
            if (source.isPlaying)
                source.Stop();
        }

        #endregion

        #region UI音效

        public static void PlaySoundByObject(UnityEngine.Object clipObject)
        {
            if (clipObject == null)
            {
                Debuger.LogError("animation clip Object is null!");
                return;
            }

            if (clipObject is AudioClip)
            {
                defaultSoundSource = MyPlaySound(defaultSoundSource, defaultSoundSourceName, clipObject as AudioClip, soundVolume);
                return;
            }

            var clip = (clipObject as GameObject).GetComponent<AudioSource>().clip;
            if (clip != null)
                defaultSoundSource = MyPlaySound(defaultSoundSource, defaultSoundSourceName, clip, soundVolume);
        }

        #endregion

        #region 背景音乐

        public static void PlayBackgroundMusic(int missionID, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            Debuger.LogError("PlayBackgroundMusic Check");
        }

        public static void PlayBackgroundMusic(int missionID, int index, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            Debuger.LogError("PlayBackgroundMusic");
        }
        public static void PlayMusic(AudioClip clip, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            musicMode = mode;
            PlayMusicByObject(clip);
        }
        public static void PlayMusic(int soundID, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            if (curMusic == soundID && musicMode == mode && defaultMusicSource.isPlaying)
                return;

            curMusic = soundID;
            musicMode = mode;
        }

        public static void PlayMusicByObject(UnityEngine.Object clipObject, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            Debuger.LogError("PlayMusicByObject: " + clipObject + " " + MusicVolume);
            if (defaultMusicSource == null)
            {
                defaultMusicSource = GameObject.Find("Driver").transform.FindChild(defaultMusicSourceName).gameObject.GetComponent<AudioSource>();
                if (defaultMusicSource == null)
                    defaultMusicSource = GameObject.Find("Driver").transform.FindChild(defaultMusicSourceName).gameObject.AddComponent<AudioSource>();
            }
            if (clipObject is AudioClip)
            {
                defaultMusicSource = MyPlaySound(defaultMusicSource, defaultMusicSourceName, clipObject as AudioClip, musicVolume);
                PrepareForNextPlay((uint)(
                    (int)((clipObject as AudioClip).length * 1000) + defaultBackgroundInterval
                    ), clipObject);
                return;
            }

            var clip = (clipObject as GameObject).GetComponent<AudioSource>().clip;
            if (clip != null)
            {
                defaultMusicSource = MyPlaySound(defaultMusicSource, defaultMusicSourceName, clip, musicVolume);
                PrepareForNextPlay((uint)(
                    (int)(clip.length * 1000) + defaultBackgroundInterval
                    ), clipObject);
            }
        }

        protected static void PrepareForNextPlay(uint time, UnityEngine.Object clipObject)
        {
            nextPlayMusicTimer = TimerHeap.AddTimer<UnityEngine.Object>(time, 0, SetNextPlay, clipObject);
        }

        protected static void SetNextPlay(UnityEngine.Object clipObject)
        {
            Debuger.LogError("SetNextPlay");

            switch (musicMode)
            {
                case PlayMusicMode.Single:
                    return;

                case PlayMusicMode.Repeat:
                    PlayMusicByObject(clipObject);
                    return;

                case PlayMusicMode.Order:
                    if (orderIndex + 1 >= backgroundMusicOrder.Count)
                    {
                        orderIndex = 0;
                        return;
                    }
                    else
                    {
                        PlayMusic(backgroundMusicOrder[orderIndex + 1]);
                    }
                    return;

                case PlayMusicMode.RepeatOrder:
                    PlayMusic(backgroundMusicOrder[orderIndex + 1 >= backgroundMusicOrder.Count ? 0 : orderIndex + 1]);
                    return;

                case PlayMusicMode.Random:
                    orderIndex = UnityEngine.Random.Range(0, backgroundMusicOrder.Count);
                    PlayMusic(backgroundMusicOrder[orderIndex]);
                    return;
            }
        }

        public static void StopBackgroundMusic()
        {
            Debuger.LogError("StopBackgroundMusic");

            if (defaultMusicSource != null)
            {
                Debuger.LogError("defaultSource" + defaultMusicSource.gameObject.name);
                defaultMusicSource.Stop();
            }

            TimerHeap.DelTimer(nextPlayMusicTimer);
        }

        public static void ChangeMusic(int soundID, PlayMusicMode mode = PlayMusicMode.Repeat)
        {
            StopBackgroundMusic();
            PlayMusic(soundID, mode);
        }

        #endregion

        #endregion
    }
}


