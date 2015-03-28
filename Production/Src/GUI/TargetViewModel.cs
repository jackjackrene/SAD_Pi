using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GUI;
using SAD.Core;



namespace SAD.Core
{
    public class TargetViewModel : ViewModelBase
    {
        private Target m_target;

        public TargetViewModel(Target target)
        {
            m_target = target;
            KillTargetCommand = new MyCommand(KillTarget);
        }

        /// <summary>
        /// Gets the target model data.
        /// </summary>
        public Target Target
        {
            get { return m_target; }
        }

        private void KillTarget()
        {
            //Do some stuff....

           // m_target.IsAlive = false;
        }

        public ICommand KillTargetCommand { get; set; }
    }
}