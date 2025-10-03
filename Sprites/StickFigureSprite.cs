using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Parkour2D360.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Sprites
{
    public class StickFigureSprite
    {
        private const int RUNNING_ANIMATION_FRAMES_NUMBER = 8;
        private const int IDLE_ANIMATION_FRAMES_NUMBER = 8;
        private const string SPRITE_FILES_RELATIVE_PATH = "SpriteTextures/StickFigureCharacterSprites2D/Fighter sprites/CombinedSprites";
        private const int PLAYER_SPEED = 6;
        private const double PLAYER_ANIMATION_SPEED = .1;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;

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
        private bool _isColliding;

        private int _fallingSpeed = 5;

        private Vector2 _position;
        private bool _flipped;
        private BoundingRectangle _hitbox;
        public BoundingRectangle Hitbox => _hitbox;

        public void Initalize()
        {
            _currentRunningState = 0;
            _currentIdleState = 0;
            _position = new Vector2(0, 500);//811
            _hitbox.ChangePositionTo(_position);

        }

        public void LoadContent(ContentManager content)
        {
            _idleTextures = content.Load<Texture2D>($"{SPRITE_FILES_RELATIVE_PATH}/idle");
            _runningTextures = content.Load<Texture2D>($"{SPRITE_FILES_RELATIVE_PATH}/run");

            _runningSoundEffect = content.Load<SoundEffect>("runningEffect");
            _runningSoundEffectInstance = _runningSoundEffect.CreateInstance();
            _runningSoundEffectInstance.IsLooped = true;

            FillIdleAnimationFrameSourceRectangles();
            FillRunningAnimationFrameSourceRectangles();

            _hitbox.ChangeDimentions(_idleAnimationFrameSourceRectangles[0].Width, _idleAnimationFrameSourceRectangles[0].Height);
        }

        private void FillIdleAnimationFrameSourceRectangles()
        {
            _idleAnimationFrameSourceRectangles = new Rectangle[IDLE_ANIMATION_FRAMES_NUMBER];

            _idleAnimationFrameSourceRectangles[0] = new Rectangle(0,0, 109, 167);
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

        public void Update(GameTime gameTime, List<BoundingRectangle> _hitboxes)
        {
            _gamePadState = GamePad.GetState(0);
            _keyboardState = Keyboard.GetState();

            Vector2 moveFromColliding = new Vector2(0, 0);

            _isColliding = false;
            foreach (BoundingRectangle hitbox in _hitboxes)
            {
                if (!Hitbox.Equals(hitbox) && CollisionHelper.ItemsCollide(Hitbox, hitbox))
                {
                    _isColliding = true;
                    moveFromColliding = CollisionHelper.MoveEntityAOutOfEntityB(Hitbox, hitbox);
                }
            }

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

            if (!_isColliding)
            {
                _position += _gamePadState.ThumbSticks.Left * new Vector2(PLAYER_SPEED, -PLAYER_SPEED);
                if (_gamePadState.ThumbSticks.Left.X < 0) _flipped = true;
                else if (_gamePadState.ThumbSticks.Left.X > 0) _flipped = false;

                if (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.W)) _position += new Vector2(0, -PLAYER_SPEED);
                if (_keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.S)) _position += new Vector2(0, PLAYER_SPEED);
                if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.A))
                {
                    _position += new Vector2(-PLAYER_SPEED, 0);
                    _flipped = true;
                }
                if (_keyboardState.IsKeyDown(Keys.Right) || _keyboardState.IsKeyDown(Keys.D))
                {
                    _position += new Vector2(PLAYER_SPEED, 0);
                    _flipped = false;
                }
                _hitbox.ChangePositionTo(_position);
            }
            else
            {
                _position += moveFromColliding;
                _hitbox.ChangePositionTo(_position);
            }
            bool falling = true;
            foreach(BoundingRectangle hitbox in _hitboxes)
            {
                if (CollisionHelper.IsCharacterStandingOnAnEntity(_hitbox, hitbox))
                {
                    falling = false; break;
                }
            }
            if (falling) _position.Y += _fallingSpeed;
        }

        private bool IsntMoving() // change to inputaction
        {
            return (_gamePadState.ThumbSticks.Left.X == 0 && _gamePadState.ThumbSticks.Left.Y == 0) &&
                (
                _keyboardState.IsKeyUp(Keys.Up) && _keyboardState.IsKeyUp(Keys.W) &&
                _keyboardState.IsKeyUp(Keys.Down) && _keyboardState.IsKeyUp(Keys.S) &&
                _keyboardState.IsKeyUp(Keys.Left) && _keyboardState.IsKeyUp(Keys.A) &&
                _keyboardState.IsKeyUp(Keys.Right) && _keyboardState.IsKeyUp(Keys.D)
                );
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if (_isMoving)
            {
                if (_animationTimer > PLAYER_ANIMATION_SPEED)
                {
                    Rectangle runningState = _runningAnimationFrameSourceRectangles[_currentRunningState];
                    _previousRunningState = _currentRunningState;
                    _currentRunningState = (_previousRunningState < RUNNING_ANIMATION_FRAMES_NUMBER-1) ? _currentRunningState + 1 : 0;
                    _hitbox.ChangeDimentions((float)(runningState.Width * 0.75), (float)(runningState.Height * 0.75));
                    _animationTimer -= PLAYER_ANIMATION_SPEED;
                }
                spriteBatch.Draw(_runningTextures, _position, _runningAnimationFrameSourceRectangles[_currentRunningState], Color.Black, 0, new Vector2(0, 0), .75f, spriteEffects, 0);
            }
            else
            {
                if (_animationTimer > PLAYER_ANIMATION_SPEED)
                {
                    Rectangle idleState = _idleAnimationFrameSourceRectangles[_currentIdleState];
                    _previousIdleState = _currentIdleState;
                    _currentIdleState = (_previousIdleState < IDLE_ANIMATION_FRAMES_NUMBER-1) ? _currentIdleState + 1 : 0;
                    _hitbox.ChangeDimentions((float)(idleState.Width * 0.75), (float)(idleState.Height * 0.75));
                    _animationTimer -= PLAYER_ANIMATION_SPEED;
                }
                spriteBatch.Draw(_idleTextures, _position, _idleAnimationFrameSourceRectangles[_currentIdleState], Color.Black, 0, new Vector2(0, 0), .75f, spriteEffects, 0);
            }
        }

    }
}
