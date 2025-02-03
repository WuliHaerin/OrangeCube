using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;

namespace LevelCustom
{
    public class LevelManager : MonoBehaviour
    {
        private GameObject root;//UI对象的根对象即canvas
        private Image ui_img_pausepanel;//暂停面板
        private Image ui_img_complete;//通关信息面板
        private Image ui_img_failed;//未通关信息面板
        private Vector3 v_origin_point;//中心点，方便设置所有面板坐标
        private Vector3 v_ini_pause_pos;//暂停面板初始坐标
        private LevelAudio LA;//背景音控制对象
        void Start()
        {
            root = GameObject.Find("Canvas");
            ui_img_pausepanel = root.transform.Find("PausePanel").GetComponent<Image>();
            ui_img_complete = root.transform.Find("ComplatedPanel").GetComponent<Image>();
            ui_img_failed = root.transform.Find("FailedPanel").GetComponent<Image>();
            v_ini_pause_pos = ui_img_pausepanel.rectTransform.localPosition;
            GlobalValue.gamestatus = GameStatus.playing;//设置游戏状态
            v_origin_point =new Vector3(0,0,0);
            LA = GameObject.Find("LevelAudio").GetComponent<LevelAudio>();
        }
        void FixedUpdate()
        {
            //若是游戏通关或是游戏失败
            if (GlobalValue.gamestatus == GameStatus.failed|| GlobalValue.gamestatus == GameStatus.completed)
            {
                GameOver(GlobalValue.gamestatus);
                Time.timeScale = 0;
            }
        }
        //游戏结束
        private void GameOver(GameStatus gstatus)
        {
            
            switch (gstatus)
            {
                case GameStatus.completed:
                    LA.Playbgm(3);
                    ui_img_complete.GetComponent<MissionComplated>().SetCompletedInfo(GlobalValue.i_want_level_best,GlobalValue.i_current_time, GlobalValue.i_current_grade);
                    ui_img_complete.transform.localPosition = v_origin_point;
                    Time.timeScale = 0;
                    break;
                case GameStatus.failed:
                    LA.Playbgm(4);
                    ui_img_failed.GetComponent<MisssionFailed>().SetFailedInfo((Mathf.Pow(GlobalValue.i_want_level,2)*5).ToString());
                    ui_img_failed.transform.localPosition = v_origin_point;
                    Time.timeScale = 0;
                    break;
                default:
                    break;
            }

            AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
    () => {
        Debug.Log("--插屏广告完成--");

    },
    (it, str) => {
        Debug.LogError("Error->" + str);
    });
        }
        //游戏暂停
        public void PauseClick()
        {
            LA.Playbgm(2);
            if (GlobalValue.gamestatus != GameStatus.pause)
            {
                GlobalValue.gamestatus = GameStatus.pause;
                Time.timeScale = 0;
                ui_img_pausepanel.transform.localPosition = v_origin_point;
            }
        }
        //游戏继续
        public void Resume()
        {
            LA.Playbgm(2);
            GlobalValue.gamestatus = GameStatus.playing;
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            Time.timeScale = 1;
        }
        //重新开始
        public void Restart()
        {
            LA.Playbgm(2);
            GlobalValue.s_next_sceneName = "Level";
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            SceneManager.LoadScene("Load");
        }
        //回到主菜单
        public void Menu()
        {
            LA.Playbgm(2);
            GlobalValue.s_next_sceneName = "Barrier";
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            SceneManager.LoadScene("Load");
        }
        //结束当前关卡
        public void Exit()
        {
          //  LA.Playbgm(2);
            ui_img_pausepanel.transform.localPosition = v_ini_pause_pos;
            GlobalValue.gamestatus = GameStatus.failed;
            ui_img_failed.transform.localPosition = v_origin_point;
           GameOver(GlobalValue.gamestatus);
        }
    }

}
