using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;

namespace LevelCustom
{
    //游戏闯关失败
    public class MisssionFailed : MonoBehaviour
    {
        private Text ui_text_failed_info;//闯关失败后的信息，为当前关卡的要求时间
        // Use this for initialization
        void Start()
        {
            ui_text_failed_info = transform.Find("Gameinfo").GetComponent<Text>();
        }
        //设置失败信息
        public void SetFailedInfo(string info)
        {
            ui_text_failed_info.text = info;
        }
        //回到主菜单
        public void BackMenu()
        {
            GlobalValue.s_next_sceneName = "Barrier";
            SceneManager.LoadScene("Load");
        }
    }

}
