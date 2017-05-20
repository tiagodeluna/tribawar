using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SwipeController {

	private static readonly SwipeController instance = new SwipeController();

	private const float MINIMUM_SWIPE_DISTANCE = 30f;

	private Vector3 startPos;
	private Vector3 endPos;
	private float swipeDist;

	private SwipeController () {}

	public static SwipeController Instance {
		get {
			return instance;
		}
	}

	public DirectionEnum SwipeTo() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;
			}
			else if (touch.phase == TouchPhase.Ended) {
				endPos = touch.position;
				swipeDist = (endPos - startPos).magnitude;
				if (swipeDist > MINIMUM_SWIPE_DISTANCE) {
					//Get Swipe direction
					return GetDirection ();
				}
			}
		}

		return DirectionEnum.NONE;
	}

	private DirectionEnum GetDirection() {
		Vector2 distance = endPos - startPos;

		if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y)) {
			if (distance.x > 0) {
				return DirectionEnum.RIGHT;
			} else {
				return DirectionEnum.LEFT;
			}
		} else {
			if (distance.y > 0) {
				return DirectionEnum.TOP;
			} else {
				return DirectionEnum.BOTTOM;
			}
		}
	}
}
