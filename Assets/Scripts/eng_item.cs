using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class eng_item : MonoBehaviour {
    public eng_voice speaker;
    public bool speak_on_awake = false;

    [Multiline]
    public string eng;

	// Use this for initialization
	void Start () {
        
	}

    private void Awake()
    {
        speaker.PlayEng(eng);
    }

    // Update is called once per frame
    void Update () {
		
	}

	void OnMouseUp()
	{
        speaker.OnAudioEnd += OnEngVoiceEnd;
	}

    void OnEngVoiceEnd() {

        speaker.OnAudioEnd -= OnEngVoiceEnd;
    }
}
