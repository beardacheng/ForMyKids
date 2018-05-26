using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WitGotSick : MonoBehaviour {
    private IEnumerator _run;
    private EngVoiceBase _engSpeaker;
    private Collider2D _collider;

    public Canvas ui;
    private bool _isWaitingForClick;

    public Dictionary<string, GameObject> objs = new Dictionary<string, GameObject>();
    public Dictionary<string, Text> texts = new Dictionary<string, Text>();

    // Use this for initialization
    void Start() {

        _isWaitingForClick = false;
        _engSpeaker = GetComponent<EngVoiceBase>();
        _collider = GetComponent<Collider2D>();

        _run = Run().GetEnumerator();

        foreach (Transform t in gameObject.transform) {
            objs.Add(t.gameObject.name, t.gameObject);
        }

        foreach (Transform t in ui.transform) {
            Debug.Log(t.name);

            Text text = t.GetComponent<Text>();
            if (text) texts.Add(t.gameObject.name, text);
        }

        Clean();
        _collider.enabled = true;
    }

    private void Clean() {
        foreach (var t in gameObject.GetComponentsInChildren<Transform>()) {
            if (t != gameObject.transform) t.gameObject.SetActive(false);
        }

        foreach (var u in ui.GetComponentsInChildren<RectTransform>()) {
            if (u != ui.transform) u.gameObject.SetActive(false);
        }

        _collider.enabled = false;
    }

    private void WaitForClick() {
        _run.MoveNext();
    }

    private void OnPlayeEngEnd() {
        Debug.Log("OnPlayeEngEnd");
        _engSpeaker.OnAudioEnd -= OnPlayeEngEnd;

        MoveNext();
    }

    private void MoveNext() {
        if (!_isWaitingForClick) _run.MoveNext();
        else _collider.enabled = true;
    }

    private void PlayEng(string text) {
        _engSpeaker.OnAudioEnd += OnPlayeEngEnd;
        _engSpeaker.PlayEng(text);
    }

    // Update is called once per frame
    void Update () {

    }

    private void iTweenBegin() {
        Debug.Log("iTweenBegin");
    }

    private void iTweenEnd() {
        Debug.Log("iTweenEnd");
        MoveNext();
          
    }

    private Hashtable iTweetCallback(Hashtable org) {
        org.Add("onstarttarget", gameObject);
        org.Add("onstart", "iTweenBegin");

        org.Add("oncompletetarget", gameObject);
        org.Add("oncomplete", "iTweenEnd");

        return org;
    }

    private IEnumerable Run() {
        //goto point;
        objs["s_1"].SetActive(true);
        iTween.FadeFrom(objs["s_1"], iTweetCallback(iTween.Hash("alpha", 0.0f, "time", 2)));
        yield return null;

        texts["t_1"].gameObject.SetActive(true);
        PlayEng(texts["t_1"].text);
        _isWaitingForClick = true;
        yield return null;

        Clean();

        objs["mom"].SetActive(true);
        texts["mom"].gameObject.SetActive(true);
        PlayEng(texts["mom"].text);
        yield return null;

        objs["dad"].SetActive(true);
        texts["and dad"].gameObject.SetActive(true);
        PlayEng(texts["and dad"].text);
        yield return null;

        texts["t_2"].gameObject.SetActive(true);
        PlayEng(texts["t_2"].text);
        yield return null;

        objs["car"].gameObject.SetActive(true);
        objs["hospital"].gameObject.SetActive(true);
        iTween.MoveTo(objs["car"], iTweetCallback(iTween.Hash("x", objs["hospital"].transform.position.x, "time", 1.5f, "easetype", "easeOutQuart")));
        _isWaitingForClick = true;
        yield return null;
        
        Clean();

        objs["doctor"].gameObject.SetActive(true);
        texts["t_3"].gameObject.SetActive(true);
        PlayEng(texts["t_3"].text);
        yield return null;

        texts["t_4"].gameObject.SetActive(true);
        PlayEng(texts["t_4"].text);
        objs["s_2"].gameObject.SetActive(true);
        _isWaitingForClick = true;
        yield return null;

        Clean();
        texts["t_5"].gameObject.SetActive(true);
        PlayEng(texts["t_5"].text);
        yield return new WaitForSeconds(1.0f);

        objs["mouth"].gameObject.SetActive(true);
        texts["mouth"].gameObject.SetActive(true);
        PlayEng("mouth");
        yield return new WaitForSeconds(1.0f);

        objs["chest"].gameObject.SetActive(true);
        texts["chest"].gameObject.SetActive(true);
        PlayEng("chest");
        yield return new WaitForSeconds(1.0f);

        objs["thermometer"].gameObject.SetActive(true);
        texts["temperature"].gameObject.SetActive(true);
        PlayEng(texts["temperature"].text);

        _isWaitingForClick = true;
        yield return null;

        Clean();
    
        objs["wow"].gameObject.SetActive(true);
        PlayEng("wow!");
        yield return null;

        texts["t_6"].gameObject.SetActive(true);
        PlayEng(texts["t_6"].text);
        yield return null;

        objs["fever"].gameObject.SetActive(true);
        iTween.FadeFrom(objs["fever"], iTweetCallback(iTween.Hash("alpha", 0.0f, "time", 1)));
        yield return new WaitForSeconds(0.5f);

        texts["t_7"].gameObject.SetActive(true);
        PlayEng(texts["t_7"].text);
        _isWaitingForClick = true;
        yield return null;

        Clean();

        objs["doctor_1"].gameObject.SetActive(true);
        texts["t_8"].gameObject.SetActive(true);
        PlayEng(texts["t_8"].text);
        yield return null;

        objs["medicine"].gameObject.SetActive(true);
        iTween.FadeFrom(objs["medicine"], iTweetCallback(iTween.Hash("alpha", 0.0f, "time", 1)));
        _isWaitingForClick = true;
        yield return null;

        Clean();
point:
        objs["take_medicine"].gameObject.SetActive(true);
        iTween.FadeFrom(objs["take_medicine"], iTweetCallback(iTween.Hash("alpha", 0.0f, "time", 1)));
        texts["t_9"].gameObject.SetActive(true);
        PlayEng(texts["t_9"].text);
        yield return null;

        yield return new WaitForSeconds(0.5f);

        texts["t_10"].gameObject.SetActive(true);
        PlayEng(texts["t_10"].text);
        yield return null;

        objs["feel_better"].gameObject.SetActive(true);
        iTween.FadeFrom(objs["feel_better"], iTweetCallback(iTween.Hash("alpha", 0.0f, "time", 1)));

        yield break;
    }

    private void OnMouseUp() {
        Debug.Log("clicked");
        _isWaitingForClick = false;
        MoveNext();
    }
}
