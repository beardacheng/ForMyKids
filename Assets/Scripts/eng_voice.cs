using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class eng_voice : MonoBehaviour {
    [Multiline]
    public string sentence = "";

    public eng_show _engShow = null;

    private AudioSource _audioSource = null;

    //audio event
    public delegate void AudioCallBack();
    public event AudioCallBack OnAudioEnd;

    //audio cache
    private static LocalBinFileCache _cache;

    //co
    Coroutine _playAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _cache = new LocalBinFileCache("audio_file_cache");
        OnAudioEnd += OnAudioEndEvent;
    }

    IEnumerator Start()
    {
        //auto play
        if (this._audioSource.playOnAwake && this.sentence != "")
        {
            yield return this._PlayEng(this.sentence);
        }
    }

    private void OnAudioEndEvent() {

    }

    public void PlayEng(string content)
    {
        StartCoroutine(this._PlayEng(content));
    }

    public IEnumerator _PlayEng(string content)
    {
        Debug.Log("play eng: " + content);
        AudioSource audioSrouce = this._audioSource;
        audioSrouce.clip = null;
        audioSrouce.Stop();

        if (null != _playAudio) StopCoroutine(_playAudio);

        if (content == "")
        {
            if (sentence == "") yield break;
        }
        else sentence = content;

        string audioFilePath = _cache.GetFilePath(sentence);

        if (audioFilePath == "") {
            var path = "https://fanyi.baidu.com/gettts?lan=en&text=" + sentence + "&spd=3";
            Debug.Log("load voice remote: " + path);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            yield return _RequestAudioByWebRequestInPC(path);
#else
            yield return _RequestAudioByWebRequest(path);
#endif
        }
        else {
            var path = "file://" + audioFilePath;
            Debug.Log("load voice in cache: " + path);
            yield return _RequestAudioByWebRequest(path, AudioType.WAV);
        }

        if (audioSrouce.clip != null) {
            if ("" == audioFilePath) {
                _cache.Save(sentence,  T_AudioClipToByteArray.AudioClipToByteArray(audioSrouce.clip) , false);
            }

            if (this._engShow)
            {
                this._engShow.ShowText(this.sentence);
            }
            audioSrouce.Play();
            _playAudio = StartCoroutine(DelayedCallback(audioSrouce.clip.length));
        }
    }

    private IEnumerator _RequestAudioByWebRequest(string path, AudioType type = AudioType.MPEG) {
        using (var audio_clip_request = UnityWebRequestMultimedia.GetAudioClip(path, type))
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
