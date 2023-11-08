using UnityEngine;

namespace Managers {
    public class GeneralManager : MonoBehaviour {

        private void Awake() {
            FenManager.Init();
        }

    }
}
