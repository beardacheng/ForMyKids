using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class eng_item : MonoBehaviour {
    public eng_voice speaker;
    public bool speak_on_start = false;

    [Multiline]
    public string eng;

	// Use this for initialization
	void Start () {
        if (speak_on_start) _PlayEng();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void _PlayEng()
    {
        speaker.PlayEng(eng);
        speaker.OnAudioEnd += OnEngVoiceEnd;
    }

	void OnMouseUp()
	{
        _PlayEng();

    }

    void OnEngVoiceEnd() {

        speaker.OnAudioEnd -= OnEngVoiceEnd;
    }
}
