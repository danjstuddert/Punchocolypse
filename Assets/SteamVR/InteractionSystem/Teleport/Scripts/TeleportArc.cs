﻿using UnityEngine;

namespace Valve.VR.InteractionSystem {
	public class TeleportArc : MonoBehaviour {
        [SerializeField] private LineRenderer linePrefab;
		[SerializeField] private int segmentCount = 60;
		[SerializeField] private float thickness = 0.01f;

		[Tooltip("The amount of time in seconds to predict the motion of the projectile.")]
		[SerializeField] private float arcDuration = 3.0f;

		[Tooltip("The amount of time in seconds between each segment of the projectile.")]
		[SerializeField] private float segmentBreak = 0.025f;

		[Tooltip("The speed at which the line segments of the arc move.")]
		[SerializeField] private float arcSpeed = 0.2f;

		[SerializeField] private Material material;

		[HideInInspector]
		public int traceLayerMask = 0;

		public float ArcDuration { get { return arcDuration; } }

		private LineRenderer[] lineRenderers;
		private float arcTimeOffset = 0.0f;
		private float prevThickness = 0.0f;
		private int prevSegmentCount = 0;
		private bool showArc = true;
		private Vector3 startPos;
		private Vector3 projectileVelocity;
		private bool useGravity = true;
		private Transform arcObjectsTransfrom;
		private bool arcInvalid = false;

		private void Start() {
			arcTimeOffset = Time.time;
		}

		void Update() {
			if (thickness != prevThickness || segmentCount != prevSegmentCount) {
				CreateLineRendererObjects();
				prevThickness = thickness;
				prevSegmentCount = segmentCount;
			}
		}

		private void CreateLineRendererObjects() {
			//Destroy any existing line renderer objects
			if (arcObjectsTransfrom != null) {
                foreach (Transform child in arcObjectsTransfrom) {
                    SimplePool.Despawn(child.gameObject);
                }
            } else {
                GameObject arcObjectsParent = new GameObject("ArcObjects");
                arcObjectsTransfrom = arcObjectsParent.transform;
                arcObjectsTransfrom.SetParent(transform);
            }

			//Create new line renderer objects
			lineRenderers = new LineRenderer[segmentCount];
			for (int i = 0; i < segmentCount; ++i) {
                GameObject newObject = SimplePool.Spawn(linePrefab.gameObject, Vector3.zero, linePrefab.transform.rotation);
				newObject.transform.SetParent(arcObjectsTransfrom);

				lineRenderers[i] = newObject.GetComponent<LineRenderer>();

                if (lineRenderers[i].material != material)
                    lineRenderers[i].material = material;

                if (lineRenderers[i].startWidth != thickness)
				    lineRenderers[i].startWidth = thickness;

                if (lineRenderers[i].endWidth != thickness)
                    lineRenderers[i].endWidth = thickness;

				lineRenderers[i].enabled = false;
			}
		}

		public void SetArcData(Vector3 position, Vector3 velocity, bool gravity, bool pointerAtBadAngle) {
			startPos = position;
			projectileVelocity = velocity;
			useGravity = gravity;

			if (arcInvalid && !pointerAtBadAngle)
				arcTimeOffset = Time.time;

			arcInvalid = pointerAtBadAngle;
		}

		public void Show() {
			showArc = true;
			if (lineRenderers == null)
				CreateLineRendererObjects();
		}

		public void Hide() {
			//Hide the line segments if they were previously being shown
			if (showArc)
				HideLineSegments(0, segmentCount);

			showArc = false;
		}


		//-------------------------------------------------
		// Draws each segment of the arc individually
		//-------------------------------------------------
		public bool DrawArc(out RaycastHit hitInfo) {
			float timeStep = arcDuration / segmentCount;

			float currentTimeOffset = (Time.time - arcTimeOffset) * arcSpeed;

			//Reset the arc time offset when it has gone beyond a segment length
			if (currentTimeOffset > (timeStep + segmentBreak)) {
				arcTimeOffset = Time.time;
				currentTimeOffset = 0.0f;
			}

			float segmentStartTime = currentTimeOffset;

			float arcHitTime = FindProjectileCollision(out hitInfo);

			if (arcInvalid) {
				//Only draw first segment
				lineRenderers[0].enabled = true;
				lineRenderers[0].SetPosition(0, GetArcPositionAtTime(0.0f));
				lineRenderers[0].SetPosition(1, GetArcPositionAtTime(arcHitTime < timeStep ? arcHitTime : timeStep));

				HideLineSegments(1, segmentCount);
			} else {
				//Draw the first segment outside the loop if needed
				int loopStartSegment = 0;
				if (segmentStartTime > segmentBreak) {
					float firstSegmentEndTime = currentTimeOffset - segmentBreak;
					if (arcHitTime < firstSegmentEndTime)
						firstSegmentEndTime = arcHitTime;

					DrawArcSegment(0, 0.0f, firstSegmentEndTime);
					loopStartSegment = 1;
				}

				bool stopArc = false;
				int currentSegment = 0;
				if (segmentStartTime < arcHitTime) {
					for (currentSegment = loopStartSegment; currentSegment < segmentCount; ++currentSegment) {
						//Clamp the segment end time to the arc duration
						float segmentEndTime = segmentStartTime + timeStep;
						if (segmentEndTime >= arcDuration) {
							segmentEndTime = arcDuration;
							stopArc = true;
						}

						if (segmentEndTime >= arcHitTime) {
							segmentEndTime = arcHitTime;
							stopArc = true;
						}

						DrawArcSegment(currentSegment, segmentStartTime, segmentEndTime);

						segmentStartTime += timeStep + segmentBreak;

						//If the previous end time or the next start time is beyond the duration then stop the arc
						if (stopArc || segmentStartTime >= arcDuration || segmentStartTime >= arcHitTime)
							break;
					}
				} else
					currentSegment--;

				//Hide the rest of the line segments
				HideLineSegments(currentSegment + 1, segmentCount);
			}

			return arcHitTime != float.MaxValue;
		}

		private void DrawArcSegment(int index, float startTime, float endTime) {
			lineRenderers[index].enabled = true;
			lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
			lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
		}

		public void SetColor(Color color) {
			for (int i = 0; i < segmentCount; ++i) {
				lineRenderers[i].startColor = color;
				lineRenderers[i].endColor = color;
			}
		}

		private float FindProjectileCollision(out RaycastHit hitInfo) {
			float timeStep = arcDuration / segmentCount;
			float segmentStartTime = 0.0f;

			hitInfo = new RaycastHit();

			Vector3 segmentStartPos = GetArcPositionAtTime(segmentStartTime);
			for (int i = 0; i < segmentCount; ++i) {
				float segmentEndTime = segmentStartTime + timeStep;
                Vector3 segmentEndPos = GetArcPositionAtTime(segmentEndTime);


				if (Physics.Linecast(segmentStartPos, segmentEndPos, out hitInfo, traceLayerMask)) {
					if (hitInfo.collider.GetComponent<IgnoreTeleportTrace>() == null) {
						Util.DrawCross(hitInfo.point, Color.red, 0.5f);
						float segmentDistance = Vector3.Distance(segmentStartPos, segmentEndPos);
						float hitTime = segmentStartTime + (timeStep * (hitInfo.distance / segmentDistance));
						return hitTime;
					}
				}

				segmentStartTime = segmentEndTime;
				segmentStartPos = segmentEndPos;
			}

			return float.MaxValue;
		}

		public Vector3 GetArcPositionAtTime(float time) {
			Vector3 gravity = useGravity ? Physics.gravity : Vector3.zero;

			Vector3 arcPos = startPos + ((projectileVelocity * time) + (0.5f * time * time) * gravity);
			return arcPos;
		}

		private void HideLineSegments(int startSegment, int endSegment) {
			if (lineRenderers != null) {
				for (int i = startSegment; i < endSegment; ++i) {
					lineRenderers[i].enabled = false;
				}
			}
		}
	}
}
