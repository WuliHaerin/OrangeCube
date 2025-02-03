using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;

namespace LevelCustom
{
    //通过当前关卡
    public class MissionComplated : MonoBehaviour
    {
        private Sprite res_img_yellow_star;//获取的评分标志
        private Sprite res_img_blue_star;//失去的评分标志
        private Image[] a_img_grade = new Image[3];//评分标志
        private Text ui_text_current;//通过当前关卡的信息
        private Text ui_text_best;//当前关卡的最佳信息
        // 初始化信息
        void Start()
        {
            res_img_blue_star = Resources.Load("barrier/star", typeof(Sprite)) as Sprite;
            res_img_yellow_star = Resources.Load("Level/getstar", typeof(Sprite)) as Sprite;
            for(int gradeindex = 1; gradeindex <= a_img_grade.Length; gradeindex++)
            {
                a_img_grade[gradeindex-1] = transform.Find("Grade/grade"+gradeindex.ToString()).GetComponent<Image>();
            }
            ui_text_current = transform.Find("Gameinfo/current").GetComponent<Text>();
            ui_text_best = transform.Find("Gameinfo/best").GetComponent<Text>();
        }
        //回到主菜单
        public void BackMenu()
        {
            GlobalValue.s_next_sceneName = "Barrier";
            SceneManager.LoadScene("Load");
        }
        //设置通过信息包括最佳信息，当前信息，以及评分
        //通关及时保存最佳信息到本地
        public void SetCompletedInfo(int best,int current,int i_grade)
        {
            ui_text_best.text = best.ToString();
            ui_text_current.text = current.ToString();
            for (int temp = i_grade; temp < 3; temp++)
            {

                a_img_grade[temp].sprite = res_img_blue_star;
            }
            for (int temp = 0; temp < i_grade; temp++)
            {
                a_img_grade[temp].sprite = res_img_yellow_star;
            }
            PlayerPrefs.SetInt("playermaxlevel",GlobalValue.i_player_max_level);
            if (GlobalValue.i_current_grade> PlayerPrefs.GetInt("levelgrade" + GlobalValue.i_want_level.ToString()))
            {
                PlayerPrefs.SetInt("levelgrade" + GlobalValue.i_want_level.ToString(), GlobalValue.i_current_grade);
            }
            PlayerPrefs.SetInt("Level"+GlobalValue.i_want_level.ToString(), GlobalValue.i_want_level_best);
            if (PlayerPrefs.GetInt("Level" + GlobalValue.i_want_level.ToString()) < 0)
            {
                PlayerPrefs.SetInt("Level" + GlobalValue.i_want_level.ToString(), 0);
            }
        }
    }

}
