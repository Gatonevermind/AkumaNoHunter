using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadText : MonoBehaviour 
{
    enum Messages { MSG_Initial_Conversation, MSG_Conversation_1, MSG_Conversation_2, MSG_Conversation_3}

    private Messages currentMessage;

    public float counterConversation;
    public bool pauseConversation;

    public Text myText;

	//List<string> gameMsgsList;
	//Dictionary<string, string> gameMsgDic;
	
	void Start () 
	{
		//gameMsgsList = new List<string>();

		//gameMsgDic = new Dictionary<string, string>();

        myText = GetComponent<Text>();

        myText.enabled = false;

        currentMessage = Messages.MSG_Initial_Conversation;

		/*
		using (StreamReader stream = new StreamReader("TestFile.txt"))
		{
			while (!stream.EndOfStream)
			{
				string line = stream.ReadLine();
				Console.WriteLine(line);

				string[] splittedLine = line.Split(" @ ", StringSplitOptions.RemoveEmptyEntries);

				string id = splittedLine[0];
				string text = splittedLine[1];

				//gameMsgsList.Add(text);
				gameMsgDic.Add(id, text);
			}
		}
		*/
	}

	void Update () 
	{
		//string currentLine = gameMsgsList[Messages.MSG_HELLO_WORLD];

		//string currentLine = gameMsgDic[Messages.MSG_HELLO_WORLD.ToString()];

        if (currentMessage == Messages.MSG_Initial_Conversation)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentMessage = Messages.MSG_Conversation_1;
                pauseConversation = true;
            }
        }

        if (currentMessage == Messages.MSG_Conversation_1)
        {
            counterConversation += Time.deltaTime; 
            
            if (counterConversation >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentMessage = Messages.MSG_Conversation_2;
                    counterConversation = 0;
                }
            }

            if (counterConversation >= 4.0f)
            {
                Debug.Log("Tiempo");
                currentMessage = Messages.MSG_Conversation_2;
                counterConversation = 0;
            }
            
        }

        if (currentMessage == Messages.MSG_Conversation_2)
        {
            counterConversation += Time.deltaTime;

            if (counterConversation >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentMessage = Messages.MSG_Conversation_3;
                    counterConversation = 0;
                }
            }

            if (counterConversation >= 4.0f)
            {
                Debug.Log("Tiempo");
                currentMessage = Messages.MSG_Conversation_3;
                counterConversation = 0;
            }
        }

        if (currentMessage == Messages.MSG_Conversation_3)
        {
            counterConversation += Time.deltaTime;

            if (counterConversation >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    myText.enabled = false;
                    pauseConversation = false;
                }
            }

            if (counterConversation >= 4.0f)
            {
                myText.enabled = false;
                pauseConversation = false;
            }

            if (counterConversation >= 5.0f) currentMessage = Messages.MSG_Initial_Conversation;
        }

        switch (currentMessage)
        {
            case Messages.MSG_Initial_Conversation:
                this.gameObject.GetComponent<Text>().text = "Presiona E para hablar con Kay";
                break;
            case Messages.MSG_Conversation_1:
                this.gameObject.GetComponent<Text>().text = "Cazador! Debes ir a la zona de entrenamiento!";
                break;
            case Messages.MSG_Conversation_2:
                this.gameObject.GetComponent<Text>().text = "La aldea esta siendo atacada por lobos";
                break;
            case Messages.MSG_Conversation_3:
                this.gameObject.GetComponent<Text>().text = "El puente esta roto! busca una alternativa para cruzar el rio!";
                break;

        }
	}
}
