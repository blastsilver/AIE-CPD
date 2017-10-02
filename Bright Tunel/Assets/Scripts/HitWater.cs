using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWater : MonoBehaviour
{
    public GameObject m_UImanager = null;

    private void Start()
    {
        m_UImanager = GameObject.FindGameObjectWithTag("Canvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            m_UImanager.GetComponent<UIManager>().HandleEvent(UIActionEvent.GAME_FINISH);
        }
    }
}
