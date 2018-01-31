using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


//Serializer for xml serializing and deserializing (save and load)
public class XMLSerializer
{
	//serializer
	public static void Serialize<T>(string outputFileName, T objToWrite, string path)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		using (FileStream stream = File.Create(path + "/" + outputFileName))
		{

			XmlSerializer serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(stream, objToWrite);
		}
	}


	//Deserializer (Filename and path)
	public static T Deserialize<T>(string fileName, string path)
	{
		T instance;

		using (FileStream stream = File.OpenRead(path + "/" + fileName))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			instance = (T)serializer.Deserialize(stream);
		}
		return instance;
	}

	//Deserializer (pure path)
	public static T Deserialize<T>(string path)
	{
		T instance;

		using (FileStream stream = File.OpenRead(path))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			instance = (T)serializer.Deserialize(stream);
		}
		return instance;
	}
}