using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController {

	private const int MIN_ITEMS_FOR_MATCH = 3;

	public static MatchInfo GetMatchInformation(Unit[,] gridItems, Unit item) {
		MatchInfo m = new MatchInfo ();
		m.match = null;
		//First checks if there was a horizontal match, then checks vertically
		List<Unit> hMatch = SearchHorizontally (gridItems, gridItems.GetLength(0), item);
		List<Unit> vMatch = SearchVertically (gridItems, gridItems.GetLength(1), item);

		//Debug.Log(string.Format("H-Count = {0}, V-Count = {1}", hMatch.Count, vMatch.Count));

		if (hMatch.Count >= MIN_ITEMS_FOR_MATCH && hMatch.Count > vMatch.Count) {
			//Debug.Log ("Horizontal match!");
			m.matchStartingX = GetMinimumX (hMatch);
			m.matchEndingX = GetMaximumX (hMatch);
			m.matchStartingY = m.matchEndingY = hMatch [0].Y;
			m.match = hMatch;
		} else if (vMatch.Count >= MIN_ITEMS_FOR_MATCH) {
			//Debug.Log ("Vertical match!");
			m.matchStartingY = GetMinimumY (vMatch);
			m.matchEndingY = GetMaximumY (vMatch);
			m.matchStartingX = m.matchEndingX = vMatch [0].X;
			m.match = vMatch;
		} else {
			//Debug.Log("None!");
		}

		return m;
	}

	private static List<Unit> SearchHorizontally(Unit[,] gridItems, int size, Unit item) {
		List<Unit> hItems = new List<Unit> () { item };
		int left = item.X - 1;
		int right = item.X + 1;

		while (left >= 0 && gridItems [left, item.Y].Class == item.Class) {
			hItems.Add (gridItems [left, item.Y]);
			left--;
		}

		while (right < size && gridItems [right, item.Y].Class == item.Class) {
			hItems.Add (gridItems [right, item.Y]);
			right++;
		}

		return hItems;
	}

	private static List<Unit> SearchVertically(Unit[,] gridItems, int size, Unit item) {
		List<Unit> vItems = new List<Unit> () { item };
		int bottom = item.Y - 1;
		int top = item.Y + 1;

		while (bottom >= 0 && gridItems [item.X, bottom].Class == item.Class) {
			vItems.Add (gridItems [item.X, bottom]);
			bottom--;
		}

		while (top < size && gridItems [item.X, top].Class == item.Class) {
			vItems.Add (gridItems [item.X, top]);
			top++;
		}

		return vItems;
	}

	private static int GetMaximumX(List<Unit> items) {
		float[] indexes = new float [items.Count];
		for (int i = 0; i < indexes.Length; i++) {
			indexes [i] = items [i].X;
		}

		return (int)Mathf.Max (indexes);
	}

	private static int GetMaximumY(List<Unit> items) {
		float[] indexes = new float [items.Count];
		for (int i = 0; i < indexes.Length; i++) {
			indexes [i] = items [i].Y;
		}

		return (int)Mathf.Max (indexes);
	}

	private static int GetMinimumX(List<Unit> items) {
		float[] indexes = new float [items.Count];
		for (int i = 0; i < indexes.Length; i++) {
			indexes [i] = items [i].X;
		}

		return (int)Mathf.Min (indexes);
	}

	private static int GetMinimumY(List<Unit> items) {
		float[] indexes = new float [items.Count];
		for (int i = 0; i < indexes.Length; i++) {
			indexes [i] = items [i].Y;
		}

		return (int)Mathf.Min (indexes);
	}

}
