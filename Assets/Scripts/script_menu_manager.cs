using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
using System;

public class script_menu_manager : MonoBehaviour {

	//BUTTONS
	public GameObject btn_main_new;
	public GameObject btn_main_continue;
	public GameObject btn_main_load;
	public GameObject btn_main_options;
	public GameObject btn_main_quit;
	public GameObject btn_main_refresh;
	public GameObject btn_main_achievement;
	public GameObject btn_main_report;
	public GameObject btn_main_news;

	//ID FOR EACH PANEL DISPLAYS, for identification purpose
	public const string CONST_ID_NEWGAME = "newgame";
	public const string CONST_ID_CONTINUE = "continue";
	public const string CONST_ID_LOAD = "load";
	public const string CONST_ID_OPTIONS = "options";
	public const string CONST_ID_NEWS = "news";
	public const string CONST_ID_ACHIEVEMENT = "achievements";
	public const string CONST_ID_REPORT = "report";

	//PANEL DISPLAY GROUPS
	public GameObject grp_news;
	public GameObject grp_report;
	public GameObject grp_achievement;
	public GameObject grp_newgame;
	public GameObject grp_continue;
	public GameObject grp_load;
	public GameObject grp_options;
	public Dictionary<string, GameObject> GROUP = new Dictionary<string, GameObject>();

	//Panel Title
	public Text txt_main_title;

	//News Status Toggles
	public bool news_loading;
	public bool news_updated;
	public bool news_forceupdate;

	//News Template Reference
	
	public GameObject[] grps = new GameObject[10];
	public Text[] titles = new Text[10];
	public Text[] details = new Text[10];
	public Text[] contents = new Text[10];

	// News Text Reference
	public IEnumerable<ParseObject> news_from_parse;
	public Text txt_news_loading;

	public string display_currentGroup;
	public string display_prevGroup;

	void Start () {
		GROUP[CONST_ID_NEWS] = grp_news;
		GROUP[CONST_ID_REPORT] = grp_report;
		GROUP[CONST_ID_ACHIEVEMENT] = grp_achievement;
		GROUP[CONST_ID_NEWGAME] = grp_newgame;
		GROUP[CONST_ID_CONTINUE] = grp_continue;
		GROUP[CONST_ID_LOAD] = grp_load;
		GROUP[CONST_ID_OPTIONS] = grp_options;

		news_loading = true;
		news_updated = false;
		news_forceupdate = false;

		news_from_parse = null;

		//Display News on start up
		display_currentGroup = CONST_ID_NEWS;
		display_prevGroup = display_currentGroup;
		updateNews();

	}

	void Update () {
		switch (display_currentGroup) {

			default:
				break;
			case CONST_ID_REPORT:
				break;
			case CONST_ID_ACHIEVEMENT:
				break;
			case CONST_ID_NEWGAME:
				break;
			case CONST_ID_CONTINUE:
				break;
			case CONST_ID_LOAD:
				break;
			case CONST_ID_OPTIONS:
				break;
			case CONST_ID_NEWS:

				if(!btn_main_refresh.GetComponent<Button> ().enabled) {
					btn_main_refresh.GetComponent<Button> ().enabled = true;
				}

				if (news_updated == false) {
					if (news_loading == true) {
						txt_news_loading.enabled = true;
					} else {
						txt_news_loading.enabled = false;
						
						string news_title;
						string news_author;
						string news_time;
						string news_content;
					
						int counter = 1;

						foreach (var news_item in news_from_parse){
					
							news_title 		= news_item.Get<string>("title");
							news_author 	= news_item.Get<string>("author");
							news_time		= news_item.CreatedAt.ToString();
							news_content 	= news_item.Get<string>("content");

							titles[counter-1].text = news_title;
							details[counter-1].text = "Published By: " + news_author + "\nPublished On: " + news_time;
							contents[counter-1].text = news_content + "\n\n";
							counter ++;
						}

					for(int i = 0; i < 10; i ++){
						if(titles[i].text.Equals ("")){
							grps[i].SetActive (false);
						} else {
							grps[i].SetActive (true);
						}
					}


						Debug.Log("news updated.\n");
						news_updated = true;
					}
				}
				break;
		}
	}

	public void onClick_main_new() {
		
		Debug.Log("Button Pressed : Main - New Game\n");
		txt_main_title.text = "New Game";
		display_currentGroup = CONST_ID_NEWGAME;
		toggleGroups();
		
	}

	public void onClick_main_continue() {
		
		Debug.Log("Button Pressed : Main - Continue\n");
		txt_main_title.text = "Continue";
		display_currentGroup = CONST_ID_CONTINUE;
		toggleGroups();
		
	}

	public void onClick_main_load() {
		
		Debug.Log("Button Pressed : Main - Load\n");
		txt_main_title.text = "Load Game";
		display_currentGroup = CONST_ID_LOAD;
		toggleGroups();
		
	}

	public void onClick_main_options() {
		
		Debug.Log("Button Pressed : Main - Options\n");
		txt_main_title.text = "Options";
		display_currentGroup = CONST_ID_OPTIONS;
		toggleGroups();
		
	}

	public void onClick_main_news() {
		
		Debug.Log("Button Pressed : Main - News\n");
		txt_main_title.text = "News";
		display_currentGroup = CONST_ID_NEWS;
		toggleGroups();
		updateNews();
		
	}

	public void onClick_main_achievement() {
		
		Debug.Log("Button Pressed : Main - Achievement\n");
		txt_main_title.text = "Achievements";
		display_currentGroup = CONST_ID_ACHIEVEMENT;
		toggleGroups();
		
	}

	public void onClick_main_report() {
		
		Debug.Log("Button Pressed : Main - Report\n");
		txt_main_title.text = "Report Bugs/Issues";
		display_currentGroup = CONST_ID_REPORT;
		toggleGroups();
		
	}

	public void onClick_main_refresh() {
		
		Debug.Log("Button Pressed : Main - Refresh\n");
		btn_main_refresh.GetComponent<Button>().enabled = false;
		news_forceupdate = true;
		updateNews();
		news_forceupdate = false;
		btn_main_refresh.GetComponent<Button>().enabled = true;
		
	}

	public void onClick_main_quit() {
		
		Debug.Log("Button Pressed : Main - Quit\n");
		
		Application.Quit();
	}

	void toggleGroups() {
		
		GROUP[display_prevGroup].SetActive(false);
		GROUP[display_currentGroup].SetActive(true);
		display_prevGroup = display_currentGroup;
		
	}

	void updateNews() {
		if (news_from_parse!=null && !news_forceupdate){
			Debug.Log("News is already loaded, no force update\n");
			return;
		}

		for(int i = 0; i < 10; i ++){
			grps[i].SetActive (false);
			titles[i].text = "";
		}
			
		Debug.Log("downloading news...\n");
		news_loading = true;
		news_updated = false;

		var query = ParseObject.GetQuery("News")
			.OrderByDescending("createdAt")
			.Limit (10);
		query.FindAsync().ContinueWith(t => {
			news_from_parse = t.Result;
			news_loading = false;
			Debug.Log ("news downloaded.\n");
		});
		news_forceupdate = false;
	}
	
}
