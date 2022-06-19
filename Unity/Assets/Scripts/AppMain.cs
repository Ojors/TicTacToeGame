using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Game.UI.UIImplement;
using UnityEngine;

public class AppMain : MonoBehaviour
{
    public static MonoBehaviour appInstance;
    
    void Start()
    {
        appInstance = this;
        
        UIManager.Instance.ShowUI<UIBackground>();
        UIManager.Instance.ShowUI<UIBattleSelect>();
    }
}
