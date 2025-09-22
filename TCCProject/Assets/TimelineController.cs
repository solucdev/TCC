using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playerController;

    void Start()
    {
        director.stopped += OnTimelineFinished;
        playerController.SetActive(false);
        director.Play();
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        playerController.SetActive(true);
    }
}