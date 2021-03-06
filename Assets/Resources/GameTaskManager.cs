﻿using UnityEngine;
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
    public Text remainingSecondsText;
    public Text currentLevelText;

    private string currentTask;
    private List<string> remainingTasks;
    private float timeStart;
    private int level = 0;

    void Start()
    {
        GameUtil.SafeFind("TypeTargetManager").SafeGetComponent<TypeTargetManager>().TargetMatch += GameTaskManager_TargetMatch;
        SetupGame();
    }

    void SetupGame()
    {
        int numTasks = level * 2 + 5;
        timeStart = Time.realtimeSinceStartup;
        remainingTasks = GenerateTaskList(numTasks);
        MoveToNextTask();
        currentLevelText.text = "" + level;
    }

    void MoveToNextTask()
    {
        if (remainingTasks.Count > 0)
        {
            currentTask = remainingTasks[0];
            remainingTasks.RemoveAt(0);
            currentTaskIcon.sprite = GameUtil.SafeLoad<Sprite>("TypeTarget/Icons/" + currentTask);
            remainingTasksText.text = "" + remainingTasks.Count;
        } else {
            level++;
            SetupGame();
        }
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

    void Update()
    {
        // Update seconds remaining
        int secondsRemaining = 30 - (int)(Time.realtimeSinceStartup - timeStart);
        if (secondsRemaining <= 0)
        {
            Application.LoadLevel("end");
        } else
        {
            remainingSecondsText.text = "" + secondsRemaining;
        }
    }
}
