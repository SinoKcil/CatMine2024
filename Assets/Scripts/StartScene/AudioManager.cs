using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//使用单例模式开发，继承单例父类，使其在场景切换时不销毁
//同时包括音量设置功能和播放音效功能
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioMixer audioMixer;

/*    //用于判断静音与否
    private bool IsMuteMaster = false;
    private bool IsMuteMusic = false;
    private bool IsMuteSound = false;
    //用于储存静音前的音量
    private float LastMaster;
    private float LastMusic;
    private float LastSound;*/

    //音量设置
    //Slider on click：调节音量，若静音则取消静音
    public void MasterSldOnClick(GameObject image, Slider slider)
    {
        audioMixer.SetFloat("vMaster", slider.value);
/*        if (IsMuteMaster == false) return;
        else
        {
            image.SetActive(false);
            IsMuteMaster = false;
        }*/
    }
    public void MusicSldOnClick(GameObject image, Slider slider)
    {
        Debug.Log(slider.value);
        audioMixer.SetFloat("vMusic", slider.value);
/*        if (IsMuteMusic == false) return;
        else
        {
            image.SetActive(false);
            IsMuteMusic = false;
        }*/
    }
    public void SoundSldOnClick(GameObject image, Slider slider)
    {
        audioMixer.SetFloat("vSound", slider.value);
/*        if (IsMuteSound == false) return;
        else
        {
            image.SetActive(false);
            IsMuteSound = false;
        };*/
    }

/*    //Button on click：若静音则取消静音并回调音量；若未静音则静音并储存音量
    public void MasterBtnOnClick(GameObject image, Slider Master)
    {
        if (IsMuteMaster)
        {
            image.SetActive(false);
            IsMuteMaster = false;
            Master.value = LastMaster;
        }

        else
        {
            image.SetActive(true);
            LastMaster = Master.value;
            Master.value = Master.minValue;
            IsMuteMaster = true;
        }
    }
    public void SoundBtnOnClick(GameObject image, Slider Sound)
    {
        if (IsMuteSound)
        {
            image.SetActive(false);
            IsMuteSound = false;
            Sound.value = Instance.LastSound;
        }

        else
        {
            image.SetActive(true);
            LastSound = Sound.value;
            Sound.value = Sound.minValue;
            IsMuteSound = true;
        }
    }
    public void MusicBtnOnClick(GameObject image, Slider Music)
    {
        if (IsMuteMusic)
        {
            image.SetActive(false);
            IsMuteMusic = false;
            Music.value = LastMusic;
        }

        else
        {
            image.SetActive(true);
            LastMusic = Music.value;
            Music.value = Music.minValue;
            IsMuteMusic = true;
        }
    }
*/
}
