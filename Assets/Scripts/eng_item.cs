using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class eng_item : MonoBehaviour {
    protected eng_voice _speaker;

    [Multiline]
    public string eng;

    public bool autoHideText = false;

    protected virtual void Start()
    {
        _speaker = FindSpeaker();
    }

    protected eng_voice FindSpeaker()
    {
        return GameObject.Find("Eng Speaker").GetComponent<eng_voice>();
    }

    protected void _PlayEng()
    {
        _speaker.PlayEng(this);
        _speaker.OnAudioEnd += OnEngVoiceEnd;
    }

    protected virtual void OnMouseUp()
	{
        _PlayEng();
    }

    protected virtual void OnEngVoiceEnd() {

        _speaker.OnAudioEnd -= OnEngVoiceEnd;
    }
}
