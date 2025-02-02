using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbolMatch : Puzzle
{
    // Door and conditions    
    [Space(5), Header("Door and Conditions"), Space(5)]
    public bool m_lockRotationOnSolve = true;

    // Lightup to signify in correct position
    [Space(5), Header("Unlock Visual Signifier"), Space(5)]
    public Material m_lightMaterial;
    public MeshRenderer m_lightNotice;

    new void Start()
    {
        base.Start();
        CheckSolve();
    }

    public void CheckSolve()
    {
        if (m_input == m_output)
        {
            m_unlocked = true;
            List<Material> nm = m_lightNotice.sharedMaterials.ToList();
            nm.Add(m_lightMaterial);
            m_lightNotice.sharedMaterials = nm.ToArray();
            foreach (Door door in m_doors)
            {
                door.CheckState();
            }
            if(m_bridge != null)
            {
                m_bridge.CheckState();
            }
        }
    }

    public override void RotatePuzzle()
    {
        if (m_lockRotationOnSolve && m_unlocked)
        {

        }
        else if (m_unlocked)
        {
            m_targetRotation = Quaternion.Euler(0, m_nextY, 0);
            m_rotations++;
            m_unlocked = false;
            List<Material> nm = m_lightNotice.sharedMaterials.ToList();
            nm.Remove(m_lightMaterial);
            m_lightNotice.sharedMaterials = nm.ToArray();
            foreach (Door door in m_doors)
            {
                door.CheckState();
            }
            if (m_bridge != null)
            {
                m_bridge.CheckState();
            }
            m_rotate = true;
        }
        else
        {
            m_targetRotation = Quaternion.Euler(0, m_nextY, 0);
            m_rotations++;
            m_rotate = true;
        }
    }

    public override void UpdatePositions()
    {
        if (m_input == Positions.THREE)
        {
            m_input = Positions.ONE;
        }
        else
        {
            m_input++;
        }
        CheckSolve();
    }
}
