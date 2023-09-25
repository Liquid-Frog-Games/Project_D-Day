# Balancing Locations
In this file I will describe the filenames and functions where you can modify values for balancing.

### General
- Playerhealth: Levelmanager.Start()
- StartingCurrency: Levelmanager.Start()
- StartingSouls: TBD
- StartingWave: Hierarchy -> LevelManager -> EnemySpawner -> Attributes-> Current Wave 
- BaseEnemies: " -> Attrbutes -> BaseEnemies
         
### Towers
- Range: Prefabs -> (turret)Handler -> TargetingRange
- BulletsPerSecond: Prefabs -> (turret)Handler -> bps
- Projectile: Prefabs -> (turret)Handler -> Bullet prefab
- Dmg: (turret)Handler.Shoot() -> SetTarget(target, dmg);
- BulletSpeed: Prefabs -> BulletHandler -> BulletSpeed
- Cost: Hierarchy -> LevelManager -> BuildManager -> Towers -> cost

### Enemy
- EnemySpeed: Prefab -> enemyMovement -> MoveSpeed
- Enemyhealth: Prefab -> healthHandler -> hitPoints
- Worth: Prefab -> healthHandler -> CurrencyWorth
- Dmg: Prefab -> healthHandler -> dmg
