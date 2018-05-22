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

    public string engItemTemplate = "";

    [SerializeField]
    protected List<string> _itemNames;

    private void Awake() {
        _itemNames = new List<string>();
        foreach (Transform trans in gameObject.transform) {
            _itemNames.Add(trans.name);
        }


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

    public void ClickItem(string name) {
        _itemNames.Remove(name);

        if (_itemNames.Count == 0) {
            GameObject.FindGameObjectWithTag("MarketController").GetComponent<find_in_market>().NextMarket();
            GameObject.Find("Eng Speaker").GetComponent<eng_voice>().HideText();
        }
    }

    [ContextMenu("SetEngItems")]
    void SetEngItems() {
        Debug.Log("Exec SetEngItems");

        if (string.IsNullOrEmpty(engItemTemplate)) return;

        foreach (var item in gameObject.GetComponentsInChildren<eng_item>()) {
            item.eng = string.Format(engItemTemplate, item.gameObject.name);
        }
    }
}
