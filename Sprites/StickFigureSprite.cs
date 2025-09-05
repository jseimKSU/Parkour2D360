using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Sprites
{
    public class StickFigureSprite
    {
        private const int MAX_RUN_INT_VALUE = 6;
        private const int MAX_IDLE_INT_VALUE = 7;
        private const string SPRITE_FILE_RELATIVE_PATH = "SpriteTextures/StickFigureCharacterSprites2D/Fighter sprites/";
        private const int PLAYER_SPEED = 6;
        private const double ANIMATION_SPEED = .1;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;

        private Texture2D[] _runningTextures = new Texture2D[MAX_RUN_INT_VALUE+1];
        private int _previousRunningState;
        private int _currentRunningState;

        private Texture2D[] _idleTextures = new Texture2D[MAX_IDLE_INT_VALUE+1];
        private int _previousIdleState;
        private int _currentIdleState;
        private bool _isMoving;
        private bool _isColliding;

        private Vector2 _position;
        private bool _flipped;
        private BoundingRectangle _hitbox;
        private double _animationTimer;
        public BoundingRectangle Hitbox => _hitbox;

        public void Initalize()
        {
            _currentRunningState = 0;
            _currentIdleState = 0;
            _position = new Vector2(-130, 651);
            _hitbox.ChangePositionTo(_position);
            _hitbox.ChangeDimentions((float)(512 * .75), (float)(512 * .75));
        }

        public void LoadContent(ContentManager content)
        {
            // have to either combine all the needed sprites to one file or Load based on some calculation with gameTime
            for (int i = 0; i <= MAX_RUN_INT_VALUE; i++)
            {
                _runningTextures[i] = content.Load<Texture2D>($"{SPRITE_FILE_RELATIVE_PATH}Run/run{i + 1}");
            }
            for (int i = 0;i <= MAX_IDLE_INT_VALUE; i++)
            {
                _idleTextures[i] = content.Load<Texture2D>($"{SPRITE_FILE_RELATIVE_PATH}Idle/idle{i}");
            }
        }

        public void Update(GameTime gameTime, List<BoundingRectangle> _hitboxes)
        {
            _gamePadState = GamePad.GetState(0);
            _keyboardState = Keyboard.GetState();

            _isColliding = false;
            foreach (BoundingRectangle hitbox in _hitboxes)
            {
                if (!Hitbox.Equals(hitbox) && CollisionCalculator.ItemsCollide(Hitbox, hitbox)) _isColliding = true; // can't stop colliding
            }


            if (!_isColliding) _position += _gamePadState.ThumbSticks.Left * new Vector2(PLAYER_SPEED, -PLAYER_SPEED);

            if (IsntMoving())
            {
                _isMoving = false;
                _currentRunningState = 0;
            }
            else
            {
                _isMoving = true;
                _currentIdleState = 0;
            }

            if (_gamePadState.ThumbSticks.Left.X < 0) _flipped = true;
            else if (_gamePadState.ThumbSticks.Left.X > 0) _flipped = false;

            if (!_isColliding)
            {
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
                _hitbox.ChangePositionTo(new Vector2(_position.X, _position.Y));
            }
        }

        private bool IsntMoving()
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
                if (_animationTimer > ANIMATION_SPEED)
                {
                    _previousRunningState = _currentRunningState;
                    _currentRunningState = (_previousRunningState < MAX_RUN_INT_VALUE) ? _currentRunningState + 1 : 0;
                    _animationTimer -= ANIMATION_SPEED;
                }
                spriteBatch.Draw(_runningTextures[_currentRunningState], _position, null, Color.Black, 0, new Vector2(0, 0), .75f, spriteEffects, 0);
            }
            else
            {
                if (_animationTimer > ANIMATION_SPEED)
                {
                    _previousIdleState = _currentIdleState;
                    _currentIdleState = (_previousIdleState < MAX_IDLE_INT_VALUE) ? _currentIdleState + 1 : 0;
                    _animationTimer -= ANIMATION_SPEED;
                }
                spriteBatch.Draw(_idleTextures[_currentIdleState], _position, null, Color.Black, 0, new Vector2(0, 0), .75f, spriteEffects, 0);
            }
        }

    }
}
