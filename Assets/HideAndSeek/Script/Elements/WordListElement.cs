using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WordListElement : MonoBehaviour {

	public static List<string> FindEventWordList(string fileName){
		List<string> wordList = new List<string>();
		FileInfo fileInfo = new FileInfo(Application.streamingAssetsPath+"/Texts/"+fileName);
		StreamReader streamReader = new StreamReader(fileInfo.FullName);

		while(streamReader.Peek() != -1){
			string getSongInfo = streamReader.ReadLine();
			Debug.Log("get SongInfo ---> "+getSongInfo);
			wordList.Add(getSongInfo);
		}

		return wordList;
	}


}
