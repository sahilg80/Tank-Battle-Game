Tank Shoot Multiplayer Game

Welcome to the Tank Shoot Multiplayer Game repository! This project offers an exciting tank shooting experience where players can engage in thrilling multiplayer battles or challenge themselves against AI opponents.

Branches:

    - main: Offers the single-player version of the Tank Shoot game.
    - multiplayer-tank-shoot: Provides the multiplayer mode where remote users can connect and battle against each other in real-time.
    
Features:

Multiplayer Mode:

    - Real-time multiplayer battles powered by Node.js and Socket.IO.
    - Seamless synchronization of player movements, health, score, shooting, and countdown timer among all connected players.
    - Deployed backend server on Render.com for efficient player management.

Single Player Mode:

    - Engaging AI opponents for solo play.
    - Enjoy the same immersive tank shooting experience even when offline.

Gameplay:

    - Choose your tank and enter your name to start the battle.
    - Score maximum kills before the countdown timer runs out to emerge victorious.
    - Dynamic and responsive tank controls for an immersive gaming experience.

Architecture:

    - Followed the MVC design architecture in Unity for efficient entity management.
    - Clean and organized codebase for easy maintenance and scalability.
    - Implemented a GameService as a service locator to register and access required services globally.

Optimization:

    - Implemented object pooling to manage frequent spawning and destruction of entities, ensuring efficient memory usage.
    - Leveraged MVC design architecture for seamless communication between multiple entities.

