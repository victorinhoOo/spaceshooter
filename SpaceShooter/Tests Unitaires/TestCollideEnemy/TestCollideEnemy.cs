﻿using IUTGame;
using SpaceShooter.model.Ennemies;
using SpaceShooter.model.Projectiles;
using SpaceShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Windows.Media.Media3D;

namespace Tests_Unitaires.TestCollideEnemy
{
    /// <summary>
    /// Test unitaires des collisions entre les ennemis
    /// </summary>
    /// <author>Victor Duboz</author>
    public class TestCollideEnemy
    {
        private Game game;
        private General general;
        private General general2;
        private Enemy soldier;
        private Enemy soldier2;
        private Enemy officer;
        private Enemy officer2;
        private FakeScreen s;
        private FakeWindow w;

        [Fact]
        public void TestCollideSoldierOfficer()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            soldier = new Soldier(100, 100, game);
            officer = new Officer(100, 100, game);


            Assert.True(officer.IsCollide(soldier));

            soldier.Angle = 40;
            officer.Angle = 20;


            officer.CollideEffect(soldier);
            soldier.CollideEffect(officer);

            Assert.Equal((360 + 180 - 40) % 360, soldier.Angle);
            Assert.Equal((360 + 180 - 20) % 360, officer.Angle);
        }

        [Fact]
        public void TestCollideSoldierSoldier()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            soldier = new Soldier(100, 100, game);
            soldier2 = new Soldier(100, 100, game);

            Assert.True(soldier.IsCollide(soldier2));

            soldier.Angle = 80;
            soldier2.Angle = 30;

            soldier.CollideEffect(soldier2);
            soldier2.CollideEffect(soldier);

            Assert.Equal((360 + 180 - 80) % 360, soldier.Angle);
            Assert.Equal((360 + 180 - 30) % 360, soldier2.Angle);
        }

        [Fact]
        public void TestCollideOfficerOfficer()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            officer = new Officer(100, 100, game);
            officer2 = new Officer(100, 100, game);

            Assert.True(officer.IsCollide(officer2));

            officer.Angle = 100;
            officer2.Angle = 50;

            officer.CollideEffect(officer2);
            officer2.CollideEffect(officer);


            Assert.Equal((360 + 180 - 100) % 360, officer.Angle);
            Assert.Equal((360 + 180 - 50) % 360, officer2.Angle);
        }

        /// <author>Alexandre Hugot</author>
        [Fact]
        public void TestCollideGeneralGeneral()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            general = new General(100, 100, game);
            general2 = new General(100, 100, game);

            Assert.True(general.IsCollide(general2));

            general.Angle = 100;
            general2.Angle = 50;

            general.CollideEffect(general2);
            general2.CollideEffect(general);


            Assert.Equal((360 + 180 - 100) % 360, general.Angle);
            Assert.Equal((360 + 180 - 50) % 360, general2.Angle);
        }

        /// <author>Alexandre Hugot</author>
        [Fact]
        public void TestCollideSoldierGeneral()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            soldier = new Soldier(100, 100, game);
            general = new General(100, 100, game);


            Assert.True(general.IsCollide(soldier));

            soldier.Angle = 40;
            general.Angle = 20;


            general.CollideEffect(soldier);
            soldier.CollideEffect(general);

            Assert.Equal((360 + 180 - 40) % 360, soldier.Angle);
            Assert.Equal((360 + 180 - 20) % 360, general.Angle);
        }

        /// <author>Alexandre Hugot</author>
        [Fact]
        public void TestCollideGeneralOfficer()
        {
            w = new FakeWindow();
            s = new FakeScreen();


            game = new TheGame(s, w);
            general = new General(100, 100, game);
            officer = new Officer(100, 100, game);


            Assert.True(officer.IsCollide(general));

            general.Angle = 40;
            officer.Angle = 20;


            officer.CollideEffect(general);
            general.CollideEffect(officer);

            Assert.Equal((360 + 180 - 40) % 360, general.Angle);
            Assert.Equal((360 + 180 - 20) % 360, officer.Angle);
        }
    }
}
