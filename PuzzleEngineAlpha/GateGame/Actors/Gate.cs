﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GateGame.Actors
{
    public class Gate : PuzzleEngineAlpha.Actors.StaticObject
    {
        #region Declarations

        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;

        #endregion
        #region Constructor

        public Gate(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, int frameWidth, int frameHeight,string tag)
            : base(tileMap,camera,location, frameWidth, frameHeight)
        {
            this.animations.Add("active", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "active"));
            currentAnimation = "active";
            tranparencyTransition = new PuzzleEngineAlpha.Animations.SmoothTransition(1.0f, 0.01f, 0.2f, 1.0f);
            this.Tag = tag;
        }

        #endregion

        #region Properties

        public Rectangle InteractionRectangle
        {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (enabled)
                tranparencyTransition.Increase(gameTime);
            else
                tranparencyTransition.Decrease(gameTime);

            Transparency = tranparencyTransition.Value;
        }

        #endregion

    }
}
