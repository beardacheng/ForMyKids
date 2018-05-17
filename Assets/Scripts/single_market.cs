using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class single_market : MonoBehaviour {
    private SpriteRenderer _renderer;
    private SpriteRenderer spriteRenderer {
        get {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
            return _renderer;
        }
    }

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnValidate() {
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show() {
        spriteRenderer.sortingLayerName = "ValidMarket";
    }

    public void Hide() {
        spriteRenderer.sortingLayerName = "InvalidMarket";
    }
}
