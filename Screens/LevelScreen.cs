using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;
using Parkour2D360.Screens.LevelScreens;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class LevelScreen : PlayableGameScreen
    {
        protected const int BASE_PLATFORM_Y = Constants.SCREEN_HEIGHT - 100;
        protected Platform BASE_PLATFORM = new Platform(
            new BoundingRectangle(0, Constants.SCREEN_HEIGHT - 100, Constants.SCREEN_WIDTH, 10),
            Color.Black,
            true
        );

        protected bool levelCompleted
        {
            get
            {
                foreach (RotatableGameScreenSide side in _gamescreenSides)
                {
                    foreach (CollectableTriangle collectable in side.Collectables)
                    {
                        if (!collectable.isCollected)
                            return false;
                    }
                }
                return true;
            }
        }

        protected string _levelName = "";
        protected int _levelNumber = -1;
        protected float _levelNameDisplayTimer = 0;
        protected const float LEVEL_NAME_DISPLAY_TIME = 3.0f;

        protected bool _isSideSliding = false;
        protected int _slideFromIndex = -1;
        protected int _slideToIndex = 0;
        protected int _slideDirection = 1; // -1 for left, 1 for right
        protected float _slideDuration = .5f;
        protected float _slideTimer = 0f;
        protected float _slideProgress =>
            MathHelper.Clamp(_slideTimer / Math.Max(0.0001f, _slideDuration), 0f, 1f);

        public LevelScreen()
        {
            Initialize();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(
            GameTime gameTime,
            bool otherScreenHasFocus,
            bool coveredByOtherScreen
        )
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            UpdateCollectables(gameTime);

            CheckCollidingWithCollectables();

            _previousGameScreenSide = _currentGameScreenSide;

            HandleRotatingSides((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (_currentGameScreenSide < 0 || _currentGameScreenSide >= _gamescreenSides.Count)
                throw new System.IndexOutOfRangeException();

            HandleFallingOffScreen();

            UpdateLevelNameTimer((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 offset = GetTransitionOffset();

            if (_isSideSliding)
            {
                Vector2 offsetFrom =
                    new Vector2(-_slideDirection * _slideProgress * Constants.SCREEN_WIDTH, 0f)
                    + offset;
                Vector2 offsetTo =
                    new Vector2(
                        _slideDirection * (1f - _slideProgress) * Constants.SCREEN_WIDTH,
                        0f
                    ) + offset;

                _spriteBatch.Begin();

                DrawLevelName();

                DrawLevelPlatforms(_slideFromIndex, offsetFrom);
                DrawLevelPlatforms(_slideToIndex, offsetTo);
                _spriteBatch.End();

                DrawCollectables();
            }
            else
            {
                _spriteBatch.Begin();

                DrawLevelName();

                DrawLevelPlatforms(_currentGameScreenSide, Vector2.Zero);
                _spriteBatch.End();

                DrawCollectables();
            }
            //if (ShouldInvertPlayersXCoordinate())
            //_stickFigureSprite.AddToPosition(new Vector2(offset.X, 0));
            base.Draw(gameTime);
        }

        private void HandleRotatingSides(float elapsedGameTime)
        {
            PlayerIndex player;
            if (!_isSideSliding)
            {
                if (_rotateLeft.Occurred(_inputState, PlayerIndex.One, out player))
                {
                    int targetSide =
                        (_currentGameScreenSide > 0)
                            ? _currentGameScreenSide - 1
                            : _gamescreenSides.Count - 1;
                    StartSideSlide(targetSide);
                }
                else if (_rotateRight.Occurred(_inputState, PlayerIndex.One, out player))
                {
                    int targetSide =
                        (_currentGameScreenSide < _gamescreenSides.Count - 1)
                            ? _currentGameScreenSide + 1
                            : 0;
                    StartSideSlide(targetSide);
                }
            }
            else
            {
                _slideTimer += elapsedGameTime;
                if (_slideTimer >= _slideDuration)
                {
                    _isSideSliding = false;
                    _currentGameScreenSide = _slideToIndex;
                    _slideTimer = 0f;
                }
            }
        }

        private void HandleFallingOffScreen()
        {
            if (_stickFigureSprite.Hitbox.Y > Constants.SCREEN_HEIGHT)
            {
                ScreenManager.AddScreen(new FailureScreen(_levelNumber), ControllingPlayer);
                ScreenManager.RemoveScreen(this);
                _stickFigureSprite.StopSoundEffects();
            }
        }

        protected void DrawLevelPlatforms(int sideIndex, Vector2 offset)
        {
            DrawNonCollidableLevelPlatforms(sideIndex, offset);
            DrawCollidableLevelPlatforms(sideIndex, offset);
        }

        protected void DrawCollidableLevelPlatforms(int sideIndex, Vector2 offset)
        {
            if (_gamescreenSides?[sideIndex]?.CollidablePlatforms == null)
                return;

            foreach (Platform platform in _gamescreenSides[sideIndex].CollidablePlatforms)
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)(platform.Location.X + offset.X),
                        (int)(platform.Location.Y + offset.Y),
                        (int)platform.Location.Width,
                        (int)platform.Location.Height
                    ),
                    platform.Color
                );
            }
        }

        protected void DrawNonCollidableLevelPlatforms(int sideIndex, Vector2 offset)
        {
            if (_gamescreenSides?[sideIndex]?.NonCollidablePlatforms == null)
                return;
            Vector2 usedOffset = offset;
            if (_isSideSliding)
            {
                usedOffset = new Vector2(-offset.X, offset.Y);
            }
            foreach (Platform platform in _gamescreenSides[sideIndex].NonCollidablePlatforms)
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)(platform.Location.X + usedOffset.X),
                        (int)(platform.Location.Y + usedOffset.Y),
                        (int)platform.Location.Width,
                        (int)platform.Location.Height
                    ),
                    platform.Color
                );
            }
        }

        protected void UpdateLoadNextLevel(LevelScreen nextLevel)
        {
            if (levelCompleted)
            {
                ScreenManager.AddScreen(nextLevel, ControllingPlayer);
                ScreenManager.RemoveScreen(this);
                _stickFigureSprite.StopSoundEffects();
            }
        }

        protected void UpdateLevelNameTimer(float elapsedSeconds)
        {
            if (_levelNameDisplayTimer < LEVEL_NAME_DISPLAY_TIME)
            {
                _levelNameDisplayTimer += elapsedSeconds;
            }
        }

        protected void DrawLevelName()
        {
            if (_levelNameDisplayTimer < LEVEL_NAME_DISPLAY_TIME)
            {
                _spriteBatch.DrawString(
                    ScreenManager.Font,
                    _levelName,
                    new Vector2((Constants.SCREEN_WIDTH / 2) - 100, 200),
                    Color.Black
                );
            }
        }

        protected void CheckCollidingWithCollectables()
        {
            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                if (
                    collectable.isCollideable
                    && CollisionHelper.ItemsCollide(_stickFigureSprite.Hitbox, collectable.Hitbox)
                )
                {
                    collectable.Collect();
                }
            }
        }

        protected void UpdateCollectables(GameTime gameTime)
        {
            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                collectable.Update(gameTime);
            }
        }

        protected void DrawCollectables()
        {
            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.None;

            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                if (!collectable.isCollected)
                    collectable.Draw();
            }

            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        protected void StartSideSlide(int targetSide, float duration = .5f)
        {
            if (_gamescreenSides.Count == 0)
                return;
            if (_isSideSliding)
                return;
            if (targetSide < 0 || targetSide >= _gamescreenSides.Count)
                return;

            _isSideSliding = true;
            _slideFromIndex = _currentGameScreenSide;
            _slideToIndex = targetSide;
            _slideTimer = 0f;
            _slideDuration = Math.Max(0.01f, duration);

            int count = _gamescreenSides.Count;
            int difference = (_slideToIndex - _slideFromIndex + count) % count;
            if (difference == 1)
                _slideDirection = 1;
            else if (difference == count - 1)
                _slideDirection = -1;
            else
            {
                _slideDirection = (targetSide > _slideFromIndex) ? 1 : -1;
            }

            _previousGameScreenSide = _currentGameScreenSide;
        }
    }
}
