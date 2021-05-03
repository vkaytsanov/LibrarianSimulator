using Unity.Mathematics;

namespace Dialog {

	public enum DialogSide {
		NpcSide,
		PlayerSide
	}

	public class Dialogue {
		public string Sentence;
		public DialogSide DialogSide;

		public Dialogue(string sentence, DialogSide side = DialogSide.NpcSide) {
			Sentence = sentence;
			DialogSide = side;
		}
	}
	public class DialogueSequence {
		public readonly Dialogue[] Dialogues;

		public DialogueSequence(string[] sentences) {
			Dialogues = new Dialogue[sentences.Length];
			for (int i = 0; i < sentences.Length; i++) {
				Dialogues[i] = new Dialogue(sentences[i]);
			}
		}
	}

}