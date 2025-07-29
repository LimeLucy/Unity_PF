using System.Collections;

namespace Casual
{
	public class DialogueUIController
	{
		private readonly UIDialogue _dialogueUI;

		public DialogueUIController(UIDialogue dialogueUI)
		{
			_dialogueUI = dialogueUI;
		}

		public IEnumerator Show(Scripts script)
		{
			yield return _dialogueUI.SetDialogueText(script);
		}

		public void Hide() => _dialogueUI.HideDialogueUI();
	}

	public class SelectUIController
	{
		private readonly UISelect _choiceUI;

		public SelectUIController(UISelect choiceUI)
		{
			_choiceUI = choiceUI;
		}

		public void Show(SelectState state) => _choiceUI.SetSelectText(state);
		public void Hide() => _choiceUI.HideSelectUI();
	}

	public class GameMenuUIController
	{
		private readonly UIInGameMenu _menuUI;

		public GameMenuUIController(UIInGameMenu menuUI)
		{
			_menuUI = menuUI;
		}

		public void Show() => _menuUI.ShowMenu();
		public void Hide() => _menuUI.HideMenu();
	}

	public class NameMarkerUIController
	{
		private readonly UINameMarker _nameMarkerUI;

		public NameMarkerUIController(UINameMarker namemarkerUI)
		{
			_nameMarkerUI = namemarkerUI;
		}

		public void Show(ZombieController zombie) => _nameMarkerUI.Show(zombie);
		public void Hide() => _nameMarkerUI.Hide();
	}

	public class UIMediator
	{
		public DialogueUIController Dialogue { get; }
		public SelectUIController Choice { get; }
		public GameMenuUIController Menu { get; }
		public NameMarkerUIController NameMarker { get; }

		public UIMediator(DialogueUIController dialogue, SelectUIController choice, GameMenuUIController menu, NameMarkerUIController nameMarker)
		{
			Dialogue = dialogue;
			Choice = choice;
			Menu = menu;
			NameMarker = nameMarker;
		}
	}
}