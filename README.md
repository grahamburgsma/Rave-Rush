# Rave-Rush
Small game created for COSC 4P98 at Brock University

### Controls
- N : Nitro
- R : Reset car
- B : Ball camera
- J : Jump
- J + direction : In the air is double jump, spin towards the arrows pressed
- (WSAD) or (Arrow Keys) :  Driving

### Gameplay
#### Overview
The gameplay revolves around trying to put the ball into the specified net. When the game starts, there is a countdown and then the user is allowed to move. The game tells you where to score (As seen on the top, or the colour of the arrow on top of the car).Once the user scores, the ball is returned back to the center, and another random colour is chosen as where to score next. When the timer reaches 0, the game pauses and all motion is slowed and user input is disallowed. The game gives you 5 seconds to look at your stats before returning to the main menu.
The user can use the ball camera (press B) or use the arrow on top of the car to locate the ball in the scene. The car can also jump and flip in various directions in order to help with hitting the ball. There are obstacles around the scene which affect the car (Spin and jump) or the ball (blows it up). When the user hits the ball hard enough, a sound and custom effect will occur.

#### Features
- 3 Different obstacles
  - Spin
  - Jump
  - Destroy Ball
- Car
  - Created in blender
  - Custom physics
  - Can jump
  - Can jump and flip in specified direction
  - Nitro boost
  - Headlights
  - Wheels
  - Working suspension
  - Custom model and texture
  - Arrow which points to ball
  - Can climb walls
  - Sounds
    - Engine
    - Collision
      - Ball
      - Environment
  - Working collision
- Goal Mechanics
  - Custom triggers to handle goals
  - Explosion effect when the ball is scored
  - Ball respawns at center after each goal
  - Scoring custom sound
  - Scoring custom text animation
- Two different camera modes
  - Follow ball camera
  - Follow car camera
- Ball object
  - Textured
  - Uses gravity
  - Bounce physics
- Main Menu
  - Different modes
  - Different game lengths
  - Custom UI
  - Main menu changes based on where the mouse is
- UI
  - Custom UI
  - Custom image overlaid on canvas for HUD
  - Displays time left
- Rave Mode
  - Has different sounds
    - Background noise
      - Had to tweak to loop properly
    - Engine noise
      - More arcadey noise
  - Has custom lights which spin
  - Custom lights determine the colours of the goals rather than static sides
- Custom arena
  - No coloured sides
  - Different ground texture


