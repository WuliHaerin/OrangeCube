using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;
namespace BarrierCustom
{
    public class Warning : MonoBehaviour
    {
        private Text ui_text_warning;
        private Text ui_text_level;
        private int i_now_level;
        private BarrierAudio BA;
        // Use this for initialization
        void Start()
        {
            ui_text_warning = transform.Find("needTime").GetComponent<Text>();
            ui_text_level = transform.Find("level").GetComponent<Text>();
            BA = GameObject.Find("BarrierAudio").GetComponent<BarrierAudio>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void EnterClick()
        {
            BA.PlayBtnClickBgm();
            GlobalValue.i_want_level = i_now_level;
            GlobalValue.s_next_sceneName = "Level";
            SceneManager.LoadScene("Load");
        }
        public void CancelClick()
        {
            BA.PlayBtnClickBgm();
            transform.Translate(900,0,0);
        }
        public void IniWarningInfo(string info,int level)
        {
            this.ui_text_warning.text = info;
            this.ui_text_level.text = level.ToString();
            this.i_now_level = level;
        }
    }
}

