using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valve.VR.InteractionSystem {
	public class TeleportPoint : TeleportMarkerBase {
		public enum TeleportPointType { MoveToLocation, SwitchToNewScene };

		[SerializeField] private TeleportPointType teleportType = TeleportPointType.MoveToLocation;
		[SerializeField] private string switchToScene;
        [SerializeField] private float minActivateDistance;
		public TeleportPointType TeleportType { get { return teleportType; } }

		private Player player;

		public override bool ShowReticle { get { return false; } }

		private void Start() {
			player = Player.Instance;
		}

		public override bool ShouldActivate(Vector3 playerPosition) {
			return ((transform.position - playerPosition).sqrMagnitude >= minActivateDistance * minActivateDistance);
		}

		public override bool ShouldMovePlayer() {
			return false;
		}

        public override void TeleportPlayer(Vector3 pointedAtPosition)
        {
            player.trackingOriginTransform.position = transform.position;
        }

        public override void Highlight(bool highlight) {
		}

		public override void SetAlpha(float tintAlpha, float alphaPercent) {

		}

		public void TeleportToScene() {
			if (!string.IsNullOrEmpty(switchToScene))
                SteamVR_LoadLevel.Begin(switchToScene);
			else
				Debug.LogError("TeleportPoint: Invalid scene name to switch to: " + switchToScene);
		}

        public override void UpdateVisuals()
        {
            
        }
    }
}
