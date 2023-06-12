﻿using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.model.Ennemies
{
    public class General : Enemy
    {
        public General(double x, double y, Game g, string name = "Ship_5.png") : base(x, y, g, name, -100)
        {



        }
        public override string TypeName => "General";

        public void Shoot()
        {

        }

        public void ShootMissile()
        {

        }
        public override void Animate(TimeSpan dt)
        {
            if (Waiting > TimeSpan.Zero)
            {
                Waiting = Waiting - dt;
            }
            if (Top < 0)
            {
                Top = 0;


            }
            else if (Bottom > GameHeight)
            {
                TheGame.RemoveItem(this);
                

            }
            else if (Touched)
            {
                TheGame.RemoveItem(this);
                
                //peut etre rajouter une option qui fais que quand le spaceship est touché 
                //alors son sprite change en exploision puis l'item disparait
            }



        }
    }
}