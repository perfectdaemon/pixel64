using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpPixel.Engine;
using SharpPixel.Game.GameObjects;
using SharpPixel.Game.Interfaces;

/*
 TODO:
 * Dynamic frequency spawn 
 */
namespace SharpPixel.Game
{
    public class GameScene : Scene, IGameScene
    {
        private IMainMenu mainMenu;
        private IGameOverMenu gameOverMenu;

        private List<GameObject>
            allGameObjects = new List<GameObject>(),

            civilians = new List<GameObject>(),
            obstacles = new List<GameObject>(),
            collectables = new List<GameObject>();

        private ActionManager actionManager = new ActionManager();

        private int oldDistance, distance, score;
        private double distanceD, speed;
        private bool isGameOver;
        private int livesCount;
        private double spawnTimer, spawnPeriod, spawnFuelTimer;

        private Random random = new Random();

        private Bitmap carBitmap, carShadowBitmap, fuelBitmap, roadSignBitmap, smokeBitmap, civilianCarBitmap, livesBitmap;

        private Player player;
        private bool blinking;
        private double blinkingTimer;

        private Pen pen = new Pen(Utility.Yellow, 1);
        private SolidBrush brush = new SolidBrush(Utility.GrayHard);

        private void CleanInactiveObjects()
        {
            allGameObjects.RemoveAll(g => !g.Active);
            civilians.RemoveAll(g => !g.Active);
            obstacles.RemoveAll(g => !g.Active);
            collectables.RemoveAll(g => !g.Active);
        }

        private void MoveIfNeeded(GameObject gameObject)
        {
            switch (gameObject.Type)
            {
                case GameObjectType.Obstacle:                
                case GameObjectType.Collectable:
                case GameObjectType.Civilian:
                    gameObject.Location.Y += distance - oldDistance;
                    break;
            }
        }

        private void CheckCollisionWithPlayer(GameObject gameObject)
        {
            if (gameObject == player)
                return;
            switch (gameObject.Type)
            {
                case GameObjectType.Collectable:                
                    if (gameObject.DoesCollideWith(player))
                    {
                        gameObject.OnCollect(player);
                        if (gameObject is Life)
                            sound.Play(Sounds.LifePickup);
                        else
                            sound.Play(Sounds.Pickup);
                    }
                    break;

                case GameObjectType.Civilian:                
                case GameObjectType.Obstacle:
                    if (gameObject.DoesCollideWith(player) && !blinking)
                    {
                        sound.Play(Sounds.Hit);
                        actionManager
                            .AddToQueue(
                                () =>
                                {
                                    blinking = true;
                                    blinkingTimer = 0;
                                    livesCount -= 1;
                                    if (livesCount < 0)
                                        isGameOver = true;
                                })
                            .AddToQueue(
                                (dt) =>
                                {
                                    blinkingTimer += dt;
                                    if (blinkingTimer > 0.1)
                                    {
                                        blinkingTimer = 0;
                                        player.Visible = !player.Visible;
                                    }
                                },
                                2.0d)
                            .AddToQueue(
                                () =>
                                {
                                    player.Visible = true;
                                    blinking = false;
                                    blinkingTimer = -1;
                                });
                    }
                    break;                
            }
        }

        private void SpawnFuel()
        {
            int lane = random.Next(Utility.LANES_COUNT);
            var position = new Point(4 + lane * Utility.LaneWidth, -40);

            var fuel = new Fuel(fuelBitmap, position);
            collectables.Insert(collectables.Count, fuel);
            allGameObjects.Add(fuel);
        }

        private void SpawnRoadSign(Point location)
        {
            var roadSign = new RoadSign(roadSignBitmap, location);

            obstacles.Insert(obstacles.Count, roadSign);
            allGameObjects.Add(roadSign);
        }

        private void SpawnCivialian(Point location, int speed)
        {
            var civilian = new Civilian(civilianCarBitmap, carShadowBitmap, smokeBitmap, location);
            civilian.Speed = speed;

            allGameObjects.Add(civilian);
            civilians.Add(civilian);
        }

        private void SpawnLife(Point location)
        {
            var life = new Life(livesBitmap, location);

            collectables.Add(life);
            allGameObjects.Add(life);
        }

        private void SpawnSomething()
        {
            int spawnType = random.Next(100);
            int lane = random.Next(Utility.LANES_COUNT);
            var position = new Point(4 + lane * Utility.LaneWidth, -40);
            if (spawnType < 40)
                SpawnRoadSign(position);
            else if (spawnType < 95)
                SpawnCivialian(position, 6 + random.Next(5));
            else
                SpawnLife(position);
        }

        private void RenderRoadMarking()
        {
            pen.Color = Utility.Yellow;
            int yOffset = distance % Utility.FIELD_SIZE - Utility.FIELD_SIZE;
            int xOffset = Utility.FIELD_SIZE / Utility.LANES_COUNT;
            for (int i = 0; i < Utility.LANES_COUNT - 1; ++i)
            {
                for (int j = 0; j < 16; ++j)
                    surface.BackGraphics.DrawLine(pen, xOffset * (i + 1), yOffset + j * 8, xOffset * (i + 1), yOffset + j * 8 + 4);
            }

            //borders
            pen.Color = Utility.GrayHard;
            surface.BackGraphics.DrawLine(pen, 0, 0, 0, Utility.FIELD_SIZE);
            surface.BackGraphics.DrawLine(pen, Utility.FIELD_SIZE - 1, 0, Utility.FIELD_SIZE - 1, Utility.FIELD_SIZE);
        }

        private void RenderGUI()
        {
            // Lives
            for (int i = 0; i < livesCount; ++i)
                surface.RenderBitmap(livesBitmap, 2 + 4 * i, 1);

            // Scores
            surface.RenderNumber(score, 2, Utility.FIELD_SIZE - 10, -1);

            // Fuel level
            pen.Color = Utility.RedHard;
            surface.BackGraphics.DrawRectangle(pen, new Rectangle(37, 58, 24, 4));
            brush.Color = Utility.RedLight;
            int length = (int)(23 * player.FuelLevel / Utility.FUEL_MAX);
            if (length > 0)
                surface.BackGraphics.FillRectangle(brush, new Rectangle(38, 59, length, 3));
        }

        public void Initialize(IMainMenu mainMenu, IGameOverMenu gameOverMenu)
        {
            this.mainMenu = mainMenu;
            this.gameOverMenu = gameOverMenu;
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    controller.SwitchTo(mainMenu);
                    break;
            }
        }

        public override void LoadResources()
        {
            carBitmap = ResourceManager.GetBitmapResource("Car");
            carShadowBitmap = ResourceManager.GetBitmapResource("CarShadow");
            fuelBitmap = ResourceManager.GetBitmapResource("Fuel");
            roadSignBitmap = ResourceManager.GetBitmapResource("Roadsign");
            smokeBitmap = ResourceManager.GetBitmapResource("smoke");
            civilianCarBitmap = ResourceManager.GetBitmapResource("CivilianCar");
            livesBitmap = ResourceManager.GetBitmapResource("Lives");
        }

        public override void Render()
        {
            // Background
            surface.RenderBackground(Utility.GrayLight);

            this.RenderRoadMarking();

            // GameObjects            
            foreach (var gameObject in collectables)
                gameObject.Render(surface);
            foreach (var gameObject in obstacles)
                gameObject.Render(surface);
            foreach (var gameObject in civilians)
                gameObject.Render(surface);

            player.Render(surface);

            // GUI
            this.RenderGUI();

            surface.SwapBuffers();
        }

        public override void Update(double dt)
        {
            actionManager.Update(dt);
            distanceD += speed * dt;
            speed += Utility.SPEED_INC * dt;
            speed = Utility.Clamp(speed, 0, Utility.SPEED_MAX);

            oldDistance = distance;
            distance = (int)Math.Floor(distanceD);
            score = distance / 10;

            if (spawnTimer > 0)
                spawnTimer -= dt;
            else
            {
                SpawnSomething();
                spawnTimer = spawnPeriod;
                spawnPeriod = Utility.Clamp(spawnPeriod - Utility.SPAWN_PERIOD_DEC, Utility.SPAWN_PERIOD_MIN, Utility.SPAWN_PERIOD_START);
            }

            if (spawnFuelTimer > 0)
                spawnFuelTimer -= dt;
            else
            {
                SpawnFuel();
                spawnFuelTimer = Utility.SPAWN_FUEL_PERIOD;
            }

            CleanInactiveObjects();

            foreach (var gameObject in allGameObjects)
            {
                gameObject.Update(dt);
                MoveIfNeeded(gameObject);
                CheckCollisionWithPlayer(gameObject);
            }

            Render();

            if (player.FuelLevel <= 0 || isGameOver)
            {
                gameOverMenu.SetScore(score);
                controller.SwitchTo(gameOverMenu);
            }
        }

        public override void Reset()
        {
            oldDistance = 0;
            distance = 0;
            score = 0;
            distanceD = 0.0d;
            speed = Utility.SPEED_START;
            spawnTimer = 0.0d;
            spawnPeriod = Utility.SPAWN_PERIOD_START;
            spawnFuelTimer = 0.0d;

            isGameOver = false;
            blinking = false;
            blinkingTimer = -1;

            livesCount = Utility.LIVES_START;

            allGameObjects.Clear();
            civilians.Clear();
            obstacles.Clear();
            collectables.Clear();

            actionManager.ClearAll();

            player = new Player(carBitmap, carShadowBitmap, smokeBitmap, new Point(19, 42));
            player.OnLifeCollect = () =>
            {
                livesCount = Utility.Clamp(livesCount + 1, 0, Utility.LIVES_MAX);
            };
            allGameObjects.Add(player);
        }
    }
}
