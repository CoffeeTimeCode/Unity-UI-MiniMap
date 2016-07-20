using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public GameObject _player;
	public GameObject _localizador;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void deletar(){
		Destroy (_localizador.GetComponent<Localizador>()._uiLocalizador);
		GameObject.Find ("Mapa").GetComponent<Mapa> ()._listaLocalizador.Remove (_localizador);
		GameObject.Find ("Mapa").GetComponent<Mapa> ().save();
		Destroy (_localizador);
		Destroy (this.gameObject);
	}

	public void teleportar(){
		_player.transform.position = _localizador.transform.position;
	}
}
