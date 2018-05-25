using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngVoiceWithText : EngVoiceBase {
    public Text _text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Play() {
        if (_text == null) return;

        base.PlayEng(_text.text);
    }
}
