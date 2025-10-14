using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Settings;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Sprites
{
    public class StickFigureSprite
    {
        private const int RUNNING_ANIMATION_FRAMES_NUMBER = 8;
        private const int IDLE_ANIMATION_FRAMES_NUMBER = 8;
        private const string SPRITE_FILES_RELATIVE_PATH =
            "SpriteTextures/StickFigureCharacterSprites2D/Fighter sprites/CombinedSprites";
        private const int PLAYER_SPEED = 6;
        private const double PLAYER_ANIMATION_SPEED = .1;
        private const float JUMPING_TIME = .5f;
        private const float JUMPING_VELOCITY = 400;
        private const float FALLING_SPEED = 250;

        private InputState _inputState;
        private InputAction _moveLeft;
        private InputAction _moveRight;
        private InputAction _jump;

        private Texture2D _runningTextures;
        private SoundEffect _runningSoundEffect;
        private SoundEffectInstance _runningSoundEffectInstance;
        private int _previousRunningState;
        private int _currentRunningState;
        private Rectangle[] _runningAnimationFrameSourceRectangles;

        private Texture2D _idleTextures;
        private int _previousIdleState;
        private int _currentIdleState;
        private Rectangle[] _idleAnimationFrameSourceRectangles;

        private double _animationTimer;

        private bool _isMoving;
        private bool _isJumping;
        private float _timeSinceJumpInput;
        private bool _flipped;

        private Vector2 _position;

        private BoundingRectangle _hitbox;
        private List<BoundingRectangle> _hitboxes;
        public BoundingRectangle Hitbox => _hitbox;

        public void Initalize(Settings.Settings settings)
        {
            _currentRunningState = 0;
            _currentIdleState = 0;
            _position = new Vector2(Constants.SCREEN_WIDTH / 2, 811);
            _inputState = new();
            _timeSinceJumpInput = 0;

            bool movementOnAWSD = (settings.KeyboardOptions == KeyboardOptions.MovementOnAWSD);
            _moveLeft = new InputAction([], [(movementOnAWSD) ? Keys.A : Keys.Left], false);
            _moveRight = new InputAction([], [(movementOnAWSD) ? Keys.D : Keys.Right], false);
            _jump = new InputAction([Buttons.A], [Keys.Space], true);
        }

        public void LoadContent(ContentManager content)
        {
            _idleTextures = content.Load<Texture2D>($"{SPRITE_FILES_RELATIVE_PATH}/idle");
            _runningTextures = content.Load<Texture2D>($"{SPRITE_FILES_RELATIVE_PATH}/run");

            _runningSoundEffect = content.Load<SoundEffect>("runningEffect");
            _runningSoundEffectInstance = _runningSoundEffect.CreateInstance();
            _runningSoundEffectInstance.Volume = .25f;
            _runningSoundEffectInstance.IsLooped = true;

            FillIdleAnimationFrameSourceRectangles();
            FillRunningAnimationFrameSourceRectangles();

            _position.X -= _idleAnimationFrameSourceRectangles[0].Width / 2;
            _hitbox.ChangePositionTo(_position);
            _hitbox.ChangeDimentions(
                _idleAnimationFrameSourceRectangles[0].Width,
                _idleAnimationFrameSourceRectangles[0].Height
            );
        }

        private void FillIdleAnimationFrameSourceRectangles()
        {
            _idleAnimationFrameSourceRectangles = new Rectangle[IDLE_ANIMATION_FRAMES_NUMBER];

            _idleAnimationFrameSourceRectangles[0] = new Rectangle(0, 0, 109, 167);
            _idleAnimationFrameSourceRectangles[1] = new Rectangle(110, 0, 110, 167);
            _idleAnimationFrameSourceRectangles[2] = new Rectangle(0, 168, 109, 172);
            _idleAnimationFrameSourceRectangles[3] = new Rectangle(110, 168, 110, 172);
            _idleAnimationFrameSourceRectangles[4] = new Rectangle(0, 340, 108, 167);
            _idleAnimationFrameSourceRectangles[5] = new Rectangle(109, 340, 111, 167);
            _idleAnimationFrameSourceRectangles[6] = new Rectangle(0, 507, 109, 173);
            _idleAnimationFrameSourceRectangles[7] = new Rectangle(110, 507, 110, 173);
        }

        private void FillRunningAnimationFrameSourceRectangles()
        {
            _runningAnimationFrameSourceRectangles = new Rectangle[RUNNING_ANIMATION_FRAMES_NUMBER];

            _runningAnimationFrameSourceRectangles[0] = new Rectangle(0, 0, 109, 166);
            _runningAnimationFrameSourceRectangles[1] = new Rectangle(110, 0, 124, 166);
            _runningAnimationFrameSourceRectangles[2] = new Rectangle(234, 0, 111, 166);
            _runningAnimationFrameSourceRectangles[3] = new Rectangle(345, 0, 111, 166);
            _runningAnimationFrameSourceRectangles[4] = new Rectangle(0, 167, 111, 173);
            _runningAnimationFrameSourceRectangles[5] = new Rectangle(112, 167, 109, 173);
            _runningAnimationFrameSourceRectangles[6] = new Rectangle(221, 167, 109, 173);
            _runningAnimationFrameSourceRectangles[7] = new Rectangle(330, 167, 109, 173);
        }

        public void Update(GameTime gameTime, List<BoundingRectangle> hitboxes)
        {
            _inputState.Update();
            _hitboxes = hitboxes;

            HandleRunningSoundEffect();

            MoveSprite(gameTime);
        }

        #region Update Helper Methods

        private void HandleRunningSoundEffect()
        {
            if (IsntMoving())
            {
                _isMoving = false;
                if (_runningSoundEffectInstance.State == SoundState.Playing)
                {
                    _runningSoundEffectInstance.Stop();
                }
                _currentRunningState = 0;
            }
            else
            {
                _isMoving = true;
                if (_runningSoundEffectInstance.State != SoundState.Playing)
                {
                    _runningSoundEffectInstance.Play();
                }
                _currentIdleState = 0;
            }
        }

        private void MoveSprite(GameTime gameTime)
        {
            Vector2 moveFromColliding = HowFarToMoveOutOfColliding();

            Vector2 controllerMovement = _inputState.HowMuchDidLeftStickMove(PlayerIndex.One);

            _position += controllerMovement * new Vector2(PLAYER_SPEED, 0);
            if (controllerMovement.X < 0)
                _flipped = true;
            else if (controllerMovement.X > 0)
                _flipped = false;

            if (_moveLeft.Occurred(_inputState, PlayerIndex.One, out PlayerIndex player))
            {
                _position += new Vector2(-PLAYER_SPEED, 0);
                _flipped = true;
            }
            if (_moveRight.Occurred(_inputState, PlayerIndex.One, out player))
            {
                _position += new Vector2(PLAYER_SPEED, 0);
                _flipped = false;
            }
            bool isFalling = IsFalling();

            if (
                !_isJumping
                && !isFalling
                && _jump.Occurred(_inputState, PlayerIndex.One, out player)
            )
            {
                _isJumping = true;
            }

            if (_isJumping)
            {
                _timeSinceJumpInput += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _position.Y -= JUMPING_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timeSinceJumpInput > JUMPING_TIME)
                {
                    _isJumping = false;
                    _timeSinceJumpInput = 0;
                }
            }

            _position += moveFromColliding;
            _hitbox.ChangePositionTo(_position);

            if (isFalling)
                _position.Y += FALLING_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private bool IsFalling() // fix so checks all hitboxes not just stops at false
        {
            foreach (BoundingRectangle hitbox in _hitboxes)
            {
                if (CollisionHelper.IsCharacterStandingOnAnEntity(_hitbox, hitbox))
                {
                    return false;
                }
            }

            return true;
        }

        private Vector2 HowFarToMoveOutOfColliding()
        {
            foreach (BoundingRectangle hitbox in _hitboxes)
            {
                if (!Hitbox.Equals(hitbox) && CollisionHelper.ItemsCollide(Hitbox, hitbox))
                {
                    return CollisionHelper.MoveEntityAOutOfEntityB(Hitbox, hitbox);
                }
            }

            return Vector2.Zero;
        }

        private bool IsntMoving()
        {
            Vector2 controllerMovement = _inputState.HowMuchDidLeftStickMove(PlayerIndex.One);
            return (controllerMovement.X == 0 && controllerMovement.Y == 0)
                && (
                    !_moveLeft.Occurred(_inputState, PlayerIndex.One, out PlayerIndex player)
                    && !_moveRight.Occurred(_inputState, PlayerIndex.One, out player)
                );
        }

        #endregion

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            SpriteEffects spriteEffects =
                (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if (_isMoving)
            {
                if (_animationTimer > PLAYER_ANIMATION_SPEED)
                {
                    Rectangle runningState = _runningAnimationFrameSourceRectangles[
                        _currentRunningState
                    ];
                    _previousRunningState = _currentRunningState;
                    _currentRunningState =
                        (_previousRunningState < RUNNING_ANIMATION_FRAMES_NUMBER - 1)
                            ? _currentRunningState + 1
                            : 0;
                    _hitbox.ChangeDimentions(
                        (float)(runningState.Width * 0.75),
                        (float)(runningState.Height * 0.75)
                    );
                    _animationTimer -= PLAYER_ANIMATION_SPEED;
                }
                spriteBatch.Draw(
                    _runningTextures,
                    _position,
                    _runningAnimationFrameSourceRectangles[_currentRunningState],
                    Color.Black,
                    0,
                    new Vector2(0, 0),
                    .75f,
                    spriteEffects,
                    0
                );
            }
            else
            {
                if (_animationTimer > PLAYER_ANIMATION_SPEED)
                {
                    Rectangle idleState = _idleAnimationFrameSourceRectangles[_currentIdleState];
                    _previousIdleState = _currentIdleState;
                    _currentIdleState =
                        (_previousIdleState < IDLE_ANIMATION_FRAMES_NUMBER - 1)
                            ? _currentIdleState + 1
                            : 0;
                    _hitbox.ChangeDimentions(
                        (float)(idleState.Width * 0.75),
                        (float)(idleState.Height * 0.75)
                    );
                    _animationTimer -= PLAYER_ANIMATION_SPEED;
                }
                spriteBatch.Draw(
                    _idleTextures,
                    _position,
                    _idleAnimationFrameSourceRectangles[_currentIdleState],
                    Color.Black,
                    0,
                    new Vector2(0, 0),
                    .75f,
                    spriteEffects,
                    0
                );
            }
        }
    }
}
