using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace CorridorWPF
{
    /// <summary>
    /// Interaction logic for StaffTvFullscreen.xaml
    /// </summary>
    public partial class StaffTvFullscreen : Window
    {
        public string token = "";
        ComboBox cb_Box = new ComboBox();
        private List<string> notes = new List<string>();

        public StaffTvFullscreen(ComboBox _cb_Box , List<string> _notes,string _token)
        {      
            InitializeComponent();

            cb_Box = _cb_Box;
            token = _token;
            notes = _notes;
            

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();

            this.WindowState = WindowState.Maximized;




        }

        public void intialScheduleLoad()
        {
            Repository.ScheduleRepository.LoadTvView(dGrid_staffTvFullscreen, cb_Box, token);
            loadNotes();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Repository.ScheduleRepository.LoadTvView(dGrid_staffTvFullscreen, cb_Box, token);
            loadNotes();
        }

        private void loadNotes()
        {
            //Fetching data from a textbox
            TVStaffNote rawNote = new TVStaffNote();
            TVStaffNote note = new TVStaffNote();
            var builder = new StringBuilder();
            TvViewStaffNotes staffNotes = new TvViewStaffNotes(dGrid_staffTvNotesFullscreen);
            staffNotes.createHeader();

            for (int i = 0; i < notes.Count; i++)
            {
                staffNotes.addNote(notes[i]);
            }
            


        }

        private void dGrid_staffTvNotesFullscreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
