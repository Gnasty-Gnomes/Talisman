using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Talisman/ControllerImages", fileName = "New Controller Image")]
public class ControllerImages : ScriptableObject
{
    public string m_spriteAsset;
    public int m_menuNavigation;
    public int m_menuSelect;
    public int m_menuBack;
    public int m_movement;
    public int m_camera;
    public int m_jump;
    public int m_interactOne;
    public int m_interactTwo;
    public int m_attack;
    public int m_block;
    public int m_heal;
    public int m_pause;
    public int m_sprint;
    public Sprite m_controlsDisplay; 
}
