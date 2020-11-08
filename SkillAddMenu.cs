using UnityEngine;
using UnityEngine.UI;
public class SkillAddMenu : MonoBehaviour
{
    //Menude kullanilan objelerin tanimlanmalari.
    [SerializeField] InputField skillName;
    [SerializeField] int iconId;
    [SerializeField] GameObject iconMenu;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    //Icon secme menusunun acilmasi
    public void OpenIconMenu() 
    {
        iconMenu.SetActive(!iconMenu.active);
    }
    //Icon secme menusunun kapanmasi
    public void CloseIconMenu() 
    {
        iconMenu.SetActive(false);
    }
    //Iconun set edilmesi
    public void SetIcon(int iconID) 
    {
        iconId = iconID;
        CloseIconMenu();
    }
    //Create butonuna basildiginda gerceklestirilen fonksiyon.
    public void Create() 
    {
        if (skillName.text.Length > 0&& iconId > 0) 
        {
            //Yeni bir SkillC nesnesi olusturur.
            SkillC newSkill = new SkillC(skillName.text.ToString());
            //Olusturulan nesnenin ikon atamasini yapar.
            newSkill.sIcon = Resources.Load<Sprite>(iconId.ToString());
            //Olusturulan nesneyi yetenek kontainarina ekler.
            FindObjectOfType<SkillContainer>().AddSkillToContainer(newSkill);
            Debug.Log("Skill Object Created Skill Name: "+skillName.text.ToString());
            //Menuyu kapatir.
            gameObject.SetActive(false);
        }
    }
    //Back butonuna basildiginda gerceklestirilen fonksiyon.
    public void Back() 
    {
        //Yetenek ekleme menusu kapatir.
        gameObject.SetActive(false);
    }
}
