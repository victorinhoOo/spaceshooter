﻿using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.model.Projectiles
{
    /// <summary>
    /// Gère les projectiles du jeu
    /// </summary>
    /// <author>Théo Cornu</author>
    abstract public class Projectile : GameItem, IAnimable
    {
        private double speed = 200;
        public double Speed { get => speed; set => speed = value; }


        /// <summary>
        /// Créé un projectile
        /// </summary>
        ///<author>Théo Cornu</author>
        public Projectile(double x, double y, Game g, string name , int zindex = 0) :
            base(x, y, g, "player_bullet.png", zindex)
        {

        }
         public abstract void Animate(TimeSpan dt);
        
        /// <summary>
        /// Renvoi le type de projectile
        /// </summary>
        public override string TypeName => "Projectile";
    }
}
