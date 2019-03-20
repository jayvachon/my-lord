using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
	YearStart,
	InYear
}

public static class Game
{
    public static GameState State { get; set; }
}
