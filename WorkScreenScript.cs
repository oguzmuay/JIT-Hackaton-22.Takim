using UnityEngine;
using UnityEngine.UI;
public class WorkScreenScript : MonoBehaviour
{
    //Kullanilan degiskenlerin tanimlanmasi.
    public float hour,minute,second;
    public Text timeText;
    void Start()
    {
        
    }
    void Update()
    {
        //Calisiyor mu diye kontrol.
        if (FindObjectOfType<Character>().work) 
        {
            //second degiskenini frame odakli degilde gercek zamana gore arttirma.
            second += Time.deltaTime;
            //Dakika degiskenini arttirma.
            if (second >= 60)
            {
                second = 0;
                minute++;
            }
            //Saat degiskenini arttirma.
            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }
            //Altta cikan kronometrenin icerisindeki yazinin ayarlanmasi.
            timeText.text = (int)hour + ":" + (int)minute + ":" + (int)second;
        }
        else
        {
            //Butun degiskenleri sifirlama.
            second = 0;
            minute = 0;
            hour = 0;
            timeText.text = "";
        }

    }
    public void FinishButton() 
    {
        FindObjectOfType<SkillContainer>().FinishWork(hour,minute,FindObjectOfType<Character>().currentWorkId);
        FindObjectOfType<Character>().work = false;
        hour = 0;
        minute = 0;
        second = 0;
    }
}
