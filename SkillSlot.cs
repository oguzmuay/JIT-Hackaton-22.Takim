using UnityEngine;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    //Hangi yetenegin oldugu belirten id.
    public int id;
    //Rahat kullanim icin skill objesi.
    public SkillC skill;
    //Slotta bulunan objeler.
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text skillTime;
    [SerializeField] Text skillLevel;
    [SerializeField] RectTransform skillBar;
    void Start()
    {
        // Slotlar her yatarildiginda scale degiskenleri 1.7 olarak degisiyodu bunu duzeltmek icin.
        GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void Update()
    {
        //Skill objesini atama.
        skill = FindObjectOfType<SkillContainer>().skillContainer[id];
        //Objelerin icerisindeki textlerin ayarlanmasi.
        skillName.text = skill.sName;
        skillIcon.sprite = skill.sIcon;
        skillTime.text = skill.sHour + " Saat :" + skill.sMinute+" Dakika";
        skillLevel.text = skill.sLevel.ToString();
        //Seviye bari her level atlandiginda sifirlanmasi icin kucukbir matemariksel islem.
        float currentTime = 0;
        if (skill.sLevel > 0) 
        {
            currentTime = (skill.sHour - FindObjectOfType<SkillContainer>().neededTimes[skill.sLevel]);
        }
        else
        {
            currentTime = skill.sHour;
        }
        //Bir resim objesinin uzunlugunu degistirerek seviye bari yaprim
        skillBar.sizeDelta = new Vector2(1235* (currentTime / FindObjectOfType<SkillContainer>().neededTimes[skill.sLevel+1]),skillBar.sizeDelta.y);
        //Yukarda bassettigim hatanin devamli olmamasi icin onlem.
        GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
