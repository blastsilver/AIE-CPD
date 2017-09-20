using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UIActionListener
{
	public GameObject menuObject = null;
	public GameObject pauseObject = null;
	public GameObject gameoverObject = null;
	public GameObject pausebuttonObject = null;
	public WorldManager worldManager = null;

	override public void HandleEvent(UIActionEvent e)
	{
		switch (e)
		{
		case UIActionEvent.GAME_MENU:
		case UIActionEvent.GAME_BACK:
			menuObject.SetActive (true);
			pauseObject.SetActive (false);
			gameoverObject.SetActive (false);
			pausebuttonObject.SetActive (false);
			worldManager.HandleEvent (e);
			break;
		case UIActionEvent.GAME_EXIT:
			Application.Quit ();
			break;
		case UIActionEvent.GAME_PAUSE:
			pauseObject.SetActive (true);
			worldManager.HandleEvent (e);
			break;
		case UIActionEvent.GAME_START:
			menuObject.SetActive (false);
			pausebuttonObject.SetActive (true);
			worldManager.HandleEvent (e);
			break;
		case UIActionEvent.GAME_FINISH:
			gameoverObject.SetActive (true);
			worldManager.HandleEvent (e);
			break;
		case UIActionEvent.GAME_RESTART:
			gameoverObject.SetActive (false);
			pausebuttonObject.SetActive (true);
			worldManager.HandleEvent (e);
			break;
		case UIActionEvent.GAME_CONTINUE:
			pauseObject.SetActive (false);
			worldManager.HandleEvent (e);
			break;
		}
	}
}