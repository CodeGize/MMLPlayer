using System;
using System.Collections.Generic;
using System.IO;
using MidiPlayer;
using StardustLib.UI;
using TextPlayer;
using UnityEngine;
using UnityEngine.UI;
namespace MMLPlayer
{
    public abstract class MusicPlayerBase
    {
        public abstract void Init();
        public abstract void Load(string filepath);
        public abstract void Unload();
        public abstract void Play(bool restart);
        public abstract void Pause();
        public abstract void Update();
        public abstract int Length { get; }
        public abstract int CurPos { get; }

        public Action OnFinish { get; set; }
    }

    public class SilencePlayer : MusicPlayerBase
    {
        public override int Length => 0;

        public override int CurPos => 0;

        public override void Init()
        {

        }

        public override void Load(string filepath)
        {

        }

        public override void Pause()
        {

        }

        public override void Play(bool restart)
        {

        }

        public override void Unload()
        {

        }

        public override void Update()
        {

        }
    }

    public class MidiMusicPlayer : MusicPlayerBase
    {
        public PlayerMML player;

        public override void Init()
        {
            var mml = new PlayerMML();
            mml.Settings.MaxDuration = TimeSpan.MaxValue;
            mml.Settings.MaxSize = int.MaxValue;
            mml.Mode = TextPlayer.MML.MMLMode.Mabinogi;
            mml.SetInstrument(0);
            mml.Normalize = true;
            mml.Loop = false;
            mml.CalculateNormalization();
            player = mml;
        }

        private StreamReader m_stream;

        public override int Length
        {
            get
            {
                if (player == null)
                    return 0;
                return (int)player.Duration.TotalSeconds;
            }
        }

        public override int CurPos
        {
            get
            {
                if (player == null)
                    return 0;
                return (int)player.Elapsed.TotalSeconds;
            }
        }
        private string m_curpath;
        public override void Load(string filePath)
        {
            if (m_curpath == filePath)
                return;
            m_curpath = filePath;
            Unload();
            m_stream = new StreamReader(File.OpenRead(filePath));
            player.Load(m_stream, true);
        }

        public override void Pause()
        {
            player.Pause();
        }

        public override void Play(bool restart)
        {
            if (restart)
                player.Stop();
            TimeSpan now = new TimeSpan(MusicPlayer.Time.Ticks);
            player.Play(now);
        }

        public override void Update()
        {
            if (player.Playing)
            {
                TimeSpan now = new TimeSpan(MusicPlayer.Time.Ticks);
                player.Update(now);
                if (!player.Playing)
                    OnFinish?.Invoke();
            }
        }

        public override void Unload()
        {
            if (m_stream != null)
            {
                m_stream.Close();
                m_stream.Dispose();
            }
        }
    }

    public class MMLPlayerMain : MonoBehaviour
    {
        public Slider ProgressSlider;
        public Text ProgressLb;
        public MultiImage PlayAndPauseBtn;
        public MultiImage LoopBtn;

        public GameObject MusicItemCtrl;

        void Awake()
        {
            ScanMusics();
            InitPlayer();
            ProgressSlider.minValue = 0;
            LoopBtn.SetState(m_loopType);
        }

        protected void Update()
        {
            if (player == null)
                return;
            player.Update();

            ProgressSlider.value = player.CurPos;
            ProgressLb.text = ConvertToTime(player.CurPos);
        }

        private string ConvertToTime(int curPos)
        {
            var min = curPos / 60;
            var sed = curPos % 60;
            return string.Format("{0:00}:{1:00}", min, sed);
        }

        private void InitPlayer()
        {

#if false//UNITY_EDITOR
            player = new SilencePlayer();
#else
            player = new MidiMusicPlayer();
#endif
            player.Init();
            player.OnFinish = OnPlayFinish;
        }

        private void OnPlayFinish()
        {
            var count = m_musics.Count;
            switch (LoopType)
            {
                case EmLoopType.LoopOrder:
                    var index = (CurMusicIndex + 1 + count) % count;
                    PlayMusic(index);
                    break;
                case EmLoopType.LoopOne:
                    InternalPlayMusic(true);
                    break;
                case EmLoopType.RandomOrder:
                    var rd = UnityEngine.Random.Range(0, count - 1);
                    PlayMusic(rd);
                    break;
            }
        }

        private class MusicItem
        {
            public string Name;
            public string Singer;
            public string Path;
        }

        private void ScanMusics()
        {
            m_musics = new List<MusicItem>();
#if UNITY_EDITOR || UNITY_STANDALONE
            var dir = Application.dataPath + "/../Musics/";
#else
            var dir = Application.persistentDataPath + "/Musics/";
#endif
            if (!Directory.Exists(dir))
                return;
            var files = Directory.GetFiles(dir, "*.mml", SearchOption.TopDirectoryOnly);
            foreach (var filepath in files)
            {
                var filename = Path.GetFileNameWithoutExtension(filepath);
                var strs = filename.Split('-');
                var item = new MusicItem()
                {
                    Name = strs[0],
                    Path = filepath
                };
                if (strs.Length >= 2)
                    item.Singer = strs[1];
                m_musics.Add(item);
            }

            var listroot = MusicItemCtrl.transform.parent;
            for (int i = 0; i < m_musics.Count; i++)
            {
                MusicItem item = m_musics[i];
                var ctrlgo = Instantiate(MusicItemCtrl, listroot);
                ctrlgo.SetActive(true);
                var ctrl = ctrlgo.GetComponent<MusicItemCtrl>();

                var txt = item.Name;
                if (!string.IsNullOrEmpty(item.Singer))
                    txt += "-" + item.Singer;
                ctrl.Init(i,txt);
                ctrl.Select(false);
            }
        }

        private MusicPlayerBase player;


        private void LoadAndPlayMusic()
        {
            if (player == null)
                return;
            if (CurMusicIndex < 0 || CurMusicIndex >= m_musics.Count)
                CurMusicIndex = 1;

            var item = m_musics[CurMusicIndex];
            player.Load(item.Path);

            ProgressSlider.maxValue = player.Length;

            InternalPlayMusic();
        }


        private void InternalPlayMusic(bool restart = false)
        {
            if (player == null)
                return;

            IsPlaying = true;
            player.Play(restart);
        }

        private void PauseMusic()
        {
            IsPlaying = false;
            player.Pause();
        }

        private bool m_isPlaying;
        public bool IsPlaying
        {
            get { return m_isPlaying; }
            private set
            {
                if (m_isPlaying == value)
                    return;
                m_isPlaying = value;
                PlayAndPauseBtn.SetState(m_isPlaying ? 1 : 0);
            }
        }

        public void OnPlayAndPauseBtnClick()
        {
            IsPlaying = !IsPlaying;
            if (IsPlaying)
            {
                LoadAndPlayMusic();
            }
            else
            {
                PauseMusic();
            }
        }

        private List<MusicItem> m_musics;

        public int CurMusicIndex { get; private set; } = -1;

        public void OnChangeMusicBtnClick(int delta)
        {
            var count = m_musics.Count;
            var index = (CurMusicIndex + delta + count) % count;
            PlayMusic(index);
        }

        public void OnMusicItemClick(Transform ctrl)
        {
            var index = ctrl.GetSiblingIndex() - 1;
            PlayMusic(index);
        }

        public void OnVolumeBtnClick()
        {

        }

        private void PlayMusic(int index)
        {
            if (index < 0 || index >= m_musics.Count)
                return;
            if (CurMusicIndex == index)
                return;

            var ctrl = GetCtrlByIndex(CurMusicIndex);
            ctrl.Select(false);
            
            CurMusicIndex = index;
            LoadAndPlayMusic();

            ctrl = GetCtrlByIndex(index);
            ctrl.Select(true);
        }

        private MusicItemCtrl GetCtrlByIndex(int index)
        {
            var ctrltrans = MusicItemCtrl.transform.parent.GetChild(index + 1);
            var ctrl = ctrltrans.GetComponent<MusicItemCtrl>();
            return ctrl;
        }

        public enum EmLoopType
        {
            /// <summary>
            /// 列表循环
            /// </summary>
            LoopOrder = 0,

            /// <summary>
            /// 单曲循环
            /// </summary>
            LoopOne = 1,

            /// <summary>
            /// 随机循环
            /// </summary>
            RandomOrder = 2,
        }

        private int m_loopType = 0;
        public EmLoopType LoopType
        {
            get { return (EmLoopType)m_loopType; }
        }

        public void OnLoopBtnClick()
        {
            m_loopType = (m_loopType + 1) % 3;
            LoopBtn.SetState(m_loopType);
        }
    }
}
