using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

public class EngVoiceBase : MonoBehaviour {
    private AudioSource _audioSource = null;

    //audio event
    public delegate void AudioCallBack();
    public event AudioCallBack OnAudioEnd;

    //audio cache
    private static LocalBinFileCache _cache;

    //co
    private Coroutine _playAudio;

    protected virtual void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _cache = new LocalBinFileCache("audio_file_cache");
        OnAudioEnd += OnAudioEndEvent;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void OnAudioEndEvent() {
        
    }

    public void PlayEng(string content) {
        StartCoroutine(_PlayEng(content));
    }

    protected IEnumerator _PlayEng(string content) {
        Debug.Log("play eng: " + content);

        if (content == "") yield break;

        StopPlay();

        string audioFilePath = _cache.GetFilePath(content);

        if (audioFilePath == "") {
            var path = "https://fanyi.baidu.com/gettts?lan=en&text=" + content + "&spd=3";
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

        if (_audioSource.clip != null) {
            if ("" == audioFilePath) {
                _cache.Save(content, T_AudioClipToByteArray.AudioClipToByteArray(_audioSource.clip), false);
            }

            BeginToPlay(content);
        }
    }

    protected virtual void BeginToPlay(string content) {
        
        _audioSource.Play();
        _playAudio = StartCoroutine(DelayedCallback(_audioSource.clip.length));
    }

    private IEnumerator _RequestAudioByWebRequest(string path, AudioType type = AudioType.MPEG) {
        using (var audio_clip_request = UnityWebRequestMultimedia.GetAudioClip(path, type)) {
            yield return audio_clip_request.SendWebRequest();
            if (audio_clip_request.isNetworkError || audio_clip_request.isHttpError) {
                Debug.LogError(audio_clip_request.error);
                yield break;
            }

            _audioSource.clip = DownloadHandlerAudioClip.GetContent(audio_clip_request);
        }
    }

    private IEnumerator _RequestAudioByWebRequestInPC(string path) {
        using (var audio_clip_request = UnityWebRequest.Get(path)) {
            yield return audio_clip_request.SendWebRequest();
            if (audio_clip_request.isNetworkError || audio_clip_request.isHttpError) {
                Debug.LogError(audio_clip_request.error);
                yield break;
            }

            _audioSource.clip = NAudioPlayer.FromMp3Data(audio_clip_request.downloadHandler.data);

        }
    }

    private IEnumerator DelayedCallback(float time) {
        yield return new WaitForSeconds(time);
        _playAudio = null;
        if (OnAudioEnd != null) {
            OnAudioEnd();
        }

        StopPlay();
    }

    public virtual void StopPlay() {
        if (null != _playAudio) StopCoroutine(_playAudio);

        if (_audioSource.clip) _audioSource.Stop();
        _audioSource.clip = null;
    }
}
