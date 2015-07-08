using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;

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
	Dictionary<string, GameObject> GROUP = new Dictionary<string, GameObject>();

	//Panel Title
	public Text txt_main_title;

	//News Status Toggles
	public bool news_loading;
	public bool news_updated;
	public bool news_forceupdate;

	//News Template Reference
	public GameObject template_item_group;
	public GameObject template_item_title;
	public GameObject template_item_post_detail;
	public GameObject template_item_post_content;

	// News Text Reference
	public IEnumerable<ParseObject> news_from_parse;

	// to be deleted
	public Text txt_main_news;
	public string news;

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
					txt_main_news.text = "Loading...";
				}				else {
					//TODO 
					txt_main_news.text = news;
					news_updated = true;
				}
			}


			break;
		}
	}

	public void onClick_main_new() {
		Debug.Log("Button Pressed : Main - New Game");
		txt_main_title.text = "New Game";
		display_currentGroup = CONST_ID_NEWGAME;
		toggleGroups();
	}

	public void onClick_main_continue() {
		Debug.Log("Button Pressed : Main - Continue");
		txt_main_title.text = "Continue";
		display_currentGroup = CONST_ID_CONTINUE;
		toggleGroups();
	}

	public void onClick_main_load() {
		Debug.Log("Button Pressed : Main - Load");
		txt_main_title.text = "Load Game";
		display_currentGroup = CONST_ID_LOAD;
		toggleGroups();
	}

	public void onClick_main_options() {
		Debug.Log("Button Pressed : Main - Options");
		txt_main_title.text = "Options";
		display_currentGroup = CONST_ID_OPTIONS;
		toggleGroups();
	}

	public void onClick_main_news() {
		Debug.Log("Button Pressed : Main - News");
		txt_main_title.text = "News";
		display_currentGroup = CONST_ID_NEWS;
		toggleGroups();
		updateNews();
	}

	public void onClick_main_achievement() {
		Debug.Log("Button Pressed : Main - Achievement");
		txt_main_title.text = "Achievements";
		display_currentGroup = CONST_ID_ACHIEVEMENT;
		toggleGroups();
	}

	public void onClick_main_report() {
		Debug.Log("Button Pressed : Main - Report");
		txt_main_title.text = "Report Bugs/Issues";
		display_currentGroup = CONST_ID_REPORT;
		toggleGroups();
	}

	public void onClick_main_refresh() {
		Debug.Log("Button Pressed : Main - Refresh");
		btn_main_refresh.GetComponent<Button>().enabled = false;
		news_forceupdate = true;
		updateNews();
		news_forceupdate = false;
		btn_main_refresh.GetComponent<Button>().enabled = true;
	}

	public void onClick_main_quit() {
		Debug.Log("Button Pressed : Main - Quit");
		Application.Quit();
	}

	void toggleGroups() {
		GROUP[display_prevGroup].SetActive(false);
		GROUP[display_currentGroup].SetActive(true);
		display_prevGroup = display_currentGroup;
	}

	void updateNews() {
		if (!news.Equals ("") && !news_forceupdate){
			return;
		}
			
		Debug.Log("Downloading news data from parse...");
		news_loading = true;
		news_updated = false;
		
		var query = ParseObject.GetQuery("News")
			.OrderByDescending("updatedAt")
				.Limit (10);
		query.FindAsync().ContinueWith(t => {
			IEnumerable<ParseObject> articles = t.Result;
			news = "";
			foreach (var article in articles){
				news += article.Get<string>("title") + "\n<hr>";
				news += article.Get<string>("content") + "\n<hr>";
			}
			Debug.Log("News updated");
			news_loading = false;
		});
		news_forceupdate = false;
		
	}
	
}
