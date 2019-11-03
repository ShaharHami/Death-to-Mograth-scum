using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] GameObject endUI;
    PlayableDirector director;
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (endUI != null)
        {
            endUI.SetActive(true);
        }
    }

    private void StopAllDirectors()
    {
        Object[] directors = Resources.FindObjectsOfTypeAll(typeof(PlayableDirector));
        foreach (PlayableDirector director in directors)
        {
            director.Stop();
        }
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
