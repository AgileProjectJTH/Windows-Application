using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
//using System.Threading.Tasks;

namespace CorridorWPF
{
    /// <summary>
    /// Interaction logic for StudentTvFullscreen.xaml
    /// </summary>
    public partial class StudentTvFullscreen : Window
    {
        
        string token;
        ComboBox cb_Box = new ComboBox();
        
        public StudentTvFullscreen(ComboBox _cb_Box, string _token)
        {
            InitializeComponent();

            cb_Box = _cb_Box;
            token = _token;


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
            

        }


        public void intialScheduleLoad()
        {
            Repository.ScheduleRepository.LoadTvView(dGridStudentTvFullscreen, cb_Box, token);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Repository.ScheduleRepository.LoadTvView(dGridStudentTvFullscreen, cb_Box, token);
        }



    }


}
