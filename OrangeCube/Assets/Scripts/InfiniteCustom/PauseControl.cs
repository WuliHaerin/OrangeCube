using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;

namespace InfiniteCustom
{

    public class PauseControl : MonoBehaviour
    {
        private Vector3 v_origin_point;
        private Vector3 v_ini_pause_pos;
        private GameObject root;
        private Image ui_img_pausepanel;
        private InfiniteAudio IA;
        // Use this for initialization
        void Start()
        {
            root = GameObject.Find("Canvas");
            ui_img_pausepanel = root.transform.Find("PausePanel").GetComponent<Image>();
            v_ini_pause_pos = ui_img_pausepanel.rectTransform.localPosition;
            GlobalValue.gamestatus = GameStatus.playing;
            v_origin_point = new Vector3(0, 0, 0);
            IA = GameObject.Find("InfiniteAudio").GetComponent<InfiniteAudio>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PauseClick()
        {

            if (GlobalValue.gamestatus != GameStatus.pause)
            {
                IA.Playbgm(2);
                GlobalValue.gamestatus = GameStatus.pause;
                Time.timeScale = 0;
                ui_img_pausepanel.transform.localPosition = v_origin_point;
            }
        }
        //游戏继续
        public void Resume()
        {
            IA.Playbgm(2);
            GlobalValue.gamestatus = GameStatus.playing;
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            Time.timeScale = 1;
        }
        //重新开始
        public void Restart()
        {
            IA.Playbgm(2);
            GlobalValue.s_next_sceneName = "Infinite";
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            SceneManager.LoadScene("Load");
        }
        //回到主菜单
        public void Menu()
        {
            IA.Playbgm(2);
            GlobalValue.s_next_sceneName = "Barrier";
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            SceneManager.LoadScene("Load");
        }
        //退出游戏
        public void Exit()
        {
            IA.Playbgm(2);
            Time.timeScale = 0;
            PlayerPrefs.SetInt("infmaxlevel", GlobalValue.inflv.Max_level);
            PlayerPrefs.SetInt("infmintime", GlobalValue.inflv.Min_time);
            GameObject.Find("Canvas/GameArea").GetComponent<IniInfinite>().PauseMission();
            //Application.Quit();
        }
    }

}
