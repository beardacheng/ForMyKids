using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eng_show : MonoBehaviour {
    public Text _engText;

    private Image __image;
    private Image _image {
        get {
            if (__image == null) __image = gameObject.GetComponent<Image>();
            return __image;
        }
    }

    private bool _isPlaying = false;

    // Use this for initialization
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PlayStart(string content){
        _engText.text = content;
        gameObject.SetActive(true);

        _isPlaying = true;
    }

    public void HideText() {
        PlayEnd();
        gameObject.SetActive(false);
    }

    public void PlayEnd() {
        _isPlaying = false;
    }

    private void OnMouseUp() {
        if (!_isPlaying) HideText();
    }
}
