using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//ʹ�õ���ģʽ�������̳е������࣬ʹ���ڳ����л�ʱ������
//ͬʱ�����������ù��ܺͲ�����Ч����
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioMixer audioMixer;

/*    //�����жϾ������
    private bool IsMuteMaster = false;
    private bool IsMuteMusic = false;
    private bool IsMuteSound = false;
    //���ڴ��澲��ǰ������
    private float LastMaster;
    private float LastMusic;
    private float LastSound;*/

    //��������
    //Slider on click��������������������ȡ������
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

/*    //Button on click����������ȡ���������ص���������δ������������������
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
