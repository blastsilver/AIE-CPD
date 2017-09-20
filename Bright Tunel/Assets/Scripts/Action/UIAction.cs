using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAction : MonoBehaviour
{
	public UIActionEvent action;
	private UIActionListener m_listener = null;

	void Awake()
	{
		UIActionListener listener = GetComponentInParent<UIActionListener>();
		if (listener.gameObject != gameObject)
		{
			m_listener = listener;
		}
	}

	public void SendEvent() { if (m_listener != null) m_listener.HandleEvent(action); }

	public void SendEvent(UIActionEvent e) { m_listener.HandleEvent(e); }

	public void SetEventListener(UIActionListener listener) { m_listener = listener; }
}