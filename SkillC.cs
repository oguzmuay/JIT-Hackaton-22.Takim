using UnityEngine;
[System.Serializable]
public class SkillC
{
    public string sName;
    public string sTitle;
    public Sprite sIcon;
    public int sLevel;
    public float sHour;
    public float sMinute;
    public SkillC(string name) 
    {
        sName = name;
    }
}
//Yeteneklerin ozelliklerini barindiran class.