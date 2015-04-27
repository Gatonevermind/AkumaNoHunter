using UnityEngine;
using System.Collections;

public class ConversationKai : MonoBehaviour 
{
    public Transform target;

    public AnimationClip talk;
    public AnimationClip idle;
    
    public bool activeConversation;
    //GameObject[] conversationControl;

    public Camera cam1;
    public Camera cam2;

    void Start() 
    {
        cam1.enabled = true;
        cam2.enabled = false;

        activeConversation = false;

        //conversationControl = GameObject.FindGameObjectsWithTag("ConversationKai");

        //foreach (GameObject conversation in conversationControl)
            //conversation.SetActive(false);
    }
    void Update()
    {
         GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
        LoadText loadText = disableConversation.GetComponent<LoadText>();

        GetComponent<Animation>().CrossFade(idle.name);

        if (activeConversation)
        {
            transform.LookAt(target);
            GetComponent<Animation>().CrossFade(talk.name);

           // foreach (GameObject conversation in conversationControl)
               // conversation.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                cam1.enabled = false;
                cam2.enabled = true;   
            }
        }
        
        if (loadText.myText.enabled == false)
        {
            //foreach (GameObject conversation in conversationControl)
                //conversation.SetActive(false);

            cam2.enabled = false;
            cam1.enabled = true;
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = true;
            Debug.Log("Enable");
            activeConversation = true;
 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeConversation = false;

            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = false;
        }
    }
}
