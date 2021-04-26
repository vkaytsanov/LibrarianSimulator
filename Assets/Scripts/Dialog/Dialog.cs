using Unity.Mathematics;

namespace Dialog {

	public enum DialogSide {
		NpcSide,
		PlayerSide
	}
	public class Dialog {
		public readonly string[] Sentences;
		public readonly DialogSide[] Sides;

		public Dialog(string[] sentences) {
			Sentences = sentences;
			Sides = new DialogSide[Sentences.Length];
			for (int i = 0; i < Sentences.Length; i++) {
				Sides[i] = DialogSide.NpcSide;
			}
		}

		public Dialog(string[] sentences, DialogSide[] sides) {
			Sentences = sentences;
			Sides = sides;
		}
	}

}