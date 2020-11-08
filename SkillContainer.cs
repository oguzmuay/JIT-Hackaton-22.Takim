using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    //Unvanlar
    public List<string> titles;
    //Her seviye icin gereken zamanlar
    public List<float> neededTimes;
    //kod icerisinde atanmamis degiskenler unity editorunun icerisinde atanip kullaniluyor yani unvanlar ve gereken saatler unity icersinde tanimlanan listeler.
    //SkillC clasi ile olusturulmus yetenek listesi.
    public List<SkillC> skillContainer;
    //Goruntu buglarini duzeltmek icin tanimladim.
    public GameObject skillContainerPanel;
    //Bir butona basildiginda acmak icin.
    public GameObject skillContainerParent;
    //Yetenek eklerken adini ve simgesi degistirmek icin kullanilan obje.
    [SerializeField] GameObject skillAddMenuObject;
    //Her yetenek olusturulda yaratilan obje.
    [SerializeField] GameObject skillSlotObject;
    void Start()
    {
        //Goruntu bugunu duzeltmek icin var. Nedense yetenek objelerini tasiyan nesnenin pozisyonu buglanip durmasi gerek yerde durmuyorudu.
        skillContainerPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-951.5f,0f);
        skillContainer = new List<SkillC>();
    }

    void Update()
    {
        //Her yetenegin unvanlarini atanilan yer.
        for (int i = 0; i < skillContainer.Count; i++)
        {
            skillContainer[i].sTitle = titles[skillContainer[i].sLevel];
        }
        //Scroll viewdeki goruntu alanini 11. nesneden sonra buyutmeye basliyorum.Bu sekilde 11. nesneden sonrada eklenilen nesneler kaydirilarak gosterile biliyor.
        skillContainerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(skillContainerPanel.GetComponent<RectTransform>().sizeDelta.x, 3000 + (skillContainer.Count > 11 ? (skillContainer.Count - 11) * 300 : 0));
        skillContainerParent.SetActive(FindObjectOfType<Character>().inSkillTree);
    }
    public void SkillTreeTrigger() 
    {
        //Karakterin calisma durumunda yeteneklerine bakilamiyor.Kullanicinin calismasinin bolunmesini engellemek icin.
        if (!FindObjectOfType<Character>().work)
            FindObjectOfType<Character>().inSkillTree = !FindObjectOfType<Character>().inSkillTree;
    }
    //Olusturulan yetenegin kontainara eklenip onun icin bir tane slot objesi olusturuluyor.
    public void AddSkillToContainer(SkillC skill) 
    {
        GameObject slot = Instantiate(skillSlotObject);
        slot.transform.SetParent(skillContainerPanel.transform);
        slot.GetComponent<SkillSlot>().id = skillContainer.Count;
        skillContainer.Add(skill);
    }
    //Yetenekleri acan butonun fonksiyon
    public void OpenSkillAddMenu() 
    {
        skillAddMenuObject.SetActive(!skillAddMenuObject.active);   
    }
    //Kullanicin calismasi her bittiginde calisilan sure onceki zamana ekleniyor.Dakika 60tan fazla ise saate cagirilip eklenir.Saat gerekli zamani gecerse seviye atlaniyor.
    public void FinishWork(float passedHour,float passedMinute,int id)
    {
        //Saat ve dakika artti
        skillContainer[id].sHour += passedHour;
        skillContainer[id].sMinute += passedMinute;
        if (skillContainer[id].sMinute >= 60) 
        {
            skillContainer[id].sHour += (skillContainer[id].sMinute % 60);
            skillContainer[id].sMinute -= (skillContainer[id].sMinute % 60) * 60;
        }
        if (skillContainer[id].sHour >= neededTimes[skillContainer[id].sLevel]) 
        {
            for (int i = 0; i < neededTimes.Count; i++)
            {
                if (skillContainer[id].sHour > neededTimes[i])
                    skillContainer[id].sLevel = i+1;
                else break;  
            }
        }
    }
}
