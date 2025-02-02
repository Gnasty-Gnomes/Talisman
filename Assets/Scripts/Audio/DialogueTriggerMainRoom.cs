using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueTriggerMainRoom : MonoBehaviour
{
    public Dialogue m_dialogue;
    public bool m_stopInteractions;
    AudioManager m_audioManager;

    private void Start()
    {
        m_audioManager = GameManager.Instance.m_audioManager;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            m_audioManager.m_stopInteractions = m_stopInteractions;
            m_audioManager.MainRoom(m_dialogue);            
            Destroy(gameObject);
        }
    }
}

