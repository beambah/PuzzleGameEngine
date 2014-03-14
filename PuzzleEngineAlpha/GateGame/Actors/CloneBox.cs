﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Actors
{
    public class CloneBox : StaticObject
    {
        #region Declarations

        Dictionary<Player, PlayerClone> playersAndClones;
        ActorManager actorManager;
        ContentManager content;

        #endregion

        #region Constructor

        public CloneBox(ActorManager actorManager,PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location,Texture2D texture, ContentManager content, int frameWidth, int frameHeight, string tag)
            : base(tileMap, camera, location, frameWidth, frameHeight)
        {
            this.content=content;
            this.actorManager=actorManager;
            this.animations.Add("active", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "active"));
            currentAnimation = "active";
            this.enabled = false;
            this.Tag = tag;
            playersAndClones = new Dictionary<Player, PlayerClone>();
        }

        #endregion

        #region Properties

        public string Tag
        {
            get;
            set;
        }

        #endregion

        #region Helper Methods

        public void AddPlayer(Player player)
        {
            if (!playersAndClones.ContainsKey(player))
            {
                PlayerClone clone = new PlayerClone(this.actorManager, this.tileMap, this.camera, player.location, content, player.frameWidth, player.frameHeight, player.collideWidth, player.collideHeight);
                clone.playerToRecord = player;
                clone.InteractionRectangle = this.CollisionRectangle;
                playersAndClones.Add(player, clone);
                actorManager.AddPlayerClone(clone);
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            foreach (PlayerClone clone in playersAndClones.Values)
            {
                clone.Update(gameTime);
                if (clone.Destroy)
                {
                    actorManager.RemovePlayerClone(clone);

                    var cloneToRemove = playersAndClones.First(kvp => kvp.Value == clone);
                    playersAndClones.Remove(cloneToRemove.Key);
                    return;
                }
            }
            base.Update(gameTime);
        }

        #endregion



    }
}
