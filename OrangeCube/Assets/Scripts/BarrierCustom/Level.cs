using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;
namespace BarrierCustom
{
    public class Level : MonoBehaviour
    {
        public int current_level;
        public LevelStatus levelstatus;
        public int current_best;
        private Image ui_img_sprite;
        private Image ui_img_lock;
        private GameObject obj_warning;
        private BarrierAudio BA;
        void Start()
        {
            ui_img_sprite = this.GetComponent<Image>();
         //   levelstatus = (LevelStatus)3;
            ui_img_sprite.sprite = Resources.Load("barrier/" + levelstatus.ToString(), typeof(Sprite)) as Sprite;
            ui_img_lock = transform.Find("lock").GetComponent<Image>();
           
            obj_warning = GameObject.Find("Canvas/warning");
            string result = System.Text.RegularExpressions.Regex.Replace(name, @"[^0-9]+", "");
            current_level = int.Parse(result);
            if (levelstatus > 0|| current_level <= (GlobalValue.i_player_max_level+1))
            {
                ui_img_lock.fillAmount = 0;
            }
            BA = GameObject.Find("BarrierAudio").GetComponent<BarrierAudio>();
            if(current_best<0)
            {
                current_best = 0;
            }
        }
       
        public void OnClick()
        {
            if (ui_img_lock.fillAmount == 0)
            {
                BA.PlayBtnClickBgm();
                obj_warning.transform.localPosition = new Vector2(0, 0);
                if(GlobalValue.i_want_level_best<0)
                {
                    GlobalValue.i_want_level_best = 0;
                }
                GlobalValue.i_want_level_best = current_best;
                obj_warning.GetComponent<Warning>().IniWarningInfo(GetMission(current_level), current_level);
            }
           
            
        }
        private string GetMission(int level)
        {
            string info;
            info = (Mathf.Pow(level,2)*5).ToString();
            return info;
        }
    }
}
