using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using Parse;
public class script_menu_manager : MonoBehaviour {
	
	public GameObject btn_main_new;
	public GameObject btn_main_continue;
	public GameObject btn_main_load;
	public GameObject btn_main_options;
	public GameObject btn_main_quit;
	public GameObject btn_main_refresh;
	public GameObject btn_main_achievement;
	public GameObject btn_main_report;
	public GameObject btn_main_news;
	public GameObject btn_main_developer;
	
	public GameObject grp_news;
	public GameObject grp_dev;
	public GameObject grp_report;
	public GameObject grp_achievement;
	public GameObject grp_newgame;
	public GameObject grp_continue;
	public GameObject grp_load;
	public GameObject grp_options;

	public Text txt_main_announcement_component;
	public Text txt_main_title;
	public string announcement;
	public bool announcement_loading;
	public bool announcement_updated;

	public string displayContent;
	public string prevDisplayContent;

	void Start () {
		btn_main_new = GameObject.Find ("btn_new_game");
		btn_main_continue = GameObject.Find ("btn_continue");
		btn_main_load = GameObject.Find ("btn_load_game");
		btn_main_options = GameObject.Find ("btn_options");
		btn_main_quit = GameObject.Find ("btn_quit");
		btn_main_news = GameObject.Find ("btn_news");
		btn_main_refresh = GameObject.Find ("btn_refresh");
		btn_main_refresh = GameObject.Find ("btn_achievement");
		btn_main_refresh = GameObject.Find ("btn_report");
		btn_main_developer = GameObject.Find ("btn_dev");

		grp_news = GameObject.Find ("grp_news");
		grp_dev = GameObject.Find ("grp_dev");
		grp_report = GameObject.Find ("grp_report");
		grp_achievement = GameObject.Find ("grp_achievement");
		grp_newgame = GameObject.Find ("grp_newgame");
		grp_continue = GameObject.Find ("grp_continue");
		grp_load = GameObject.Find ("grp_load");
		grp_options = GameObject.Find ("grp_options");

		txt_main_announcement_component = GameObject.Find ("txt_content").GetComponent<Text>();

		txt_main_title = GameObject.Find ("txt_title").GetComponent<Text>();

		announcement = "";
		announcement_loading = true;
		announcement_updated = false;

		displayContent = "news";
		prevDisplayContent = displayContent;

		refreshPanelContent();


	}

	void Update () {
		clearContent();
		switch(displayContent){
		default:
			break;
		case "news":
			if(announcement_updated == false){
				if(announcement_loading == true){
					txt_main_announcement_component.text = "Loading...";
				} else {
					txt_main_announcement_component.text = announcement;
					announcement_updated = true;
				}
			}
			break;
		case "developer":
			GameObject pan_main_content = GameObject.Find ("pan_main");

			break;
		}
	}


	public void onClick_main_new(){
		Debug.Log("Button Pressed : Main - New Game");
		txt_main_title.text = "New Game";
		displayContent = "newgame";
		refreshPanelContent();
	}

	public void onClick_main_continue(){
		Debug.Log("Button Pressed : Main - Continue");
		txt_main_title.text = "Continue";
		displayContent = "continuegame";
		refreshPanelContent();
	}

	public void onClick_main_load(){
		Debug.Log("Button Pressed : Main - Load");
		txt_main_title.text = "Load Game";
		displayContent = "loadgame";
		refreshPanelContent();
	}

	public void onClick_main_options(){
		Debug.Log("Button Pressed : Main - Options");
		txt_main_title.text = "Options";
		displayContent = "options";
		refreshPanelContent();
	}

	public void onClick_main_quit(){
		Debug.Log("Button Pressed : Main - Quit");
		Application.Quit();
	}

	public void onClick_main_news(){
		Debug.Log("Button Pressed : Main - News");
		txt_main_title.text = "News";
		displayContent = "news";
		refreshPanelContent();
	}

	public void onClick_main_refresh(){
		Debug.Log("Button Pressed : Main - Refresh");
		refreshPanelContent();
	}

	public void onClick_main_achivement(){
		Debug.Log("Button Pressed : Main - Achievement");
		txt_main_title.text = "Achievements";
		displayContent = "achievement";
		refreshPanelContent();
	}

	public void onClick_main_report(){
		Debug.Log("Button Pressed : Main - Report");
		txt_main_title.text = "Report Bugs/Issues";
		displayContent = "report";
		refreshPanelContent();
	}

	public void onClick_main_dev(){
		Debug.Log("Button Pressed: Main - Dev");
		txt_main_title.text = "Developer Tools";
		displayContent = "developer";
		refreshPanelContent();
	}

	
	void clearContent(){
		if (displayContent.Equals (prevDisplayContent)){
			//do nothing
		} else {
			txt_main_announcement_component.text = "";
			displayContent = prevDisplayContent;
		}
	}

	void toggleGroupEnabled(){
		grp_news = GameObject.Find ("grp_news");
		grp_dev = GameObject.Find ("grp_dev");
		grp_report = GameObject.Find ("grp_report");
		grp_achievement = GameObject.Find ("grp_achievement");
		grp_newgame = GameObject.Find ("grp_newgame");
		grp_continue = GameObject.Find ("grp_continue");
		grp_load = GameObject.Find ("grp_load");
		grp_options = GameObject.Find ("grp_options");
	}

	void refreshPanelContent(){
		announcement_loading = true;
		announcement_updated = false;

		ParseObject result;


		switch(displayContent){
		default:
			break;
		case "news":
			Debug.Log("Downloading news data from parse...");

			ParseQuery<ParseObject> query = ParseObject.GetQuery("News");
			query.GetAsync("tCWofrnBWD").ContinueWith(t =>
			                                          {
				result = t.Result;
				announcement = result.Get<string>("content");
				Debug.Log("News updated");
				announcement_loading = false;
			});
			break;
		}

	}



}
