using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class eng_show : MonoBehaviour {
    Text _engText;

	// Use this for initialization
	void Start () {
        this._engText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowText(string content){
        this._engText.text = content;
        this.gameObject.transform.SetAsLastSibling();
    }

    public void HideText() {
        this.gameObject.transform.SetAsFirstSibling();
    }
}
