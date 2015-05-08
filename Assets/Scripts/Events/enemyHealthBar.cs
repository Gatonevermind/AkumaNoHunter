using UnityEngine;
using System.Collections;
public class enemyHealthBar : MonoBehaviour
{
    public Transform target;
    // Use this for initialization
    void Start()
    {
        //target = transform.parent;
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        Debug.Log("target is " + screenPos.x + " pixels from the left");
    }
}