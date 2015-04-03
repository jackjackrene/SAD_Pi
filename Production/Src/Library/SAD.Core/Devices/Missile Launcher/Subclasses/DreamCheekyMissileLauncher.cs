﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildDefender;

namespace SAD.Core.Devices.Missile_Launcher.Subclasses
{
    sealed class DreamCheekyMissileLauncher : SADMissileLauncher
    {
        private static DreamCheekyMissileLauncher dreamCheekyMissileLauncherInstance;
        private MissileLauncher internalMissileLauncher;    // should this be a "singleton?"

        private DreamCheekyMissileLauncher() : base()
        {
            MaxMissileCount = 4;
            CurrentPhi = 0;
            CurrentTheta = 0;
            CurrentMissileCount = MaxMissileCount;

            internalMissileLauncher = new MissileLauncher();
        }

        public static DreamCheekyMissileLauncher GetInstance()
        {
            if (dreamCheekyMissileLauncherInstance == null)
            {
                MissileLauncherType = SADMissileType.DreamCheekyMissileLauncher;

                dreamCheekyMissileLauncherInstance = new DreamCheekyMissileLauncher();
                return dreamCheekyMissileLauncherInstance;
            }

            return dreamCheekyMissileLauncherInstance;
        }

        public override void Kill(double phi, double theta)
        {
            System.Console.WriteLine("Kill method of SADMissileLauncher invoked!");

            if (CurrentMissileCount < 1)
                System.Console.WriteLine("No Missiles left! MUST RELOAD!!");
            else
            {
                Move(phi, theta);
                Fire();
            }
        }

        public override void Fire()
        {
            if (CurrentMissileCount < 1)
                System.Console.WriteLine("Reload! No missiles left!");
            else
            {
                System.Console.WriteLine("Fire method of SADMissileLauncher invoked!");
                internalMissileLauncher.command_Fire();

                // Update Missile count (could be its own method)
                currentMissleCount--;
            }
        }

        public override void Move(double phi, double theta)
        {
            CurrentPhi = phi;
            CurrentTheta = theta;

            if (CurrentPhi > 135)
                CurrentPhi = 135;
            if (CurrentPhi < -135)
                CurrentPhi = -135;

            if (CurrentTheta > 45)
                CurrentTheta = 45;
            if (CurrentTheta < 0)
                CurrentTheta = 0;

            System.Console.WriteLine("Move method of SADMissileLauncher invoked!");
            try
            {
                // Reset the Launcher
                internalMissileLauncher.command_reset();

                // Move by theta
                if (theta < 0)
                    internalMissileLauncher.command_Down(DegreesToTime(theta));
                else
                    internalMissileLauncher.command_Up(DegreesToTime(theta));

                // Move by phi
                if (phi < 0)
                    internalMissileLauncher.command_Left(DegreesToTime(phi));
                else
                    internalMissileLauncher.command_Right(DegreesToTime(phi));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in Move method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
                System.Console.WriteLine("Reseting to default coordinates...");

                internalMissileLauncher.command_reset();
            }
        }

        public override void MoveBy(double phi, double theta)
        {
            CurrentPhi = CurrentPhi + phi;
            CurrentTheta = CurrentTheta = theta;

            if (CurrentPhi > 135)
                CurrentPhi = 135;
            if (CurrentPhi < -135)
                CurrentPhi = -135;

            if (CurrentTheta > 45)
                CurrentTheta = 45;
            if (CurrentTheta < 0)
                CurrentTheta = 0;

            System.Console.WriteLine("MoveBy method of SADMissileLauncher invoked!");

            try
            {
                // Move by theta
                if (theta < 0)
                    internalMissileLauncher.command_Down(DegreesToTime(theta));
                else
                    internalMissileLauncher.command_Up(DegreesToTime(theta));

                // Move by phi
                if (phi < 0)
                    internalMissileLauncher.command_Left(DegreesToTime(phi));
                else
                    internalMissileLauncher.command_Right(DegreesToTime(phi));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in MoveBy method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
                System.Console.WriteLine("Reseting to default coordinates...");

                internalMissileLauncher.command_reset();
            }
        }

        private int DegreesToTime(double degree)
        {
            int returnValue = 0;
            double conversionRate = 20;

            returnValue = Math.Abs((int)Math.Round(degree * conversionRate));

            return returnValue;
        }
    }
}
