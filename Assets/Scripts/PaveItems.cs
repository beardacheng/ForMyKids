using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaveItems : MonoBehaviour {
    public GameObject startItem;
    private Collider2D _collider;

    private GameObject _lastItem;
    private GameObject _lineStartItem;

    private int _lastLineCount;
    private int _thisLineCount;
    private int _lineNo;
    private bool _paved;

	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("TogglePave")]
    private void TogglePave() {
        _paved = gameObject.transform.childCount > 1;
        if (!_paved) {
            Pave();
            _paved = true;
        }
        else {
            DePave();
            _paved = false;
            InitCount();
        }
    }

    private void InitCount() {
        _lastLineCount = -1;
        _thisLineCount = 1;
        _lineNo = 1;
    }

    private void DePave() {
        foreach (var item in gameObject.GetComponentsInChildren<Transform>()) {
            if (item.gameObject != startItem && item.gameObject != gameObject) DestroyImmediate(item.gameObject);
        }
    }

    private void Pave() {
        _collider = GetComponent<Collider2D>();
        _lastItem = startItem;
        _lineStartItem = startItem;

        InitCount();

        while(true) {
            if (!_PlaceNext()) break;
        }

    }

    private bool _PlaceNext() {
        var startBound = _lastItem.GetComponent<Renderer>().bounds;
        var startPos = _lastItem.transform.position;

        var newPos = new Vector3(startPos.x, startPos.y, startPos.z);

        newPos.x = _lastItem.transform.position.x + startBound.size.x;
        var newItem = Instantiate(_lastItem, newPos, _lastItem.transform.rotation, gameObject.transform);

        if (_collider.bounds.Intersects(newItem.GetComponent<Renderer>().bounds)
            && (_lastLineCount <= 0 || _thisLineCount + 1 < _lastLineCount)) {
            _lastItem = newItem;
            _thisLineCount++;
            newItem.name = string.Format("{0}_{1}_{2}", startItem.name, _lineNo, _thisLineCount);
            return true;
        }
        else {
            newPos = new Vector3(_lineStartItem.transform.position.x + startBound.size.x * 0.5f, 
                                 _lineStartItem.transform.position.y + startBound.size.y * 0.7f,
                                 _lineStartItem.transform.position.z + 0.01f);
            newItem.transform.position = newPos;

            if (_collider.bounds.Intersects(newItem.GetComponent<Renderer>().bounds)) {
                _lastItem = _lineStartItem = newItem;
                _lastLineCount = _thisLineCount;
                _thisLineCount = 1;
                _lineNo++;
                newItem.name = string.Format("{0}_{1}_{2}", startItem.name, _lineNo, _thisLineCount);
                return true;
            }
            else {
                DestroyImmediate(newItem);
                return false;
            }
        }
    }
}
