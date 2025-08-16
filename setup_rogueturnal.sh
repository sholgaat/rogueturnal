#!/bin/bash

# Rogueturnal project setup script
# Creates folders, initializes git, adds placeholder scripts

echo ">>> Setting up Rogueturnal project structure..."

# Create folder structure
mkdir -p Assets/{Art/{Sprites,Tilesets},Audio/{Music,SFX},Prefabs,Scenes,Scripts/{Player,Enemies,Weapons,Systems},UI/{HUD,Menus}}

# Create basic README
echo "# Rogueturnal" > README.md

# Initialize git repository
git init
echo "Library/
Temp/
Obj/
Build/
Builds/
Logs/
UserSettings/
*.csproj
*.unityproj
*.sln
*.suo
*.tmp
*.user
*.userprefs
*.pidb
*.booproj
*.svd
*.pdb
*.mdb
*.opendb
*.VC.db
*.pidb.meta
*.pdb.meta
*.mdb.meta
.DS_Store
.idea/
.vscode/
" > .gitignore

git add .
git commit -m "Initial project structure for Rogueturnal"

# Create placeholder C# scripts
cat <<EOL > Assets/Scripts/Player/PlayerController.cs
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("PlayerController initialized");
    }

    void Update()
    {
        // TODO: Add movement logic
    }
}
EOL

cat <<EOL > Assets/Scripts/Systems/CurrencySystem.cs
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public int Aetherion = 0;        // normal currency
    public int ElysiumShards = 0;    // premium currency

    public void AddAetherion(int amount)
    {
        Aetherion += amount;
        Debug.Log("Aetherion: " + Aetherion);
    }

    public void AddElysiumShards(int amount)
    {
        ElysiumShards += amount;
        Debug.Log("Elysium Shards: " + ElysiumShards);
    }
}
EOL

cat <<EOL > Assets/Scripts/Systems/IdleSystem.cs
using UnityEngine;

public class IdleSystem : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Idle system initialized");
    }

    void Update()
    {
        // TODO: Handle idle resource generation
    }
}
EOL

echo ">>> Rogueturnal setup complete!"

