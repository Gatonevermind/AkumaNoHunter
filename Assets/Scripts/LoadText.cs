//using UnityEngine;
//using System.Collections;
//using System.IO;
//using System;
//using System.Collections.Generic;
//using UnityEngine.UI;
//
//public class LoadText : MonoBehaviour 
//{
//    enum Languages { Spanish, English, French}
//    Languages currentLanguage;
//
//    enum Messages { MSG_Initial_Conversation, MSG_Conversation_1, MSG_Conversation_2, MSG_Conversation_3}
//    private Messages currentMessage;
//
//    public float counterConversation;
//    public bool pauseConversation;
//
//    public Text myText;
//
//	//List<string> gameMsgsList;
//	Dictionary<string, string> gameMsgDic;
//	
//	void Start () 
//	{
//		//gameMsgsList = new List<string>();
//
//		gameMsgDic = new Dictionary<string, string>();
//
//        currentLanguage = Languages.English;
//
//        myText = GetComponent<Text>();
//
//        myText.enabled = false;
//
//        currentMessage = Messages.MSG_Initial_Conversation;
//
//        if (currentLanguage == Languages.Spanish)
//        {
//            using (StreamReader stream = new StreamReader("Assets/Dictionary/Spanish.txt"))
//            {
//                while (!stream.EndOfStream)
//                {
//                    string line = stream.ReadLine();
//                    Console.WriteLine(line);
//
//                    string[] separator = new string[1];
//                    separator[0] = "&&";
//
//                    string[] splittedLine = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
//
//                    string id = splittedLine[0];
//                    string text = splittedLine[1];
//
//                    //gameMsgsList.Add(text);
//                    gameMsgDic.Add(id, text);
//                }
//            }
//        }
//        else if (currentLanguage == Languages.English)
//        {
//            using (StreamReader stream = new StreamReader("Assets/Dictionary/English.txt"))
//            {
//                while (!stream.EndOfStream)
//                {
//                    string line = stream.ReadLine();
//                    Console.WriteLine(line);
//
//                    string[] separator = new string[1];
//                    separator[0] = "&&";
//
//                    string[] splittedLine = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
//
//                    string id = splittedLine[0];
//                    string text = splittedLine[1];
//
//                    //gameMsgsList.Add(text);
//                    gameMsgDic.Add(id, text);
//                }
//            }
//        }
//        else if (currentLanguage == Languages.French)
//        {
//            using (StreamReader stream = new StreamReader("Assets/Dictionary/French.txt"))
//            {
//                while (!stream.EndOfStream)
//                {
//                    string line = stream.ReadLine();
//                    Console.WriteLine(line);
//
//                    string[] separator = new string[1];
//                    separator[0] = "&&";
//
//                    string[] splittedLine = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
//
//                    string id = splittedLine[0];
//                    string text = splittedLine[1];
//
//                    //gameMsgsList.Add(text);
//                    gameMsgDic.Add(id, text);
//                }
//            }
//        }
//	}
//
//	void Update () 
//	{
//		if(ConversationKai.activeConversation)
//		{
//			//string currentLine = gameMsgsList[Messages.MSG_HELLO_WORLD];
//
//			//string currentLine = gameMsgDic[(int)Messages.MSG_HELLO_WORLD];
//
//	        if (currentMessage == Messages.MSG_Initial_Conversation)
//	        {
//	            if (Input.GetKeyDown(KeyCode.E))
//	            {
//	                currentMessage = Messages.MSG_Conversation_1;
//	                pauseConversation = true;
//	            }
//	        }
//
//	        if (currentMessage == Messages.MSG_Conversation_1)
//	        {
//	            counterConversation += Time.deltaTime; 
//	            
//	            if (counterConversation >= 1.0f)
//	            {
//	                if (Input.GetKeyDown(KeyCode.E))
//	                {
//	                    currentMessage = Messages.MSG_Conversation_2;
//	                    counterConversation = 0;
//	                }
//	            }
//
//	            if (counterConversation >= 4.0f)
//	            {
//	                currentMessage = Messages.MSG_Conversation_2;
//	                counterConversation = 0;
//	            }
//	            
//	        }
//
//	        if (currentMessage == Messages.MSG_Conversation_2)
//	        {
//	            counterConversation += Time.deltaTime;
//
//	            if (counterConversation >= 1.0f)
//	            {
//	                if (Input.GetKeyDown(KeyCode.E))
//	                {
//	                    currentMessage = Messages.MSG_Conversation_3;
//	                    counterConversation = 0;
//	                }
//	            }
//
//	            if (counterConversation >= 4.0f)
//	            {
//	                currentMessage = Messages.MSG_Conversation_3;
//	                counterConversation = 0;
//	            }
//	        }
//
//	        if (currentMessage == Messages.MSG_Conversation_3)
//	        {
//	            counterConversation += Time.deltaTime;
//
//	            if (counterConversation >= 1.0f)
//	            {
//	                if (Input.GetKeyDown(KeyCode.E))
//	                {
//	                    myText.enabled = false;
//	                    pauseConversation = false;
//	                }
//	            }
//
//	            if (counterConversation >= 4.0f)
//	            {
//	                myText.enabled = false;
//	                pauseConversation = false;
//	            }
//
//	            if (counterConversation >= 5.0f) currentMessage = Messages.MSG_Initial_Conversation;
//	        }
//
//	        this.gameObject.GetComponent<Text>().text = gameMsgDic[currentMessage.ToString()];
//		}
//	}
//}
