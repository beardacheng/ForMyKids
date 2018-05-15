using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class eng_item : MonoBehaviour {
    public eng_voice speaker;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
        speaker.OnAudioEnd += OnEngVoiceEnd;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
	}

    void OnEngVoiceEnd() {
        speaker.StopPlay();
        speaker.OnAudioEnd -= OnEngVoiceEnd;
    }
}
