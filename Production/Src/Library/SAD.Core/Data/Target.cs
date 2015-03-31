using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SAD.Core
{
    public class Target : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Fields for talking to the ViewModel
        private string name;
        private double x;
        private double y;
        private double z;
        private bool friend;
        private bool status; // as in "Dead or Alive"
        private int points;
        private int flashRate;
        private int spawnRate;
        private bool canSwapSidesWhenHit;
        private double phi;
        private double theta;

        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public double X 
        { 
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }
        public double Y 
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }
        public double Z 
        { 
            get
            {
                return z;
            }
            set
            {
                z = value;
                OnPropertyChanged();
            }
        }
        public bool Friend 
        { 
            get
            {
                return friend;
            }
            set
            {
                friend = value;
                OnPropertyChanged();
            }
        }
        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public int Points 
        { 
            get
            {
                return points;
            }
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }
        public int FlashRate
        {
            get
            {
                return flashRate;
            }
            set
            {
                flashRate = value;
                OnPropertyChanged();
            }
        }
        public int SpawnRate
        {
            get
            {
                return spawnRate;
            }
            set
            {
                spawnRate = value;
                OnPropertyChanged();
            }
        }
        public bool CanSwapSidesWhenHit 
        {
            get
            {
                return canSwapSidesWhenHit;
            }
            set
            {
                canSwapSidesWhenHit = value;
                OnPropertyChanged();
            }
        }
        public double Phi 
        {
            get
            {
                return phi;
            }
            set
            {
                phi = value;
                OnPropertyChanged();
            }
        }
        public double Theta 
        {
            get
            {
                return theta;
            }
            set
            {
                theta = value;
                OnPropertyChanged();
            }
        }

        public Target()
        {
            Name = "NOT VALID";
        }
        
        public Target(string Nm, double x, double y, double z, bool Frd, int Pts, int FR, int SR, bool CSSWH)
        {
            Name = Nm;
            X = x;
            Y = y;
            Z = z;
            Friend = Frd;
            Points = Pts;
            FlashRate = FR;
            SpawnRate = SR;
            CanSwapSidesWhenHit = CSSWH;
        }

        // The [NotifyPropertyChangedInvocator] is a Resharper thing, ignore it
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
