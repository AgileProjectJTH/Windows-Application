using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CorridorWPF
{
    /// <summary>
    /// This class is related to TvViewStaff class, this is for the datagrid next to it which should contain notes related to a staff
    /// </summary>
    class TvViewStaffNotes
    {
        DataGrid dataGrid;
        public TvViewStaffNotes(DataGrid _dataGrid)
        {
            dataGrid = _dataGrid;
        }
        /// <summary>
        /// Creates a header which contains the string "Notes"
        /// </summary>
        public void createHeader()
        {
            clearGrid();

            DataGridTextColumn gridColumn = new DataGridTextColumn();
            gridColumn.Header = "Notes";
            gridColumn.Binding = new Binding("Notes");
            gridColumn.Width = dataGrid.Width;
            dataGrid.Columns.Add(gridColumn);

        }
        /// <summary>
        /// Clears the datagrid
        /// </summary>
        public void clearGrid()
        {
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.Items.Refresh();
        }
        /// <summary>
        /// Adds a string note to the datagrid
        /// </summary>
        /// <param name="note"></param>
        public void addNote(string note)
        {
            TVStaffNote staffNote = new TVStaffNote();
            DataGridRow newRow = new DataGridRow();

            staffNote.Notes = note;

            newRow.Item = staffNote;
            dataGrid.Items.Add(newRow);
            updateRowHeight();

        }
        /// <summary>
        /// Updates the row height in realation to how many rows there are, should fill the datagrid evenly
        /// </summary>
        private void updateRowHeight()
        {
            dataGrid.MinRowHeight = ((dataGrid.Height / dataGrid.Items.Count) - 5);
        }
    }

    class TvViewStaff
    {

        DataGrid dataGrid;

        public TvViewStaff(DataGrid _dataGrid)
        {
             dataGrid = _dataGrid;
        }

        /// <summary>
        /// Creates headers for the TV view
        /// </summary>
        public void createHeaders()
        {
            clearGrid();

            string[] TvHeaders = new string[2] { "Staff", "Availability" };

            for (int ii = 0; ii < 2; ii++)
            {
                DataGridTextColumn gridColumn = new DataGridTextColumn();
                gridColumn.Header = TvHeaders[ii];
                gridColumn.Binding = new Binding(TvHeaders[ii]);
                gridColumn.Width = (dataGrid.ActualWidth / 2);
                dataGrid.Columns.Add(gridColumn);
            }

        }


        /// <summary>
        /// Adds a new Staff member
        /// </summary>
        /// <param name="staffName"></param>
        /// <param name="availability"></param>
        public void addStaff(string staffName, bool availability)
        {
            TVStaff staff = new TVStaff();
            DataGridRow newRow = new DataGridRow();

            staff.Staff = staffName;
            
            if (availability)
            {
                staff.Availability = "Available";
                newRow.Background = Brushes.LightGreen;
            }
            else
            {
                staff.Availability = "Unavailable";
                newRow.Background = Brushes.Salmon;
            }

            newRow.Item = staff;
            dataGrid.Items.Add(newRow);

            updateRowHeight();
        }

        /// <summary>
        /// Updates the height of each row
        /// </summary>
        private void updateRowHeight()
        {
            dataGrid.MinRowHeight = ((dataGrid.ActualHeight / dataGrid.Items.Count) - 5); //Changed dataGrid.Height to dataGrid.ActualHeight
        }


        /// <summary>
        /// Clears the content in the Data Grid
        /// </summary>
        public void clearGrid()
        {
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.Items.Refresh();
        }

    }

    class TvViewStudents //Antar att klassen kommer skilja sig lite grann gentemot TvViewStaff
    {
        DataGrid dataGrid;

        public TvViewStudents(DataGrid _dataGrid)
        {
            dataGrid = _dataGrid;
        }

        /// <summary>
        /// Creates headers for the TV view
        /// </summary>
        public void createHeaders()
        {
            clearGrid();

            string[] TvHeaders = new string[2] { "Staff", "Availability" };

            for (int ii = 0; ii < 2; ii++)
            {
                DataGridTextColumn gridColumn = new DataGridTextColumn();
                gridColumn.Header = TvHeaders[ii];
                gridColumn.Binding = new Binding(TvHeaders[ii]);
                gridColumn.Width = (dataGrid.Width / 2);
                dataGrid.Columns.Add(gridColumn);
            }

        }

        /// <summary>
        /// Adds a new Staff member
        /// </summary>
        /// <param name="staffName"></param>
        /// <param name="availability"></param>
        public void addStaff(string staffName, bool availability)
        {
            TVStaff staff = new TVStaff();
            DataGridRow newRow = new DataGridRow();

            staff.Staff = staffName;

            if (availability)
            {
                staff.Availability = "Available";
                newRow.Background = Brushes.LightGreen;
            }
            else
            {
                staff.Availability = "Unavailable";
                newRow.Background = Brushes.Salmon;
            }

            newRow.Item = staff;
            dataGrid.Items.Add(newRow);

            updateRowHeight();
        }

        /// <summary>
        /// Updates the height of each row
        /// </summary>
        private void updateRowHeight()
        {
            dataGrid.MinRowHeight = ((dataGrid.Height / dataGrid.Items.Count) - 5);
        }


        /// <summary>
        /// Clears the content in the Data Grid
        /// </summary>
        public void clearGrid()
        {
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.Items.Refresh();
        }


    }




    /// <summary>
    /// Staff member class
    /// </summary>
    public class TVStaff
    {
        public string Staff { get; set; }
        public string Availability { get; set; }
    }

    public class TVStaffNote
    {
        public string Notes { get; set; }
    }
        
        
}
