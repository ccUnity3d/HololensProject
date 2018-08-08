using System.Collections;
using System.Collections.Generic;

public class BumCoroutineManager
{
    private readonly List<CoroutineTask> activeTaskList = new List<CoroutineTask>();

    public BumCoroutineManager()
    {

    }

    public void init()
    {

    }

    public void tick()
    {
        if (activeTaskList.Count > 0)
        {
            bool anyTaskFinish = false;
            for (int i = 0; i < activeTaskList.Count; i++)
            {
                CoroutineTask tempTask = activeTaskList[i];
                if (!tempTask.routine.MoveNext())
                {
                    anyTaskFinish = true;
                    tempTask.isFinish = true;
                    activeTaskList[i] = tempTask;
                }
            }
            if (anyTaskFinish)
            {
                activeTaskList.RemoveAll(x => x.isFinish == true);
            }
        }
    }

    public CoroutineTask startCoroutine(IEnumerator routine)
    {
        if (routine.MoveNext())
        {
            CoroutineTask tempTask = new CoroutineTask(routine);
            activeTaskList.Add(tempTask);
            return tempTask;
        }
        return new CoroutineTask();
    }

    public void stopCoroutine(CoroutineTask routineTask)
    {
        if (activeTaskList.Contains(routineTask))
            activeTaskList.Remove(routineTask);
    }

    public void clear()
    {

    }
}

public struct CoroutineTask
{
    public IEnumerator routine;
    public bool isFinish;

    public CoroutineTask(IEnumerator routine)
    {
        this.routine = routine;
        isFinish = false;
    }
}
