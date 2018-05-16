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
        _init();

        //auto play
        if (this._audioSource.playOnAwake && this.sentence != "")
        {
            yield return this._PlayEng(this.sentence);
        }
    }

    private void _init()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            OnAudioEnd += OnAudioEndEvent;
        }
    }

    private void OnAudioEndEvent() {

    }

    public void PlayEng(string content)
    {
        _init();
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
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        yield return _RequestAudioByWebRequestInPC(path);
#else
        yield return _RequestAudioByWebRequest(path);
#endif

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

    private IEnumerator _RequestAudioByWebRequestInPC(string path)
    {
        Debug.Log(path);
        using (var audio_clip_request = UnityWebRequest.Get(path))
        {
            yield return audio_clip_request.SendWebRequest();
            if (audio_clip_request.isNetworkError || audio_clip_request.isHttpError)
            {
                Debug.LogError(audio_clip_request.error);
                yield break;
            }

            this._audioSource.clip = NAudioPlayer.FromMp3Data(audio_clip_request.downloadHandler.data);

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
        if (_audioSource) _audioSource.Stop();
        if (_engShow) _engShow.HideText();
    }
}
