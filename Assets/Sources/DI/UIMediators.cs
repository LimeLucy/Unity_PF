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

	public class ChoiceUIController
	{
		private readonly UISelect _choiceUI;

		public ChoiceUIController(UISelect choiceUI)
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

	public class UIMediator
	{
		public DialogueUIController Dialogue { get; }
		public ChoiceUIController Choice { get; }
		public GameMenuUIController Menu { get; }

		public UIMediator(DialogueUIController dialogue, ChoiceUIController choice, GameMenuUIController menu)
		{
			Dialogue = dialogue;
			Choice = choice;
			Menu = menu;
		}
	}
}