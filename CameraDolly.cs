using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    //Kameranin gidicegi hedef nokta.
    public Transform moveTarget;
    //Kameranin gidicegi hedef nokta.
    public Transform lookTarget;
    Vector3 refCurrentVel;
    //kameranin bir noktadan digerine gecisinde yumusak bir gecis icin bir degisken.
    public float smoothTime;
    public GameObject camera;
    //Kameranin hedeften her eksende ne kadar uzakta olucagini belirleyen degiskenler.
    [SerializeField] float xOffSet;
    [SerializeField] float yOffSet;
    [SerializeField] float zOffSet;
    void Start()
    {
        
    }
    void Update()
    {   
        //Kameranin bulunucagi noktanin offset verileri ile hesaplanmasi.
        Vector3 newVector = new Vector3(moveTarget.position.x-xOffSet, moveTarget.position.y + yOffSet, moveTarget.position.z - zOffSet);
        //Kameranin posisyonunun ayarlanmasi.
        transform.position = Vector3.SmoothDamp(transform.position,newVector,ref refCurrentVel,smoothTime);
        //Kameranin belirlenen lookTargeta baktirma
        camera.transform.LookAt(lookTarget,Vector3.up);
    }
}
