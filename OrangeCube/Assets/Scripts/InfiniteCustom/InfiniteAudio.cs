using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalCustom;
namespace InfiniteCustom
{
    public class InfiniteAudio : MonoBehaviour
    {
        //当前场景的所有音源
        private AudioSource[] infinitebgm = new AudioSource[4];
        // 获取到相应音源
        void Start()
        {
            infinitebgm[0] = transform.Find("InfiniteBGM").GetComponent<AudioSource>();
            infinitebgm[1] = transform.Find("cubeClickbgm").GetComponent<AudioSource>();
            infinitebgm[2] = transform.Find("btnClickbgm").GetComponent<AudioSource>();
            infinitebgm[3] = transform.Find("missionbgm").GetComponent<AudioSource>();
            for (int temp = 0; temp < infinitebgm.Length; temp++)
            {
                infinitebgm[temp].volume = GlobalValue.f_soundvalue;
            }
        }
        //根据索引值播放相应音乐
        public void Playbgm(int bgmindex)
        {
            infinitebgm[bgmindex].Play();
        }
    }

}
