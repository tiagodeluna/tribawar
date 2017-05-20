using System.Collections;
using UnityEngine;

public static class TransformExtensions {

	//Moves the selected element to the target position
	public static IEnumerator Move (this Transform t, Vector3 target, float duration) {
		Vector3 diffVector = target - t.position;
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		float counter = 0;
		while (counter < duration) {
			//Distance moved at every fraction of time
			float movAmount = (Time.deltaTime * diffLength) / duration;
			//The new item position
			t.position += diffVector * movAmount;
			counter += Time.deltaTime;
			yield return null;
		}

		t.position = target;
	}

	//Changes select item scale
	public static IEnumerator Scale (this Transform t, Vector3 target, float duration) {
		Vector3 diffVector = target - t.localScale;
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		float counter = 0;
		while (counter < duration) {
			float movAmount = (Time.deltaTime * diffLength) / duration;
			//The new item scale
			t.localScale += diffVector * movAmount;
			counter += Time.deltaTime;
			yield return null;
		}

		t.localScale = target;
	}
}
