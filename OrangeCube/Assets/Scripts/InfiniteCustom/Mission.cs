using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;

public class Mission : MonoBehaviour {
    private Text ui_text_current;
    private Text ui_text_best;
    // Use this for initialization
    void Start () {
        ui_text_current = transform.Find("thistime").GetComponent<Text>();
        ui_text_best = transform.Find("best").GetComponent<Text>(); ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BackMenu()
    {
        GlobalValue.s_next_sceneName = "Start";
        SceneManager.LoadScene("Load");
    }
    public void SetMissionInfo(string cur,string best)
    {
        ui_text_current.text = cur;
        ui_text_best.text = best;
    }
}
