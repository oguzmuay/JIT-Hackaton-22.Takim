using UnityEngine;
using UnityEngine.EventSystems;
public class WorkButton : MonoBehaviour,IPointerDownHandler
{   
    //Slotun id'sinin tanimlanmasi
    [SerializeField] int id;
    void Start()
    {
        //Tanimlanan degiskenin parent objesinde verinin alinip esitlenmesi.
        id = GetComponentInParent<SkillSlot>().id;
    }
    //Karakterin calismaya baslamasi icin kullanilan fonksiyon.
    void StartWork()
    {
        if (!FindObjectOfType<Character>().work) 
        {
            Debug.Log("Work Work Work");
            //Hangi yetenek icin calistigini belirtmek icin bir degisken atamasi,
            FindObjectOfType<Character>().currentWorkId = id;
            //Karakterin calisirken meyve toplama ve bahcede calisma gibi iki tane animasyonlu goruntusu var.
            //Burda ikisinden birni rastgele olarak ekleniyor.
            //Planlarda kilicla egitim zirh dovme gibi animasyonlarda eklemek vardi ancak zaman ve animasyon eksikligi oldugu icin eklmeyemedik.
            FindObjectOfType<Character>().pathId = Random.Range(0,2);
            //Calistigini belirten degisken.
            FindObjectOfType<Character>().work = true;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        StartWork();
    }
}
