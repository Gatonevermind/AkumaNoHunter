using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(Screen.width - 100, Screen.height -100, 100, 60), "Assets");

        
        if (GUI.Button(new Rect(Screen.width - 90, Screen.height - 70, 80, 20), "Enemy"))
        {
            GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);

            enemy.name = "Enemy";
            enemy.tag = "Enemy";

            enemy.transform.position = new Vector3(Random.Range(-23, 23), 0.5F, Random.Range(-23, 23));

            enemy.AddComponent<CharacterController>();
            enemy.AddComponent<Rigidbody>();
            enemy.AddComponent("EnemyAI");
            enemy.AddComponent("EnemyHealth");
            

                
        }

    }
}
