# AI ScavengAR Hunt
AR Scavenger Hunt for the University of Pennsylvania

## Overview
This goal of this application is to expose students and faculty to the wonders of Artificial Intelligence through an Augmented Reality scavenger hunt. 

## Requirements
A smartphone or tablet will be required to run this application.
- An iPhone or iPad running iOS9 or higher
- An Android running 4.4 or higher

## Development
### 2/11/18
One of the goals for today was to be able to manipulate objects that get projected through AR. I was able to find an asset on the Unity store called [Lean Touch](https://assetstore.unity.com/packages/tools/input-management/lean-touch-30111) that does just this and more. At this current state, I can move objects and scale them around the AR marker.

Here's a screenshot of the current state. Users can click on as many cubes as they want and move/scale them to their liking.

![Floating Cubes](Images/floatingCubes.png)

Build: MoveObjects1

### 2/12/18
Today I was able to implement a basic inventory UI and multiple targets. This is the basis of the game. Each marker will have a different object or video associated with it for the player to collect. What I want to get working is to make the objects more interactive. The objects can move and scale based on the yesterday's work. To combine that with the UI requires double clicking, which I wasn't able to figure yet.
![Targets](Images/targets.png)

When users collect items, they'll show up in the inventory. Users can access this at any time by clicking the inventory button on the top right and exit with the close button. Something to look forward to with this is bringing the objects back into the scene and playing around with it. If we go with building the robot route, this might have to come into play or an animation will play snapping the piece to the robot. 
![Targets](Images/inventory.png)

Bugs
- *Double Click* - If you move the cube and then deselect, it wont' select again.

Todo
- Double Click
- Video

Build: MoveObjects2, MoveObjects3

## Resources
- [Unity 2017.3](https://unity3d.com/)
- [Vuforia](https://www.vuforia.com/)
- [Lean Touch](https://assetstore.unity.com/packages/tools/input-management/lean-touch-30111)