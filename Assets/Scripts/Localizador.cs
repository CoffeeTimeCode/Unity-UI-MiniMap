using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Localizador : MonoBehaviour {

	public Image _image;
	public Text _text;
	public string _nome; 
	public Color _cor;

	public Text _distancia;

	public GameObject _player;
	public Marcador _marcador;

	public GameObject _uiLocalizador;

	// Use this for initialization
	void Start () {
		_marcador._cor = _cor;
		_marcador._nome = _nome;

		GameObject _temp = Instantiate (_uiLocalizador)	 as GameObject;
		_temp.GetComponent<RectTransform> ().SetParent (GameObject.Find("Canvas").GetComponent<RectTransform>());

		_image = _temp.transform.Find ("Image").GetComponent<Image>();
		_text = _temp.transform.Find ("Nome").GetComponent<Text>();
		_distancia = _temp.transform.Find ("Distancia").GetComponent<Text>();

		_image.color = _cor;
		_text.text = _nome;

		_uiLocalizador = _temp;
	}
	
	// Update is called once per frame
	void Update () {
		_distancia.text = Vector3.Distance (this.transform.position,_player.transform.position).ToString("0.0")+"M";
		_uiLocalizador.GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint (this.transform.position);
		if(_uiLocalizador.GetComponent<RectTransform> ().position.z < 0 )
		{
			_uiLocalizador.GetComponent<RectTransform> ().position = new Vector3(-25.0f,0.0f,0.0f);
		}
	}
}
