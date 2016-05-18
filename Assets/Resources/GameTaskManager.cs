using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameTaskManager : MonoBehaviour {

    static string[] tasks = new string[]
    {
        "Beans",
        "Salsa",
        "Rice",
        "Lettuce",
        "Chicken",
        "Money"
    };

    public delegate void DoWorkerAction(string name);
    public event DoWorkerAction TaskComplete;

    public Image currentTaskIcon;
    public Text remainingTasksText;

    private string currentTask;
    private List<string> remainingTasks;

    void Start()
    {
        GameUtil.SafeFind("TypeTargetManager").SafeGetComponent<TypeTargetManager>().TargetMatch += GameTaskManager_TargetMatch;
        remainingTasks = GenerateTaskList(10);
        MoveToNextTask();
    }

    void MoveToNextTask()
    {
        if (remainingTasks.Count > 0)
        {
            currentTask = remainingTasks[0];
            remainingTasks.RemoveAt(0);
        }
        currentTaskIcon.sprite = GameUtil.SafeLoad<Sprite>("TypeTarget/Icons/" + currentTask);
        remainingTasksText.text = "" + remainingTasks.Count;
    }

    List<string> GenerateTaskList(int length)
    {
        List<string> output = new List<string>();
        for (int i = 0; i < length; i++)
        {
            output.Add(tasks[Random.Range(0, tasks.Length)]);
        }
        return output;
    }

    private void GameTaskManager_TargetMatch(string command)
    {
        if (command == currentTask)
        {
            MoveToNextTask();
        }
    }
}
