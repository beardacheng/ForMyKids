using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class eng_voice : MonoBehaviour {
    [Multiline]
    public string sentence = "";

    public eng_show _engShow = null; 

    private AudioSource _audioSource = null;

    //audio event
    public delegate void AudioCallBack();
    public event AudioCallBack OnAudioEnd;

    //co
    Coroutine _playAudio;

    IEnumerator Start()
	{
        this._audioSource = GetComponent<AudioSource>();

        //auto play
        if (this._audioSource.playOnAwake && this.sentence != "")
        {
            yield return this._PlayEng(this.sentence);
        }

        this.OnAudioEnd += OnAudioEndEvent;
	}

    private void OnAudioEndEvent() {
        
    }

    public void PlayEng(string content)
    {
        StartCoroutine(this._PlayEng(content));
    }

    public IEnumerator _PlayEng(string content)
    {
        AudioSource audioSrouce = this._audioSource;
        audioSrouce.clip = null;
        audioSrouce.Stop();

        if (null != _playAudio) StopCoroutine(_playAudio);

        if (content == "")
        {
            if (this.sentence == "") yield break;
        }
        else this.sentence = content;

        var path = "https://fanyi.baidu.com/gettts?lan=en&text=" + sentence + "&spd=3";
        yield return _RequestAudioByWebRequest(path);

        if (audioSrouce.clip != null)
        {
            if (this._engShow)
            {
                this._engShow.ShowText(this.sentence);
            }
            audioSrouce.Play();
            _playAudio = StartCoroutine(DelayedCallback(audioSrouce.clip.length));
        }
    }

    private IEnumerator _RequestAudioByWebRequest(string path) {
        Debug.Log(path);
        using (var audio_clip_request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return audio_clip_request.SendWebRequest();
            if (audio_clip_request.isNetworkError || audio_clip_request.isHttpError)
            {
                Debug.LogError(audio_clip_request.error);
                yield break;
            }

            this._audioSource.clip = DownloadHandlerAudioClip.GetContent(audio_clip_request);
        }
    }

    private IEnumerator DelayedCallback(float time)  
    {  
        yield return new WaitForSeconds (time);
        _playAudio = null;
        if (OnAudioEnd != null) {
            OnAudioEnd();
        }
    } 

    public void StopPlay(){
        this._audioSource.Stop();
        if (this._engShow) this._engShow.HideText();
    }
}
