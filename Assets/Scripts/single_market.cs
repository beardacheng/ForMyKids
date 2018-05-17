using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class single_market : MonoBehaviour {
    private SpriteRenderer __renderer;
    private SpriteRenderer _renderer {
        get {
            if (__renderer == null) __renderer = GetComponent<SpriteRenderer>();
            return __renderer;
        }
    }

    private void Awake() {
        gameObject.SetActive(false);
    }


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
