using System.Collections;
using System.Collections.Generic;

public class MatchInfo {

	public List<Unit> match;
	public int matchStartingX;
	public int matchEndingX;
	public int matchStartingY;
	public int matchEndingY;

	public bool ValidMatch {
		get{ return this.match != null; }
	}

	public bool IsHorizontal {
		get { return this.matchStartingY == this.matchEndingY; }
	}

	public bool IsVertical {
		get { return this.matchStartingX == this.matchEndingX; }
	}
}
