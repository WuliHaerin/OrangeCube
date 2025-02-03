using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalCustom;
namespace BarrierCustom
{
    public class BarrierAudio : MonoBehaviour
    {
        private AudioSource BtnClick;//按钮点击声
        private AudioSource BarrierBGM;//场景背景音乐
        // Use this for initialization
        void Start()
        {
            BarrierBGM = transform.Find("BarrierBGM").GetComponent<AudioSource>();
            BtnClick = transform.Find("BtnClick").GetComponent<AudioSource>();
            BarrierBGM.volume = GlobalValue.f_soundvalue;
            BtnClick.volume = GlobalValue.f_soundvalue;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PlayBtnClickBgm()
        {
            BtnClick.Play();
        }

    }
}
