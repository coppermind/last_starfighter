using UnityEngine;
using System.Collections;

public class DifficultyModifier : MonoBehaviour {

	/**
	 * Difficulty modifiers are stored in static arrays
	 * with 3 elements
	 * 0 = easy
	 * 1 = normal
	 * 2 = hard
	 */
	 
	#region Player modifiers
	static float[] playerShield                   = {   1.20f,   1.00f,   0.90f  };
	static float[] playerLaserDamage              = {   1.20f,   1.00f,   0.90f  };
	static float[] playerLaserSpeed               = {   1.20f,   1.00f,   0.90f  };
	static float[] playerTorpedoDamage            = {   1.20f,   1.00f,   0.90f  };
	static float[] playerTorpedoSpeed             = {   1.20f,   1.00f,   0.90f  };
	static float[] playerHitPoints                = {   1.20f,   1.00f,   0.90f  };
	static float[] playerSpeed                    = {   1.10f,   1.00f,   0.90f  };
	static float[] ftlTimer                       = {   0.90f,   1.00f,   1.10f  };
	#endregion
	
	
	#region Enemy modifiers
	static float[] enemySpawnRate                 = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyHitPoints                 = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyExitRate                  = {   1.10f,   1.00f,   0.90f  };
	static float[] enemyLaserDamage               = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyLaserRate                 = {   0.90f,   1.00f,   1.10f  };
	static float[] enemyLaserSpeed                = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberSpawnRate                = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberHitPoints                = {   0.85f,   1.00f,   1.10f  };
	static float[] bomberSpeed                    = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoDamage            = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoRate              = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoSpeed             = {   0.90f,   1.00f,   1.10f  };
	static float[] bomberTorpedoBarrage           = {  -1.00f,   0.00f,   1.00f  };
	static float[] bomberTorpedoBarrageRate       = {   1.10f,   1.00f,   0.90f  };
	#endregion
	
	
	#region Environment modifiers
	static float[] asteroidSpawnRate              = {   0.90f,   1.00f,   1.10f  };
	static float[] ufoSpawnRate                   = {   1.10f,   1.00f,   0.90f  };
	#endregion


	public static int DifficultyIndex() {
		return PlayerPrefsManager.GetDifficulty() - 1;
	}

	
	public static float For(string element) {
		int i = DifficultyIndex();
		float val;
		switch (element) {
			// Player modifiers
			case "PlayerShield":                       val = playerShield[i]; break;
			case "PlayerLaserDamage":                  val = playerLaserDamage[i]; break;
			case "PlayerLaserSpeed":                   val = playerLaserSpeed[i]; break;
			case "PlayerTorpedoDamage":                val = playerTorpedoDamage[i]; break;
			case "PlayerTorpedoSpeed":                 val = playerTorpedoSpeed[i]; break;
			case "PlayerHitPoints":                    val = playerHitPoints[i]; break;
			case "PlayerSpeed":                        val = playerSpeed[i]; break;
			case "FtlTimer":                           val = ftlTimer[i]; break;
			
			// Enemy modifiers
			case "EnemySpawnRate":                     val = enemySpawnRate[i]; break;
			case "EnemyHitPoints":                     val = enemyHitPoints[i]; break;
			case "EnemyExitRate":                      val = enemyExitRate[i]; break;
			case "EnemyLaserDamage":                   val = enemyLaserDamage[i]; break;
			case "EnemyLaserRate":                     val = enemyLaserRate[i]; break;
			case "EnemyLaserSpeed":                    val = enemyLaserSpeed[i]; break;
			case "BomberSpawnRate":                    val = bomberSpawnRate[i]; break;
			case "BomberHitPoints":                    val = bomberHitPoints[i]; break;
			case "BomberSpeed":                        val = bomberSpeed[i]; break;
			case "BomberTorpedoDamage":                val = bomberTorpedoDamage[i]; break;
			case "BomberTorpedoRate":                  val = bomberTorpedoRate[i]; break;
			case "BomberTorpedoSpeed":                 val = bomberTorpedoSpeed[i]; break;
			case "BomberTorpedoBarrage":               val = bomberTorpedoBarrage[i]; break;
			case "BomberTorpedoBarrageRate":           val = bomberTorpedoBarrageRate[i]; break;
			
			// Environment modifiers
			case "AsteroidSpawnRate":                  val = asteroidSpawnRate[i]; break;
			case "UfoSpawnRate":                       val = ufoSpawnRate[i]; break;
			
			default:
				val = 0; break;
		}
		return val;
	}
	
}
