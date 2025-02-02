using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Talisman/DialogueSequence", fileName = "New Dialogue Sequnce")]
public class DialogueSequence : Dialogue
{
    public string m_section;
    public List<DialogueObject> m_sequence;
}

