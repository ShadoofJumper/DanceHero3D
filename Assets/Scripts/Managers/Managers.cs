using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(SoundManager))]


public class Managers : MonoBehaviour
{

    public static ScoreManager Score { get; private set; }
    public static SoundManager Sound { get; private set; }
    private List<IGameManager> _startSequence; // all managers


    void Awake()
    {
        Score = GetComponent<ScoreManager>();
        Sound = GetComponent<SoundManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Score);
        _startSequence.Add(Sound);

        StartCoroutine(StartupManager());
    }

    private IEnumerator StartupManager()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.StartUp();
        }
        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) // working untill all of manager will be working
        {
            //save start count of reeady manager each iteration
            int lastReady = numReady;
            numReady = 0;
            //then count ready
            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            //if ready bigger then last info then print info in console
            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null; // stop on one framer befor next iteration
        }
        Debug.Log("All manager started up");
    }

}
