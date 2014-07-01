using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SelectManager : MonoBehaviour {
	private UISprite uiSpriteCharaLeft;	// hidari
	private UISprite uiSpriteCharaRight; // migi
	private UILabel charaNameLabel;	//
	private UILabel charaWordLabel;	//
	private Color sayColor = Color.gray;
	private Color notSayColor = Color.white;

	private void Awake(){
		uiSpriteCharaLeft = GameObject.Find(PathHelper.selectCharaLeftSpritePath).GetComponent<UISprite>();
		uiSpriteCharaRight = GameObject.Find(PathHelper.selectCharaRightSpritePath).GetComponent<UISprite>();
		charaNameLabel = GameObject.Find(PathHelper.selectNameLabelPath).GetComponent<UILabel>();
		charaWordLabel = GameObject.Find(PathHelper.selectWordLabelPath).GetComponent<UILabel>();
		Debug.Log(string.Format("texture left -> {0}, texture right -> {1}, chara name -> {2}, chara word -> {3}",
		                        uiSpriteCharaLeft, uiSpriteCharaRight, charaNameLabel, charaWordLabel));
	}


	private int testIndex = 0;
	private void Start(){
		List<string> wordList = WordListElement.FindEventCommunicationList("story00.txt");
		StartEventUI(wordList, ref testIndex);
		Debug.Log("testIndex  "+testIndex);
	}

	
	private void StartEventUI(List<string> targetList, ref int targetIndex){

		for(int i=targetIndex; i<targetList.Count; i++){
			string[] fruit = targetList[i].Split(',');
			string eventType = fruit[0];

			if(eventType == "say"){return;}

			switch(eventType){
			case "start":
				SetStartEvent();
				break;
			case "ui":
				SetUI(fruit);
				break;
			case "bgm":
				SetBgm(fruit[1]);
				break;
			case "end":
				SetEventEnd();
				break;
			}

			targetIndex++;
		}
	}

	//start
	private void SetStartEvent(){
		Debug.Log("<< funtion start >>");
		uiSpriteCharaRight.color = notSayColor;
		uiSpriteCharaLeft.color = notSayColor;
	}

	//ui
	private void SetUI(string[] tartgetFruit){
		Debug.Log("<< funtion ui >>");
		switch(tartgetFruit[1]){
		case "left":
			uiSpriteCharaLeft.spriteName = FindSprite(tartgetFruit[2]);
			break;
		case "right":
			uiSpriteCharaRight.spriteName = FindSprite(tartgetFruit[2]);
			break;
		}
	}

	private string FindSprite(string spriteName){
		if(spriteName =="unity"){
			return "Photo_Unitychan";
		}else if(spriteName =="ser"){
			return "Photo_Sergeant";
		}else if(spriteName =="pres"){
			return "Photo_President";
		}
		return "";
	}

	//say
	private void FindSay(string[] targetFruit){
		charaNameLabel.text = FindCharaName(targetFruit[1]);
		charaWordLabel.text = targetFruit[2];
	}

	private string FindCharaName(string targetName){
		if(targetName == "unity"){
			return "ユニティちゃん";
		}else if(targetName == "ser"){
			return "軍曹";
		}else if(targetName == "pres"){
			return "大統領";
		}

		return "";
	}

	//bgm
	private void SetBgm(string bgmName){
		Debug.Log("set bgm ---->"+bgmName);
	}
	

	//end
	private void SetEventEnd(){
		Debug.Log("set end");
		Destroy(this.gameObject);
	}
}