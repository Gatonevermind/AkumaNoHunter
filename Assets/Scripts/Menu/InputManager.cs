using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private Vector3 lastPosition;

	private InputBehaviour selectedInput;
	
	void Update () {
		
		/*Touch handling: */
		if( Input.touchCount == 1 ){
			Touch touch = Input.touches[0];
			// EVENTO DE TOCAR LA PANTALLA TÁCTIL
			if( touch.phase == TouchPhase.Began ){
			 	// CUANDO PRESIONAMOS...
				handleDown(touch.fingerId, touch.position);
			}
		}  else if( Input.touchCount == 0 ){
			/*Mouse handling*/
			// EVENTO DE CLIC CON EL MOUSE
			if( Input.GetMouseButtonDown(0) ){
				// CUANDO PRESIONAMOS...
				handleDown(0, Input.mousePosition);
			}  
		}
		
	}
	
	private void handleDown(int button, Vector3 position){
		// VAMOS A DETECTAR SI TOCAMOS ALGO
		selectedInput = getGameObject( position, out lastPosition );

		if( selectedInput != null ){
			// SI LO QUE HEMOS TOCADO NO ESTÁ VACÍO, 
			Debug.Log(selectedInput.gameObject.name );
			// LLAMAMOS A SU ACCION DE DENTRO DEL INPUTBEHAVIOUR
			selectedInput.triggerDown(button);
		}
	}
	
	private InputBehaviour getGameObject( Vector3 screenPos, out Vector3 point ){
		InputBehaviour result = null;
		
		// Construct a ray from the current touch coordinates
		// LANZAMOS UN RAYCAST DESDE LA PANTALLA
		Ray ray = camera.ScreenPointToRay( screenPos );
		RaycastHit hit;

		if ( Physics.Raycast( ray, out hit ) ) {
			Debug.Log("RayCast");
			// SI EL RAYCAST TOCA UN ELEMENTO CON INPUTBEHAVIOUR, LO RECOGEMOS
			result = hit.transform.gameObject.GetComponent<InputBehaviour>();
			point = hit.point;

		}  else {
			point = Vector3.zero;
			//distance = 0;
		}
		
		return result;
	}

}
