using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


/// <summary>
/// XML Data class used to load specific character dialogue from an XML file
/// 
/// 
/// 
/// Liam MacLean 28/01/2018 19:23
/// </summary>



//Dialogue XML class
public class XMLDialogueText
{
	[XmlAttribute("CharacterName")]
	private string m_name;

	[XmlElement("CharacterDialogue")]
	private string m_CharacterDialogue;

	//get method for dialogue
	public string GetCharacterDialogue()
	{
		return m_CharacterDialogue;
	}

	//get method for character name
	public string GetCharacterName()
	{
		return m_name;
	}
		
	//base constructor (Required for XML parsing)
	public XMLDialogueText()
	{
	}

	//constructor for editor
	public XMLDialogueText(string name, string characterDialogue)
	{
		m_CharacterDialogue = characterDialogue;
		m_name = name;
	}



}