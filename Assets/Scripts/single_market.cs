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
    }


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show() {
        _renderer.sortingLayerName = "ValidMarket";
    }

    public void Hide() {
        _renderer.sortingLayerName = "InvalidMarket";
    }
}
