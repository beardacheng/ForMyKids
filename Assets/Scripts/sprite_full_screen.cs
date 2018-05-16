using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class sprite_full_screen : MonoBehaviour {
    private Camera _mainCamera;

    private float _screenHeight;
    private float _screenWidth;
    private Vector2 _orgSize;
    

	// Use this for initialization
	void Start () {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        _orgSize = new Vector2(sprite.texture.width / sprite.pixelsPerUnit, sprite.texture.height / sprite.pixelsPerUnit);
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _screenHeight = _mainCamera.orthographicSize * 2;

        _SetScale();   
    }

    private void _SetScale() {
        _screenWidth = _screenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(_screenWidth / _orgSize.x,
                                         _screenHeight / _orgSize.y, 1);
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        _SetScale();
#endif
    }

    

}
