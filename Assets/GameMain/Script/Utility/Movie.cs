using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// 影片
    /// </summary>
    public abstract class Movie : MonoBehaviour {
        /// <summary>
        /// 当影片更新时调用
        /// </summary>
        public abstract event UnityAction<Texture> onUpdate;
        /// <summary>
        /// 当影片播放完毕事件
        /// </summary>
        public abstract event UnityAction onStop;
        /// <summary>
        /// 自动播放
        /// </summary>
        public abstract bool playOnAwake {
            get;
            set;
        }
        /// <summary>
        /// 播放中？
        /// </summary>
        public abstract bool isPlaying {
            get;
        }
        /// <summary>
        /// 循环
        /// </summary>
        public abstract bool loop {
            get;
            set;
        }
        /// <summary>
        /// 播放
        /// </summary>
        public abstract void Play();
        /// <summary>
        /// 暂停
        /// </summary>
        public abstract void Pause();
        /// <summary>
        /// 停止
        /// </summary>
        public abstract void Stop();
    }
