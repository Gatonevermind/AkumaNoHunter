using UnityEngine;
using System.Collections;

public class InputBehaviour : MonoBehaviour {

	// Cuando se llama, notifica a todos los métodos que hacen referencia al delegate (DownHandler).
	public delegate void DownHandler( int button );
	// evento que se activa cuando aparece el DownHandler
	public event DownHandler onDown;

	// Funcion que se llama desde InputManger, activa el evento onDown de éste y otras clases
	public void triggerDown(int button){
		if( onDown != null ) 
			onDown(button);
	}

}

