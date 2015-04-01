using System;
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
        private int moveDirectionConstant;                  // Field for MoveUp/Down/Left/Right methods

        private DreamCheekyMissileLauncher() : base()
        {
            MaxMissileCount = 4;
            CurrentMissileCount = MaxMissileCount;
            MoveDirectionConstant = 100;

            internalMissileLauncher = new MissileLauncher();
        }

        public static DreamCheekyMissileLauncher GetInstance()
        {
            if (dreamCheekyMissileLauncherInstance == null)
            {
                dreamCheekyMissileLauncherInstance = new DreamCheekyMissileLauncher();
                return dreamCheekyMissileLauncherInstance;
            }

            return dreamCheekyMissileLauncherInstance;
        }

        // Property for MoveUp/Down/Left/Right methods
        public int MoveDirectionConstant
        {
            get { return moveDirectionConstant; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Error: the move direction constant cannot be negative", value.ToString());
                else
                    moveDirectionConstant = value; 
            }
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

        public override void MoveUp()
        {
            System.Console.WriteLine("MoveUp method of SADMissileLauncher invoked!");

            try
            {
                internalMissileLauncher.command_Up(DegreesToTime(MoveDirectionConstant));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in MoveUp method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
                System.Console.WriteLine("Reseting to default coordinates...");

                internalMissileLauncher.command_reset();
            }
        }

        public override void MoveDown()
        {
            System.Console.WriteLine("MoveDown method of SADMissileLauncher invoked!");

            try
            {
                internalMissileLauncher.command_Down(DegreesToTime(MoveDirectionConstant));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in MoveDown method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
                System.Console.WriteLine("Reseting to default coordinates...");

                internalMissileLauncher.command_reset();
            }
        }

        public override void MoveLeft()
        {
            System.Console.WriteLine("MoveLeft method of SADMissileLauncher invoked!");

            try
            {
                internalMissileLauncher.command_Left(DegreesToTime(MoveDirectionConstant));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in MoveLeft method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
                System.Console.WriteLine("Reseting to default coordinates...");

                internalMissileLauncher.command_reset();
            }
        }

        public override void MoveRight()
        {
            System.Console.WriteLine("MoveRight method of SADMissileLauncher invoked!");

            try
            {
                internalMissileLauncher.command_Right(DegreesToTime(MoveDirectionConstant));
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.WriteLine("EXCEPTION in MoveRight method of DreamCheekyMissileLauncher: The arguments are out of range.", e);
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
