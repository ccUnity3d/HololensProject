using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// 序列帧影片
    /// </summary>
    public sealed class MovieOfFrames : Movie {
        /// <summary>
        /// 类型
        /// </summary>
        public enum Style {
            /// <summary>
            /// 循环
            /// </summary>
            Loop = 0,
            /// <summary>
            /// 乒乓
            /// </summary>
            PingPong = 1,
            /// <summary>
            /// 单次
            /// </summary>
            Once = 2
        }
        [Serializable]
        class OnUpdateEvent : UnityEvent<Texture> { }
        Texture2D m_DefaultTexture = null;
        [SerializeField]
        bool m_PlayOnAwake = false;
        [SerializeField]
        int m_CurrentFrame = 0;
        [SerializeField]
        int m_FrameRate = 25;
        [SerializeField]
        Style m_Style = Style.Once;
        [SerializeField]
        List<Texture2D> m_Frames = new List<Texture2D>();
        Coroutine m_PlayCor = null;
        bool m_IsPlaying = false;
        [SerializeField]
        OnUpdateEvent m_OnUpdate = null;
        [SerializeField]
        UnityEvent m_OnStop = null;
        /// <summary>
        /// 当序列帧更新时调用
        /// </summary>
        public override event UnityAction<Texture> onUpdate {
            add {
                m_OnUpdate.AddListener(value);
            }
            remove {
                m_OnUpdate.RemoveListener(value);
            }
        }
        /// <summary>
        /// 当序列帧播放完毕事件
        /// </summary>
        public override event UnityAction onStop {
            add {
                m_OnStop.AddListener(value);
            }
            remove {
                m_OnStop.RemoveListener(value);
            }
        }
        /// <summary>
        /// 播放类型
        /// </summary>
        public Style style {
            get {
                return m_Style;
            }
            set {
                m_Style = value;
            }
        }
        /// <summary>
        /// 播放中？
        /// </summary>
        public override bool isPlaying {
            get {
                return m_IsPlaying;
            }
        }
        /// <summary>
        /// 循环
        /// </summary>
        public override bool loop {
            get {
                return style == Style.Loop;
            }
            set {
                if (value)
                    style = Style.Loop;
                else
                    style = Style.Once;
            }
        }
        /// <summary>
        /// 当前显示画面
        /// </summary>
        public Texture2D texture {
            get;
            private set;
        }
        /// <summary>
        /// 总帧数
        /// </summary>
        public int maxFrames {
            get {
                return m_Frames.Count - 1;
            }
        }
        /// <summary>
        /// 当前帧数
        /// </summary>
        public int currentFrame {
            get {
                return m_CurrentFrame;
            }
            set {
                value = Mathf.Clamp(value, 0, maxFrames);
                m_CurrentFrame = value;
                Sample();
            }
        }
        /// <summary>
        /// 帧速率
        /// </summary>
        public int frameRate {
            get {
                return m_FrameRate;
            }
        set
        {
            m_FrameRate = value;
        }
        }

        /// <summary>
        /// 起始是否播放
        /// </summary>
        public override bool playOnAwake {
            get {
                return m_PlayOnAwake;
            }
            set {
                m_PlayOnAwake = value;
            }
        }
        /// <summary>
        /// 序列集合
        /// </summary>
        public Texture2D[] frames {
            get {
                return m_Frames.ToArray();
            }
            set {
            m_Frames = value.ToList();
                //m_Frames = value.Where(f => Regex.IsMatch(f.name, @"^\d+$")).OrderBy(f => int.Parse(f.name)).ToList();
                Sample();
                Stop();
            }
        }

        public float duration;


        /// <summary>
        /// 播放
        /// </summary>
        public override void Play() {
            if (isPlaying)
                return;
            m_IsPlaying = true;
            m_CurrentFrame = 0;
            Sample();
            m_PlayCor = StartCoroutine(PlayMovie());
            StartCoroutine(Internal_OnFinished());
            //StartCoroutine(AutoHide(duration));
            
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public override void Pause()
        {
            StopCoroutine(m_PlayCor);
            m_IsPlaying = false;
        }
        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop() {
           if(m_PlayCor!=null) StopCoroutine(m_PlayCor);
            m_CurrentFrame = 0;
            m_IsPlaying = false;
            m_OnStop.Invoke();
        }
        /// <summary>
        /// 采样（更新图片）
        /// </summary>
        public void Sample() {
            try {
                texture = m_Frames[m_CurrentFrame];
                m_OnUpdate.Invoke(texture);
            }
            catch {
                if (m_DefaultTexture == null) {
                    m_DefaultTexture = new Texture2D(8, 8);
                }
                m_OnUpdate.Invoke(m_DefaultTexture);
            }
        }
        IEnumerator Internal_OnFinished() {
            while (isPlaying) {
                yield return new WaitForEndOfFrame();
            }
            Stop();
        }
        void Awake() {
            //m_Frames = m_Frames.Where(f => Regex.IsMatch(f.name, @"^\d+$")).OrderBy(f => int.Parse(f.name)).ToList();
            //m_Frames = m_Frames.Where(f => true).OrderBy(f => int.Parse(f.name)).ToList();
            //m_Frames = m_Frames.ToList();

        }


        IEnumerator AutoHide(float delay) {
            yield return new WaitForSeconds(delay);
            Stop();
            gameObject.SetActive(false);
        }





        void OnEnable() {
            if (m_PlayOnAwake)
                Play();
        }
        void OnDisable() {
            Stop();
        }
        IEnumerator PlayMovie() {
            bool flag = true;
            while (isPlaying) {
                float waitTime = 1f / m_FrameRate;
                switch (m_Style) {
                    case Style.Loop:
                        if (m_CurrentFrame == maxFrames) {
                            m_CurrentFrame = 0;
                        }
                        m_CurrentFrame++;
                        break;
                    case Style.PingPong:
                        if (m_CurrentFrame == maxFrames) {
                            flag = false;
                        }
                        if (m_CurrentFrame == 0) {
                            flag = true;
                        }
                        if (flag) {
                            m_CurrentFrame++;
                        }
                        else {
                            m_CurrentFrame--;
                        }
                        break;
                    case Style.Once:
                        if (m_CurrentFrame == maxFrames) {
                            m_IsPlaying = false;
                            StopCoroutine(m_PlayCor);
                        }
                        m_CurrentFrame++;
                        break;
                }
                m_CurrentFrame = Mathf.Clamp(m_CurrentFrame, 0, maxFrames);
                Sample();
                yield return new WaitForSecondsRealtime(waitTime);
            }
        }
    }

