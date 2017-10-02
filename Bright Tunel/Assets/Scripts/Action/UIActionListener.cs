using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIActionListener : MonoBehaviour
{
	private UIActionListener m_listener = null;

	void Awake()
	{
		UIActionListener listener = GetComponentInParent<UIActionListener>();
		if (listener.gameObject != gameObject)
		{
			m_listener = listener;
		}
	}

	public void SendEvent(UIActionEvent e) { if (m_listener != null) m_listener.HandleEvent(e); }

	public void SetEventListener(UIActionListener listener) { m_listener = listener; }

	virtual public void HandleEvent(UIActionEvent e) { SendEvent(e); }
}