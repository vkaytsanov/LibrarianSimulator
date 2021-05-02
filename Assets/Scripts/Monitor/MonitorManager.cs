using UnityEngine;

namespace Monitor {

	public class NamedArrayAttribute : PropertyAttribute {
		public readonly string[] Names;

		public NamedArrayAttribute(string[] names) {
			Names = names;
		}
	}

	public class MonitorManager : MonoBehaviour {
		private MonitorState _currentState;

		[SerializeField] [NamedArrayAttribute(new string[] {"Menu", "SearchingBook", "CheckingBook", "Registering"})]
		private GameObject[] screens;
		
		private void Start() {
			_currentState = MonitorState.Menu;
		}
		
		public void OnStateChange(int nextState) {
			GetAssociatedScreen(_currentState).SetActive(false);
			GetAssociatedScreen((MonitorState) nextState).SetActive(true);

			_currentState = (MonitorState) nextState;
		}
		private GameObject GetAssociatedScreen(MonitorState state) {
			return screens[(sbyte) state];
		}
	}

}