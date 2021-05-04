namespace NPC {

	public enum ActionType : sbyte {
		ReturningBook = 0,
		WantingBook = 1,
		Registration = 2,
		ReadingBook = 3,
	}

	public class NpcAction {
		public ActionType Type;
		/** Additional info if needed like book title */
		public string Info;
	}
}