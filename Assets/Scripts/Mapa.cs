using UnityEngine;
using UnityEngine.UI; //
using System.Collections;
using System.Collections.Generic;

public class Mapa : MonoBehaviour {

	public GameObject _player;
	public GameObject _mapaMenu;
	public Text _posicao;

	public InputField _nome;
	public InputField _x;
	public InputField _y;
	public InputField _z;

	public Image _viewCor;

	public GameObject _localizador;

	public List<GameObject> _listaLocalizador = new List<GameObject>();

	public GameObject _uiLista;
	public GameObject _uiItem;

	// Use this for initialization
	void Start () {
		load ();
		_mapaMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	 _posicao.text = "Posiçao Atual\n" +
			" X: " + _player.transform.position.x.ToString ("0.00") +
			" Y: " + _player.transform.position.y.ToString ("0.00") +
			" z: " + _player.transform.position.z.ToString ("0.00");

		_x.text = _player.transform.position.x.ToString ("0.00");
		_y.text = _player.transform.position.y.ToString ("0.00");
		_z.text = _player.transform.position.z.ToString ("0.00");

		if(Input.GetKeyDown(KeyCode.M)){
			_mapaMenu.SetActive (!_mapaMenu.activeSelf);
		}
	}

	public void TrocarCor()
	{
		_viewCor.color = new Color (Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
	}

	public void CriarLocalizador()
	{
		GameObject _objeto = Instantiate (_localizador,_player.transform.position,_localizador.transform.rotation) as GameObject;
		_objeto.GetComponent<Localizador> ()._player = _player;
		_objeto.GetComponent<Localizador> ()._nome = _nome.text;
		_objeto.GetComponent<Localizador> ()._cor = _viewCor.color;
		_listaLocalizador.Add (_objeto);
		save ();

		GameObject _objItem = Instantiate (_uiItem);
		_objItem.transform.FindChild ("Nome").GetComponent<Text> ().text = _nome.text;
		_objItem.transform.FindChild ("Posição").GetComponent<Text> ().text = "Posição: "+_player.transform.position;

		_objItem.GetComponent<Item> ()._player = _player;
		_objItem.GetComponent<Item> ()._localizador = _objeto;

		_objItem.transform.SetParent (_uiLista.transform);
	}	

	public void save(){
		limparLista ();
		for (int i = 0; i < _listaLocalizador.Count; i++) {
			PlayerPrefs.SetFloat ("x id: "+ i,_listaLocalizador[i].transform.position.x);
			PlayerPrefs.SetFloat ("y id: "+ i,_listaLocalizador[i].transform.position.y);
			PlayerPrefs.SetFloat ("z id: "+ i,_listaLocalizador[i].transform.position.z);

			PlayerPrefs.SetString ("nome id: "+ i,_listaLocalizador[i].GetComponent<Localizador>()._nome);

			PlayerPrefs.SetFloat ("r id: "+ i,_listaLocalizador[i].GetComponent<Localizador>()._cor.r);
			PlayerPrefs.SetFloat ("g id: "+ i,_listaLocalizador[i].GetComponent<Localizador>()._cor.g);
			PlayerPrefs.SetFloat ("b id: "+ i,_listaLocalizador[i].GetComponent<Localizador>()._cor.b);
		}
		PlayerPrefs.SetInt ("total",_listaLocalizador.Count);
	}

	public void limparLista(){
		for (int i = 0; i < PlayerPrefs.GetInt("total"); i++) {
			PlayerPrefs.DeleteKey ("x id: "+ i);
			PlayerPrefs.DeleteKey ("y id: "+ i);
			PlayerPrefs.DeleteKey ("z id: "+ i);

			PlayerPrefs.DeleteKey ("nome id: "+ i);

			PlayerPrefs.DeleteKey ("r id: "+ i);
			PlayerPrefs.DeleteKey ("g id: "+ i);
			PlayerPrefs.DeleteKey ("b id: "+ i);
		}
	}

	public void load(){		
		for (int i = 0; i < PlayerPrefs.GetInt("total"); i++) {
			GameObject _objeto = Instantiate (_localizador,new Vector3(PlayerPrefs.GetFloat("x id: "+i),PlayerPrefs.GetFloat("y id: "+i),PlayerPrefs.GetFloat("z id: "+i)),_localizador.transform.rotation) as GameObject;
			_objeto.GetComponent<Localizador> ()._player = _player;
			_objeto.GetComponent<Localizador> ()._nome = PlayerPrefs.GetString("nome id: "+i);
			_objeto.GetComponent<Localizador> ()._cor = new Vector4(PlayerPrefs.GetFloat("r id: "+i),PlayerPrefs.GetFloat("g id: "+i),PlayerPrefs.GetFloat("b id: "+i),1.000f);
			_listaLocalizador.Add (_objeto);

			GameObject _objItem = Instantiate (_uiItem);
			_objItem.transform.FindChild ("Nome").GetComponent<Text> ().text = PlayerPrefs.GetString("nome id: "+i);
			_objItem.transform.FindChild ("Posição").GetComponent<Text> ().text = "Posição: "+new Vector3(PlayerPrefs.GetFloat("x id: "+i),PlayerPrefs.GetFloat("y id: "+i),PlayerPrefs.GetFloat("z id: "+i));

			_objItem.GetComponent<Item> ()._player = _player;
			_objItem.GetComponent<Item> ()._localizador = _objeto;

			_objItem.transform.SetParent (_uiLista.transform);
		}
	}
}	
