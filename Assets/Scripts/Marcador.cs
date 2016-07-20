using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Marcador : MonoBehaviour {

	public Image _image;
	public Text _text;
	public string _nome; 
	public Color _cor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		_text.text = _nome;
		_image.color = _cor;
	}
}
