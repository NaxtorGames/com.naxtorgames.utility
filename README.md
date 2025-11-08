# Utilities Package

## Installation

### Via Unity Package Manager
- Follow [these](https://docs.unity3d.com/Manual/upm-ui-giturl.html) steps and by step 4 paste "https://github.com/NaxtorGames/com.naxtorgames.utility.git".

### Via manifest.json
1. Open '**<path_to_project>/Packages/manifest.json**'
2. Paste 
```json
"com.naxtorgames.utility": "https://github.com/NaxtorGames/com.naxtorgames.utility.git"
```
In the projects dependencies 
```json
{
  "dependencies": {
    ...
  }
}
```
3. Save '**manifest.json**' and return to Unity.

### With ZIP
1. Download this code as Zip.
2. Extract the Zip at "**<path_to_project>/Packages/com.naxtorgames.utility**>"
3. Return to Unity and let it compile.