using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class eng_voice : EngVoiceBase {
    public eng_show _engShow = null;
    protected eng_item _current;

    protected override void OnAudioEndEvent() {
        if (_engShow) _engShow.PlayEnd();

        if (_current) {
            if (_current.autoHideText) _engShow.HideText();
        }

        base.OnAudioEndEvent();
    }

    public void PlayEng(eng_item item) {
        base.PlayEng(item.eng);
        _current = item;
    }


    protected override void BeginToPlay(string content) {
        if (_engShow) _engShow.PlayStart(content);
        base.BeginToPlay(content);
    }

    public void HideText() {
        if (_engShow) _engShow.HideText();
    }

    public override void StopPlay() {
        _current = null;

        base.StopPlay();
    }
}
