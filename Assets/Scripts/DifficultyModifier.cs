using UnityEngine;
using System.Collections;

public class DifficultyModifier : MonoBehaviour {

	/*
	Difficulty modifiers are stored in static arrays with 3 elements
	0 = easy, 1 = normal, 2 = hard
	*/
	
	#region Player modifiers
	static float[] playerSpeed                    = {   1.10f,   1.00f,   0.90f  };
	static float[] playerLaserRate                = {   1.20f,   1.00f,   0.90f  };
	static float[] playerLaserDamage              = {   1.20f,   1.00f,   0.90f  };
	static float[] playerLaserSpeed               = {   1.20f,   1.00f,   0.90f  };
	static float[] playerTorpedoDamage            = {   1.20f,   1.00f,   0.90f  };
	static float[] playerTorpedoSpeed             = {   1.20f,   1.00f,   0.90f  };
	
	// Changes that need game/level reload
	static float[] playerHitPoints                = {   1.20f,   1.00f,   0.90f  };
	static float[] playerShieldHitPoints          = {   1.20f,   1.00f,   0.90f  };
	static float[] ftlTimer                       = {   0.90f,   1.00f,   1.10f  };
	#endregion
	
	
	#region Enemy modifiers
	static float[] enemySpawnRate                 = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyExitRate                  = {   1.10f,   1.00f,   0.90f  };
	static float[] enemyLaserDamage               = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyLaserRate                 = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyLaserSpeed                = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberSpawnRate                = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberSpeed                    = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoDamage            = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoRate              = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoSpeed             = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoBarrage           = {  -1.00f,   0.00f,   1.00f  };
	static float[] bomberTorpedoBarrageRate       = {   1.10f,   1.00f,   0.90f  };
	
	// Changes that need game/level reload
	static float[] enemyHitPoints                 = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberHitPoints                = {   0.85f,   1.00f,   1.10f  };
	#endregion
	
	
	#region Environment modifiers
	static float[] asteroidSpawnRate              = {   0.90f,   1.00f,   1.10f  };
	static float[] ufoSpawnRate                   = {   1.10f,   1.00f,   0.90f  };
	#endregion


	public static int Index() {
		return PlayerPrefsManager.GetDifficulty() - 1;
	}

	
	public static float ForPlayerShieldHitPoints() {
		return playerShieldHitPoints[Index()];
	}
	
	public static float ForPlayerLaserRate() {
		return playerLaserRate[Index()];
	}
	
	public static float ForPlayerLaserDamage() {
		return playerLaserDamage[Index()];
	}
	
	public static float ForPlayerLaserSpeed() {
		return playerLaserSpeed[Index()];
	}
	
	public static float ForPlayerTorpedoDamage() {
		return playerTorpedoDamage[Index()];
	}
	
	public static float ForPlayerTorpedoSpeed() {
		return playerTorpedoSpeed[Index()];
	}
	
	public static float ForPlayerHitPoints() {
		return playerHitPoints[Index()];
	}
	
	public static float ForPlayerSpeed() {
		return playerSpeed[Index()];
	}
	
	public static float ForFtlTimer() {
		return ftlTimer[Index()];
	}
	
	public static float ForEnemySpawnRate() {
		return enemySpawnRate[Index()];
	}
	
	public static float ForEnemyHitPoints() {
		return enemyHitPoints[Index()];
	}
	
	public static float ForEnemyExitRate() {
		return enemyExitRate[Index()];
	}
	
	public static float ForEnemyLaserDamage() {
		return enemyLaserDamage[Index()];
	}
	
	public static float ForEnemyLaserRate() {
		return enemyLaserRate[Index()];
	}
	
	public static float ForEnemyLaserSpeed() {
		return enemyLaserSpeed[Index()];
	}
	
	public static float ForBomberSpawnRate() {
		return bomberSpawnRate[Index()];
	}
	
	public static float ForBomberHitPoints() {
		return bomberHitPoints[Index()];
	}
	
	public static float ForBomberSpeed() {
		return bomberSpeed[Index()];
	}
	
	public static float ForBomberTorpedoDamage() {
		return bomberTorpedoDamage[Index()];
	}
	
	public static float ForBomberTorpedoRate() {
		return bomberTorpedoRate[Index()];
	}
	
	public static float ForBomberTorpedoSpeed() {
		return bomberTorpedoSpeed[Index()];
	}
	
	public static float ForBomberTorpedoBarrage() {
		return bomberTorpedoBarrage[Index()];
	}
	
	public static float ForBomberTorpedoBarrageRate() {
		return bomberTorpedoBarrageRate[Index()];
	}
	
	public static float ForAsteroidSpawnRate() {
		return asteroidSpawnRate[Index()];
	}
	
	public static float ForUfoSpawnRate() {
		return ufoSpawnRate[Index()];
	}	
}
