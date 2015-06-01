using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	public Texture2D currentCursorTexture;
	public Texture2D cursorTexture1;
	public Texture2D cursorTexture2;
	public Texture2D cursorTexture3;
	public Texture2D cursorTexture4;
	public Texture2D cursorTexture5;
	public Texture2D cursorTexture6;
	public Texture2D cursorTexture7;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public int cursorSizeX;
	public int cursorSizeY;

	//public GameObject disableOne;
	private bool disableCursor;

	private bool animationCursor;
	private float counterAnim;
	public float speedAnim;

	private int currentSceneNum;

	void Start()
	{	
		//disableCursor = false;
		currentCursorTexture = cursorTexture1;
		speedAnim = 20.0f;
		animationCursor = false;
		//Screen.showCursor = false;;
		Cursor.visible = false;
	}

	void OnGUI()
	{
		GUI.DrawTexture (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorSizeX, cursorSizeY), currentCursorTexture);
		//GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Pressed left click.");

			animationCursor = true;
		}

		if (animationCursor) {
			counterAnim += speedAnim * Time.deltaTime;
			
			if (counterAnim >= 1)
				currentCursorTexture = cursorTexture2;
			if (counterAnim >= 2)
				currentCursorTexture = cursorTexture3;
			if (counterAnim >= 3)
				currentCursorTexture = cursorTexture4;
			if (counterAnim >= 4)
				currentCursorTexture = cursorTexture5;
			if (counterAnim >= 5)
				currentCursorTexture = cursorTexture6;
			if (counterAnim >= 6)
				currentCursorTexture = cursorTexture7;
			if (counterAnim >= 7) {
				currentCursorTexture = cursorTexture1;
				animationCursor = false;
			}
		}
		
		if (!animationCursor) {
			currentCursorTexture = cursorTexture1;
			counterAnim = 0;
		}

		/*if (disableCursor) {
			disableOne.SetActive(false);
		}
		
		if ((!disableCursor && IntroCinematic.intro)&& !BossEvent.bossCinematic) {
			disableOne.SetActive(true);
		}*/
	}

	void OnMouseEnter() {
		Cursor.SetCursor(currentCursorTexture, hotSpot, cursorMode);
	}

	void OnMouseExit() {
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}
}