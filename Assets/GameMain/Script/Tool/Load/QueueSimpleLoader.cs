using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSimpleLoader : MyEventDispatcher {

    public object bringData;
    public List<QueueItem> loaderQueue = new List<QueueItem>();
    private int count = 0;
    private float temptotal;
    public int getCount
    {
        get
        {
            return count;
        }
    }

    public float getProgress
    {
        get
        {
            if (count == 0) return 1;
            temptotal = 0;
            for (int i = 0; i < loaderQueue.Count; i++)
            {
                temptotal += loaderQueue[i].progress;
            }
            return temptotal / count;
        }
    }

    public float itemProgress
    {
        get
        {
            if (count == 0) return 1;
            temptotal = 0;
            for (int i = 0; i < loaderQueue.Count; i++)
            {
                if (loaderQueue[i].progress == 1)
                {
                    temptotal += 1;
                }
            }
            return temptotal / count;
        }
    }

    public QueueSimpleLoader(object data = null)
    {
        this.bringData = data;
        SimpleLoader.StaticEventDispatcher.addEventListener(LoadResourceEvent.Cancel,CamcelQueueLoad);
    }

    public void AddQueueItem(SimpleLoader loader)
    {
        if (loader == null) return;
        QueueItem item = new QueueItem(loader);
        item.addEventListener(LoadResourceEvent.Progress,ItemProgress);
        item.addEventListener(LoadResourceEvent.Complete,ItemComplete);
        loaderQueue.Add(item);
        count = loaderQueue.Count;
    }

    public void ItemProgress(MyEvent data)
    {
        this.dispatchEvent(new LoadResourceEvent(LoadResourceEvent.QueueProgress,getProgress));
        if (getProgress ==1)
        {
            this.dispatchEvent( new LoadResourceEvent(LoadResourceEvent.QueueComplete,this));
        }
    }

    public void ItemComplete(MyEvent data)
    {
        QueueItem item = data.data as QueueItem;
        item.removeEventListener(LoadResourceEvent.Complete,ItemComplete);
        //this.dispatchEvent(new  LoadResourceEvent(LoadResourceEvent.ItemProgress,new object[] { item.loader,itemProgress}));
    }

    private void CamcelQueueLoad(MyEvent obj)
    {
        // TODO  UI
        ClearAllListener();
        loaderQueue.Clear();
    }

    private void ClearAllListener()
    {
        this.ClearListioner();
        for (int i = 0; i < loaderQueue.Count; i++)
        {
            loaderQueue[i].ClearListioner();
        }
    }

    public void Stop()
    {
        ClearAllListener();
        for (int i = 0; i < loaderQueue.Count; i++)
        {
            loaderQueue[i].Stop();
        }
    }

    public class QueueItem : MyEventDispatcher
    {
        public float progress;
        public SimpleLoader loader;
        public SimpleLoadState state
        {
            get
            {
                if (loader == null)
                {
                    return SimpleLoadState.None;
                }
                return loader.state;
            }
        }

        public QueueItem(SimpleLoader loader)
        {
            this.loader = loader;
            //1 simpleloader 添加监听
            loader.addEventListener(LoadResourceEvent.Complete,LoadComplete);
            loader.addEventListener(LoadResourceEvent.Progress,LoadProgress);
        }

        private void LoadProgress(MyEvent obj)
        {
            progress =(float) obj.data;
            this.dispatchEvent(new LoadResourceEvent(LoadResourceEvent.Progress,loader.progress));
        }

        private void LoadComplete(MyEvent obj)
        {   
            //加载完成 执行移除
            loader.removeEventListener(LoadResourceEvent.Complete, LoadComplete);
            loader.removeEventListener(LoadResourceEvent.Progress, LoadProgress);
            //单个QueueItem 抛事件
            this.dispatchEvent(new LoadResourceEvent(LoadResourceEvent.Complete,this));
            progress = 1;
            this.dispatchEvent(new LoadResourceEvent(LoadResourceEvent.Progress,progress));
        }

        public void Stop()
        {
            loader.canceled = true;
            loader.removeEventListener(LoadResourceEvent.Complete,LoadComplete);
            loader.removeEventListener(LoadResourceEvent.Progress,LoadProgress);
            //SimpleInnerLoader.RemoveLoader(loader);
        }
    }
}
