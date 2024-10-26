﻿using IUTGame;
using SpaceShooter.model.Projectiles;
using SpaceShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SpaceShooter.model.Ennemies
{
    /// <summary>
    /// Gère les soldats
    /// </summary>
    /// <author>Victor Duboz</author>
    public class Soldier : Enemy
    {
        private TimeSpan shootInterval = TimeSpan.FromSeconds(1); // Intervalle de 1 seconde entre les tirs
        private TimeSpan timeSinceLastShot = TimeSpan.Zero; // Temps écoulé depuis le dernier tir

        private List<string> explosionSprites; // Liste des sprites d'explosion
        private int currentExplosionIndex; // index de l'explosion du soldat
        private bool isExploding; // soldat en train d'exploser ou non
        public bool IsExploding { get => isExploding; set => isExploding = value; }

        private TheGame g;


        /// <summary>
        /// Construit un soldat, le fait apparaître avec un angle aléatoire parmis un interval
        /// </summary>
        /// <param name="x">Coordonnées d'apparition</param>
        /// <param name="y">Coordonnées d'apparition</param>
        /// <param name="g">Jeu auquel il appartient</param>
        /// <param name="name">sprite du soldat</param>
        public Soldier(double x, double y, Game g, string name = "Ship_5.png") : base(x, y, g, name, -100)
        {
            Random random = new Random();
            double randomAngle;

            if (random.NextDouble() < 0.5)
            {
                randomAngle = random.NextDouble() * 50 - 25; // apparaît entre -25 et 25
            }
            else
            {
                randomAngle = random.NextDouble() * 50 - 25 + 180; // apparaît avec un angle opposé
            }
            base.Angle = randomAngle;
            this.g = (TheGame)g;

            explosionSprites = new List<string>()
            {
                "explosion1.png",
                "explosion2.png",
                "explosion3.png",
                "explosion4.png",
                "explosion5.png"
            };
            currentExplosionIndex = 0;
            IsExploding = false;
        }


        /// <summary>
        /// Tire une balle
        /// </summary>
        /// <author>Victor Duboz</author>
        public void Shoot()
        {
            Bullet bullet = new Bullet(this.Left, this.Bottom - 10, this.TheGame);
            TheGame.AddItem(bullet);
        }

        private Random random = new Random();

        /// <summary>
        /// Effectue l'animation du soldat
        /// </summary>
        /// <param name="dt">durée écoulée depuis la dernière animation</param>
        /// <author>Victor Duboz</author>
        public override void Animate(TimeSpan dt)
        {
            if (IsExploding)
            {
                if (currentExplosionIndex < explosionSprites.Count)
                {
                    this.ChangeSprite(explosionSprites[currentExplosionIndex]);
                    currentExplosionIndex++;
                    if (currentExplosionIndex >= explosionSprites.Count)
                    { 
                        TheGame.RemoveItem(this);
                        --GeneratorEnemy.Amount;
                        this.GenerateBonus();
                        g.Score += 1;
                    }
                }
                return;
            }

            if (this.Top < 0)
            {
                Top = 0;
                Angle = 360 - Angle;
            }
            else if (Bottom > GameHeight - 100)
            {
                Bottom = GameHeight - 100;
                Angle = 360 - Angle;
            }
            else if (Left < 0)
            {
                Angle = (360 + 180 - Angle) % 360;
                Left = 0;
            }
            else if (Right > GameWidth)
            {
                Angle = (360 + 180 - Angle) % 360;
                Right = GameWidth;
            }

            if (timeSinceLastShot >= shootInterval)
            {
                Shoot();
                double delay = random.NextDouble() * 0.5 + 0.5; // délai aléatoire entre les tirs
                shootInterval = TimeSpan.FromSeconds(delay);
                timeSinceLastShot = TimeSpan.Zero; // Réinitialise le temps écoulé depuis le dernier tir
            }

            MoveDA(Speed * dt.TotalSeconds, Angle);
            timeSinceLastShot += dt;

        }


        /// <summary>
        /// Gère les collisions du soldat avec les autres items du jeu
        /// </summary>
        /// <param name="other">autre item</param>
        /// <author>Victor Duboz</author>
        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "PlayerBullet")
            {
               
                if (!IsExploding)
                {
                    IsExploding = true;
                    currentExplosionIndex = 0;
                    TheGame.RemoveItem(other);
                    return;
                }


            }
            if (other.TypeName == "Enemy")
            {
                Angle = (360 + 180 - Angle) % 360;
            }
        }
    }
}
