using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

	//item collection class used to store all the items in the game currently in the database (XML file)

[XmlRoot("DialogueDatabase")]
public class XMLDialogueDatabase
{
	[XmlArray("DialogueList")]
	[XmlArrayItem("Dialogue")]
	//list of database items
	public List<XMLDialogueText> dialogueText = new List<XMLDialogueText>();

	//constructor
	public XMLDialogueDatabase()
	{

	}
		
	//Creates and returns list of Dialogue from a certain character
	//Choose from: Dennis, Gnasher, Flat faced girl, Pie guy, Dennis Dad
	public List<string> RetrieveCharacterDialogueList(string Character)
	{
		//create temporary string list of dialogue 
		List<string> dialogue = new List<string>();

		//For each item in the database
		for (int i = 0; i < dialogueText.Count; i++) {
			//if the character name the item corresponds with the character dialogue we want
			if (dialogueText [i].GetCharacterName() == Character) {
				//retrieve the dialogue from item
				dialogue.Add (dialogueText [i].GetCharacterDialogue());
			}
		}

		//return the list after everything has been retrieved
		return dialogue;
	}
}

