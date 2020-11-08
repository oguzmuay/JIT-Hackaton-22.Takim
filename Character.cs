using UnityEngine;
using UnityEngine.AI;
public class Character : MonoBehaviour
{
    //Tanimlamalar


    [SerializeField] NavMeshAgent charAgent;
    //Objects
    [SerializeField] GameObject addButtonObject;
    [SerializeField] GameObject finishButtonObject;
    //Karakterin durumunu gosteren bool degiskenler.
    //State Bools
    public bool idle;
    public bool inSkillTree;
    public bool work;
    public int currentWorkId;
    public PathPoints GatherPath;
    public PathPoints FarmPath;
    //switch case yapisinda kullanmak icin hangi isi yapicagini belirten degisken 0(Farm Path) 1(Gather Path)
    public int pathId;
    //Bulundugu rotada suan hedef aldigi noktanin listedeki indexi
    [SerializeField]int pathTargetIndex;
    //Unity animatorunde kullanmak icin tanimladigim bool degisken karakterin calistigini anlamak icin kullandim.
    public bool animWork;
    //Karakterin calisma animasyonunu kac kere tekrarladigini gormek icin kullandim.
    public int animLoop;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Animator>().SetBool("inwork", work);
        //Idle boolunun ayarlanmasi
        if (work || inSkillTree) 
        {
            idle = false;
        }
        else
        {
            idle = true;
        }
        //Calisiyor ise
        if (work)
        {
            //Ekrandaki bazi buronlari kapatip ekrana kronometre ve calismayi bitirme butonunu getiri.
            addButtonObject.SetActive(false);
            finishButtonObject.SetActive(true);
            inSkillTree = false;
            Move();
            GetComponent<Animator>().SetBool("walk", !animWork);
            //Ekrandaki butonlari acmak ve kapamak icin.
        } else if (inSkillTree)
        {
            finishButtonObject.SetActive(false);
            addButtonObject.SetActive(false);
        } else if (idle) 
        {
            finishButtonObject.SetActive(false);
            addButtonObject.SetActive(true);
        }
    }
    //Karakterin onceden belirlenmis yollarda haraket edip calisma alanina geldiginda belli bir sure calismasini ve sonra "topladiklarini" depoya goturmesini saglayan fonkiyon.
    void Move() 
    {
        //Iki casede ayni islemleri yapiyo sadece set ettikleri animasyon boollari ve sectikleri pathler farkli
        switch(pathId)
        {
            case 0:
                if (!animWork) charAgent.destination = FarmPath.Path[pathTargetIndex].position;
                if (getDistance(FarmPath.Path[pathTargetIndex].position) < .5f)
                {
                    if (pathTargetIndex==0) 
                    {
                        animWork = true;
                        GetComponent<Animator>().SetBool("farm", true);
                    }
                    else if(pathTargetIndex == FarmPath.Path.Count-1)
                    {
                        animWork = true;
                        GetComponent<Animator>().SetBool("putdown",true);
                        GetComponent<Animator>().SetBool("false", false);
                        pathTargetIndex = 0;
                    }
                    else
                    {
                        pathTargetIndex++;
                    }
                    if (animLoop > 20) 
                    {
                        if (pathTargetIndex == FarmPath.Path.Count-1) 
                        {
                            pathTargetIndex = 0;
                        }
                        else
                        {
                            pathTargetIndex++;
                        }
                        animWork = false;
                        animLoop = 0;
                    }      
                }
                break;
                //Yukarda acikladim.
                case 1:
                if (!animWork) charAgent.destination = GatherPath.Path[pathTargetIndex].position;
                if (getDistance(GatherPath.Path[pathTargetIndex].position) < .5f)
                {
                    if (pathTargetIndex == 0)
                    {
                        animWork = true;
                        GetComponent<Animator>().SetBool("gather", true);
                    }
                    else if (pathTargetIndex == GatherPath.Path.Count - 1)
                    {
                        animWork = true;
                        GetComponent<Animator>().SetBool("putdown", true);
                        GetComponent<Animator>().SetBool("gather", false);
                        pathTargetIndex = 0;
                    }
                    else
                    {
                        pathTargetIndex++;
                    }
                    if (animLoop > 20)
                    {
                        if (pathTargetIndex == GatherPath.Path.Count - 1)
                        {
                            pathTargetIndex = 0;
                        }
                        else
                        {
                            pathTargetIndex++;
                        }
                        animWork = false;
                        animLoop = 0;
                    }
                }
                break;
        }
    }
    //Iki nokta arasindaki mesafeyi olcmek icin coklu kullanmalarda daha kisa bir yazim olsun diye kullandim.
    float getDistance(Vector3 target) 
    {
        return Vector3.Distance(gameObject.transform.position,target);
    }
    //Unity'nin icerisinden animasyonlarin icersine event atayip public fonksiyonlari cagirabiliyoruz burasi calisma animasyonlarinda cagiriliyor.
    public void AnimLoop() 
    {
        animLoop++;
    }
    //Yaptigi isi resetlemek icin kullandim.
    public void ResetAnim() 
    {
        animWork = false;
        GetComponent<Animator>().SetBool("putdown", false);
    }
}
