using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;
using TMPro;

namespace LevelCustom
{
    public class TimeManager : MonoBehaviour
    {
        private TMP_Text ui_text_current_level;//显示当前关卡的文本
        private Text ui_text_time;//显示通过当前关卡剩余时间
        private Image ui_img_slider;//下方评分进度条
        private Image[] a_img_grade = new Image[3];//下方表示评分的三个精灵图片
        private Sprite res_spr_bird_blue;//获取到表示获得评分的标志--蓝色小鸟
        private Sprite res_spr_bird_grey;//获取到表示失去评分的标志--灰色小鸟
        private Level lv;//当前关卡对象
        private int i_left_time;//剩余时间
        private float missionTime;

        public LevelStatus grade;//当前关卡的评分
        // start方法中获取到相应组件并初始化一些信息
        void Start()
        {
            ui_text_current_level = transform.Find("Level").GetComponent<TMP_Text>();
            ui_text_time = transform.Find("Time").GetComponent<Text>();
            ui_img_slider = transform.Find("GradeSlider/Image").GetComponent<Image>();
            //Resources.Load方法获取资源
            res_spr_bird_blue = Resources.Load("Level/twitter",typeof(Sprite)) as Sprite;
            res_spr_bird_grey = Resources.Load("Start/songbird", typeof(Sprite)) as Sprite;
            for (int gradeindex = 1; gradeindex <= a_img_grade.Length; gradeindex++)
            {
                a_img_grade[gradeindex - 1] = transform.Find("Grade/grade"+gradeindex.ToString()).GetComponent<Image>();
            }
            lv = transform.GetComponent<IniGame>().lv;
            ui_text_current_level.text ="关卡 "+ lv.I_level.ToString();
            //初始游戏为3级评分
            grade = LevelStatus.grade3;
            GlobalValue.i_current_grade = 3;
            missionTime = lv.I_mission;
            StartCoroutine(CountDown());
        }

        // 通过Time.timeSinceLevelLoad获取到进入当期场景已耗费时间
        //之所以将时间的更新放在FixedUpdate中是为了之后使用timescale来实现游戏暂停
        void FixedUpdate()
        {
            //判断剩余时间是否大于0，进行游戏时间以及下方游戏评分的更新
            if (missionTime >= 0 )
            {
                ui_text_time.text = missionTime.ToString();
                GlobalValue.i_current_time = (int)Time.timeSinceLevelLoad;
                ui_img_slider.fillAmount = 1 - Time.timeSinceLevelLoad / (float)lv.I_mission;
                if (GlobalValue.i_current_grade != GradeControl(ui_img_slider.fillAmount))
                {
                    GlobalValue.i_current_grade = GradeControl(ui_img_slider.fillAmount);
                    
                    for (int temp=0;temp< GlobalValue.i_current_grade; temp++)
                    {
                        a_img_grade[temp].sprite = res_spr_bird_blue;
                    }
                    for (int temp= GlobalValue.i_current_grade; temp<3;temp++)
                    {
                        a_img_grade[temp].sprite = res_spr_bird_grey;
                    }
                }
            }
            else 
            {
                if(!isCancelAd)
                {
                    SetAdPanel(true);
                    StartCoroutine("Die");
                }
                else
                {
                    //若是时间消耗完，则判定当前关卡失败
                    //同时下方评分归零
                    GlobalValue.gamestatus = GameStatus.failed;
                    for (int temp = 0; temp < 3; temp++)
                    {
                        a_img_grade[temp].sprite = res_spr_bird_grey;
                    }
                }
            }
           
        }

        public IEnumerator CountDown()
        {
            for(; ; )
            {
                missionTime--;
                yield return new WaitForSeconds(1f);
            }          
        }

        public GameObject AdPanel;
        public bool isCancelAd;
        public void SetAdPanel(bool a)
        {
            AdPanel.SetActive(a);
            Time.timeScale = a ? 0 : 1;
        }

        public void CancelAd()
        {
            isCancelAd = true;
            SetAdPanel(false);
        }

        public IEnumerator Die()
        {
            yield return new WaitForSeconds(0.2f);
//若是时间消耗完，则判定当前关卡失败
                    //同时下方评分归零
                    GlobalValue.gamestatus = GameStatus.failed;
                    for (int temp = 0; temp < 3; temp++)
                    {
                        a_img_grade[temp].sprite = res_spr_bird_grey;
                    }
        }

        public void Revive()
        {
            AdManager.ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    missionTime = 10;
                    StopCoroutine("Die");
                    SetAdPanel(false);

                    AdManager.clickid = "";
                    AdManager.getClickid();
                    AdManager.apiSend("game_addiction", AdManager.clickid);
                    AdManager.apiSend("lt_roi", AdManager.clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
        }

        //根据进度条的值来判断游戏评分
        //消耗时间大于50%为3级评分（最高三级）
        //消耗时间在20%-50%之间为2级评分
        //消耗时间在20%之下则为1级评分
        int GradeControl(float slider_value)
        {
            int i_grade=3;
            int slidervalue = (int)(slider_value * 10);
      
            switch (slidervalue)
            {
                case 0:
                case 1:
                    i_grade = 1;
                    break;
                case 2:
                case 3:
                case 4:
                    i_grade = 2;
                    break;
                default:
                    i_grade = 3;
                    break;
            }
            return i_grade;
        }
    }
}

