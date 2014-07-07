using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SelectManager : MonoBehaviour {
	private GameObject objectMaster;
	private UISprite uiSpriteCharaLeft;	// hidari
	private UISprite uiSpriteCharaRight; // migi
	private UILabel charaNameLabel;	//
	private UILabel charaWordLabel;	//
	private Color sayColor = Color.gray;
	private Color notSayColor = Color.white;
	private string targetStoryTextName;
	public string TargetStoryTextName{
		get{return targetStoryTextName;}
	}


	private void Awake(){
		objectMaster = GameObject.Find(PathHelper.selectMasterObjectMasterPath);
		uiSpriteCharaLeft = GameObject.Find(PathHelper.selectCharaLeftSpritePath).GetComponent<UISprite>();
		uiSpriteCharaRight = GameObject.Find(PathHelper.selectCharaRightSpritePath).GetComponent<UISprite>();
		charaNameLabel = GameObject.Find(PathHelper.selectNameLabelPath).GetComponent<UILabel>();
		charaWordLabel = GameObject.Find(PathHelper.selectWordLabelPath).GetComponent<UILabel>();

		objectMaster.SetActive(false);
		targetStoryTextName = "story00.text";
	}

	private void Start(){
		Time.timeScale = 0;
		OpenSelectWindow();
	}

	private void Update(){
		if(Input.GetMouseButtonDown(0)){
			OpenSelectWindow();
		}
	}

	private int wordIndex = 0;
	private void OpenSelectWindow(string textName = "storyDefo.txt"){
		List<string> wordList = WordListElement.FindEventWordList(textName);
		StartEventUI(wordList, ref wordIndex);
 		string[] wordData = wordList[wordIndex].Split(',');
		FindSay(wordData);
		wordIndex ++;
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
		BGMManager.FindSE(PathHelper.se000Path);
		objectMaster.SetActive(true);
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
	
	private void ChangeSayCharaUIColor(string sayCharaName){
		string ansPhotoName ="";
		switch(sayCharaName){
		case "ユニティちゃん":
			ansPhotoName = "Photo_Unitychan";
			break;
		case "軍曹":
			ansPhotoName = "Photo_Sergeant";
			break;
		case "大統領":
			ansPhotoName = "Photo_President";
			break;
		}

		Debug.Log("change color ---> "+ansPhotoName+"   "+uiSpriteCharaLeft.spriteName+"   "+uiSpriteCharaRight.spriteName);

		if(uiSpriteCharaRight.spriteName != ansPhotoName){
			//Debug.Log("function ----> Left");
			uiSpriteCharaRight.color = Color.gray;
			uiSpriteCharaLeft.color = Color.white;
		}else{
			//Debug.Log("function ----> right");
			uiSpriteCharaRight.color = Color.white;
			uiSpriteCharaLeft.color = Color.gray;
		}
	}

	//say
	private void FindSay(string[] targetFruit){
		BGMManager.FindSE(PathHelper.se001Path);
		charaNameLabel.text = FindCharaName(targetFruit[1]);
		charaWordLabel.text = targetFruit[2];
		ChangeSayCharaUIColor(charaNameLabel.text);
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
		objectMaster.SetActive(false);
		wordIndex = 0;
		Time.timeScale = 1f;
	}
}