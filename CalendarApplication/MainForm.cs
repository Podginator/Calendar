using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Calendar
{
    public partial class MainForm : Form 
    {
        #region Members
        IAppointments _Appointments;
        IAppointments _TodaysAppointments;
        IAppointment _SelectedAppointment;
        //Used to determine if mousemove needs to invalidate
        int _MousePos;
        int _SelectedRow;
        int _HighlightedRow;
        //Used to store where the rectanges are and used in other functions.
        Dictionary<IAppointment, Rectangle> _UiRect;
        private ToolTip _DisabledTooltip;
        bool _ToolTipActive;
        Timer _Notifications;
        const int PanelRowHeight = 45;
        const int ApptOffset = 50;
        
        #endregion

        #region Constructor
        public MainForm()
        {
            _Notifications = new Timer();
            _UiRect = new Dictionary<IAppointment, Rectangle>();
            _DisabledTooltip = new ToolTip();
            InitializeComponent();
        }
        #endregion

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            #region Notifications
            TimeSpan diffbetween = DateTime.Now.Minute > 20 && DateTime.Now.Minute < 50 ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 50, 0) - DateTime.Now
                : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Minute < 50 ? DateTime.Now.Hour : DateTime.Now.AddHours(1).Hour, 20, 0) - DateTime.Now;
            //Get time to the nearest 20 or 50 minute interval.
            _Notifications.Interval = (int)diffbetween.TotalMilliseconds;
            _Notifications.Tick += new EventHandler(DisplayNotification);
            _Notifications.Start();
            #endregion
            _Appointments = new Appointments();
            if (!_Appointments.Load())
            {
                MessageBox.Show("No appointment file exists or an error occured while trying to load the appointments file",
                                "Creating New File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            _TodaysAppointments = new Appointments();
            labelDisplayedDate.Text = DateTime.Now.ToLongDateString();
            GetAppointmentsOnSelectedDate(DateTime.Now);
            vScrollBar.Height = panelDailyView.ClientRectangle.Size.Height;
            vScrollBar.Maximum = 47 - (panelDailyView.ClientRectangle.Size.Height / PanelRowHeight);
            // Chaned this so that it starts closer to the DateTime.Now.
            //The vScrollBar range at the start-up resolution is capped at 38. So we have to make sure it's no higher than that.
            _SelectedRow = Utility.ConvertTimeToRow(DateTime.Now);
            vScrollBar.Value = Math.Min(38, _SelectedRow);
        }

        private void panelDailyView_Paint(object sender, PaintEventArgs e)
        {
            #region Local Vars
            int paintWidth = panelDailyView.ClientRectangle.Size.Width - vScrollBar.Width;
            int paintHeight = panelDailyView.ClientRectangle.Size.Height;
            int displayedRowCount = paintHeight / PanelRowHeight;
            int panelTopRow;
            int nextRow;
            int apptStartRow;
            int apptLength;
            string dispTime;
            _UiRect.Clear();
            Font font = new Font("Arial", 10);
            Brush drawBrush = new SolidBrush(Color.DarkBlue);
            Brush appointmentBrush;
            //Brush highlighedappointmentBrush = new SolidBrush(Color.AliceBlue);


            Graphics g = e.Graphics;
            #endregion

            #region Background Panel
            // Fill the background of the panel
            g.FillRectangle(new SolidBrush(Color.Linen), 0, 0, paintWidth, paintHeight);
            panelTopRow = vScrollBar.Value;
            if (_SelectedRow >= panelTopRow &&
                _SelectedRow <= panelTopRow + displayedRowCount)
            {
                // If the selected time is displayed, mark it
                g.FillRectangle(new SolidBrush(Color.DarkKhaki),
                                0,
                                (_SelectedRow - panelTopRow) * PanelRowHeight,
                                paintWidth,
                                PanelRowHeight);
            }
            if (_HighlightedRow >= panelTopRow &&
                _HighlightedRow <= panelTopRow + displayedRowCount && _HighlightedRow != _SelectedRow)
            {
                // If the selected time is displayed, mark it

                g.FillRectangle(new SolidBrush(Color.Yellow),
                                0,
                                (_HighlightedRow - panelTopRow) * PanelRowHeight,
                                paintWidth,
                                PanelRowHeight);
                SizeF strSize = g.MeasureString("Double Click to add appointment", font);
                g.DrawString("Double click to add appointment!", font, drawBrush, (paintWidth / 2) - strSize.Width / 2, ((_HighlightedRow - panelTopRow) * PanelRowHeight + strSize.Height / 2));

            }
            // Display the times at the start of the rows and
            // the lines separating the rows
            nextRow = panelTopRow;
            for (int i = 0; i <= displayedRowCount; i++)
            {
                dispTime = (nextRow / 2).ToString("0#") + (nextRow % 2 == 0 ? ":00" : ":30");
                nextRow++;
                g.DrawString(dispTime, font, drawBrush, 2, (i * PanelRowHeight + 4));
                g.DrawLine(Pens.DarkBlue, 0, i * PanelRowHeight, paintWidth, i * PanelRowHeight);
            }
            // Now fill in the appointments
            #endregion

            #region Appointments
            foreach (IAppointment appointment in _TodaysAppointments)
            {
                //Have to reset this, otherwise they stay on screen.
                if (_UiRect.ContainsKey(appointment))
                {
                    _UiRect[appointment] = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    _UiRect.Add(appointment, new Rectangle(0, 0, 0, 0));
                }
                apptStartRow = Utility.ConvertTimeToRow(appointment.Start);
                apptLength = Utility.ConvertLengthToRows(appointment.Length);

                // See if the appointment is inside the part of the day displayed on the panel
                if (((apptStartRow >= panelTopRow) &&
                     (apptStartRow <= panelTopRow + displayedRowCount)) ||
                    (apptStartRow + apptLength > panelTopRow))
                {
                    // Calculate the area of the panel occupied by
                    // the appointment
                    if (apptStartRow < panelTopRow)
                    {
                        apptLength = apptLength - (panelTopRow - apptStartRow);
                        apptStartRow = panelTopRow;
                    }
                    int apptDispStart = (apptStartRow - panelTopRow) * PanelRowHeight;
                    int apptDispLength = apptLength * PanelRowHeight;

                    if (apptDispStart + apptDispLength > paintHeight)
                    {
                        apptDispLength = paintHeight - apptDispStart;
                    }

                    //Rendered here.
                    Rectangle apptRectangle = new Rectangle(ApptOffset,
                                                            apptDispStart,
                                                            (paintWidth - (ApptOffset * 2)),
                                                            apptDispLength);
                    _UiRect[appointment] = apptRectangle;



                }
            }

            //Method used to check if they're overlapping, will adjust rectangles if so.
            OverlappingRectangles();

            foreach (KeyValuePair<IAppointment, Rectangle> appointment in _UiRect)
            {
                string type = Regex.Match(appointment.Key.DisplayableDescription, @"\[.*\]").Value;

                switch(type)
                {
                    case "[Meeting]":
                        appointmentBrush = new SolidBrush(Color.DodgerBlue);
                        break;
                    case "[Birthday]":
                        appointmentBrush = new SolidBrush(Color.LightGreen);
                        break;
                    case "[Interview]":
                        appointmentBrush = new SolidBrush(Color.Coral);
                        break;
                    case "[HR]":
                        appointmentBrush = new SolidBrush(Color.Turquoise);
                        break;
                    default:
                        appointmentBrush = new SolidBrush(Color.LightBlue);
                        break;
                }

                //appointmentBrush = drawBrush;
                if (appointment.Value.Y >= 0 && appointment.Value.X != 0)
                {
                    g.FillRectangle(appointmentBrush,
                               appointment.Value);

                    // Draw the black line around it
                    g.DrawRectangle(Pens.Black, appointment.Value);
                    if (Utility.ConvertTimeToRow(appointment.Key.Start) >= panelTopRow)
                    {
                        // If the top line of the appointment is displayed,
                        // write out the subject and location.  Temporarily
                        // reduce the clip area for the graphics object to ensure
                        // that the text does not extend beyond the rectangle
                        Region oldClip = g.Clip;
                        g.Clip = new Region(appointment.Value);
                        Rectangle clippingrect = appointment.Value;
                        clippingrect.X += 5;
                        clippingrect.Y += 5;
                        //clippingrect.Width -= 20;

                        SizeF strSize = g.MeasureString(appointment.Key.DisplayableDescription, font, clippingrect.Width);
                        font = strSize.Height > clippingrect.Height ? new Font("Arial", 8) : new Font("Arial", 10);

                        g.DrawString(appointment.Key.DisplayableDescription, font, drawBrush, clippingrect);
                        g.Clip = oldClip;


                    }
                }
            }
            #endregion
        }

        private void panelDailyView_Resize(object sender, EventArgs e)
        {
            int oldMax = vScrollBar.Maximum;

            // Adjust the scroll bar range to reflect the
            // fact that the number of rows on the panel may
            // be differetnx
            vScrollBar.Maximum = 47 - (panelDailyView.ClientRectangle.Size.Height / PanelRowHeight);
            if (vScrollBar.Value == oldMax)
            {
                vScrollBar.Value = vScrollBar.Maximum;
            }
            // Force a repaint of the panel
            panelDailyView.Invalidate();
        }

        private void panelDailyView_MouseMove(object sender, MouseEventArgs e)
        {
            int y = e.Y / PanelRowHeight;
            if (Utility.ConvertRowToDateTime(monthCalendar.SelectionRange.Start, y + vScrollBar.Value) < DateTime.Now)
            {
                _HighlightedRow = -1;
            }
            else
            {
                _HighlightedRow = y + vScrollBar.Value;
            }

            if (_HighlightedRow != _MousePos && _HighlightedRow != -1)
            {
                panelDailyView.Invalidate();
            }
            _MousePos = _HighlightedRow;
        }

        private void panelDailyView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // See if we are on an appointment. If
                // so, display the context menu.
                IAppointment appointment = CheckForAppointment(e);
                //Disable the ability to Edit past Appointments.
                if ((appointment != null && appointment.Start > DateTime.Now) || 
                    (appointment is RecurringAppointment && monthCalendar.SelectionRange.Start > DateTime.Now.Date))
                {
                    _SelectedAppointment = appointment;
                    contextMenuStrip.Show(panelDailyView, new Point(e.X, e.Y));
                }
            }
            else
            {
                // Calculate the new selected row and force
                // a repaint of the panel
                int y = e.Y / PanelRowHeight;
                _SelectedRow = y + vScrollBar.Value;
                panelDailyView.Invalidate();
            }
        }

        private void panelDailyView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IAppointment appointment = CheckForAppointment(e);
            //Can't edit past appointments(At all)
            if (appointment != null && appointment.Start > DateTime.Now)
            {
                AddEdit(appointment);
            }
            else
            {
                if (_HighlightedRow != -1)
                {
                    NewAppointment();
                }
            }
            panelDailyView.Invalidate();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            panelDailyView.Invalidate();
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            labelDisplayedDate.Text = monthCalendar.SelectionRange.Start.ToLongDateString();
            GetAppointmentsOnSelectedDate(monthCalendar.SelectionRange.Start);
            _HighlightedRow = -1;
            //Disable Buttons
            if(monthCalendar.SelectionRange.Start < DateTime.Now.Date)
            {
                buttonNewAppt.Enabled = false;
                buttonNewReccuringAppt.Enabled = false;
            }
            else
            {
                buttonNewAppt.Enabled = true;
                buttonNewReccuringAppt.Enabled = true;
            }
            panelDailyView.Invalidate();
        }

        private void buttonNewAppt_Click(object sender, EventArgs e)
        {
            NewAppointment();
        }

        private void newAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewAppointment();
        }

        private void buttonNewReccuringAppt_Click(object sender, EventArgs e)
        {
            NewRecurringAppointment();
        }

        private void newRecurringAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewRecurringAppointment();
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEdit(_SelectedAppointment);
            panelDailyView.Invalidate();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_SelectedAppointment.GetType() == typeof(RecurringAppointment))
            {
                DialogResult dialogResult = MessageBox.Show("Delete all recurring appointments?", "Delete Recurring", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    RecurringAppointment comparison = (RecurringAppointment)_SelectedAppointment;
                    comparison.OccuringDates.Remove(monthCalendar.SelectionRange.Start.Date);

                }
                else if (dialogResult == DialogResult.Yes)
                {
                    _Appointments.Remove(_SelectedAppointment);
                }

            }
            else
            {
                _Appointments.Remove(_SelectedAppointment);
            }
            _SelectedAppointment = null;
            GetAppointmentsOnSelectedDate(monthCalendar.SelectionRange.Start);
            panelDailyView.Invalidate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Appointments.Save();
        }

        private void panelDailyView_MouseLeave(object sender, EventArgs e)
        {
            _HighlightedRow = -1;
            panelDailyView.Invalidate();
            monthCalendar.Focus();
        }

        //Changing the mousewheel behaviour.
        private void panelDailyView_MouseEnter(object sender, EventArgs e)
        {
            vScrollBar.Focus();
        }

        //Detect if over a Control. 
        //Used instead of Button Hover because a Disabled control cannot have events associated with it.
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = GetChildAtPoint(e.Location);
            if (control != null && control.GetType() == typeof(Button) && control.Enabled == false)
            {
                if (!_ToolTipActive)
                {
                    _DisabledTooltip.Show("Cannot add appointments in the past", control, control.Width / 2, control.Height / 2);
                    _ToolTipActive = true;
                }
            }
            else
            {
                _DisabledTooltip.Hide(this);
                _ToolTipActive = false;
            }
        }

        #endregion
         
        #region Private Methods

       private void DisplayNotification(object sender, EventArgs e)
       {
           List<string> descriptions = new List<string>();
           //This will now occur every 30 minutes, the initial will set it to a 20 or 50 minute time, this will ensure this is kept.
           _Notifications.Interval = (int)TimeSpan.FromMinutes(30).TotalMilliseconds;
           //Adding to a list of descriptions ensures that we can utilize the multiple overlapping notifications.
            foreach(IAppointment apt in _Appointments)
            {
                if(apt.Start.Date == DateTime.Now.Date && apt.Start.Hour == DateTime.Now.AddMinutes(10).Hour && apt.Start.Minute == DateTime.Now.AddMinutes(10).Minute)
                {
                    descriptions.Add(apt.DisplayableDescription + " ");
                }
            }

            string allDescriptions = String.Join("and ", descriptions);
            Notification notifier = new Notification(allDescriptions + "start in 10 minutes", true);
            DateTime timeOpened = DateTime.Now;
            if (descriptions.Count() > 0 && notifier.ShowDialog() == DialogResult.OK)
            {
                TimeSpan difference = timeOpened - DateTime.Now;
                //As long as the event hasn't already happened then set another reminder for x amount of minutes
                //where X is the time between now and when you opened the form erased from 10 minutes.
                if (timeOpened - DateTime.Now < TimeSpan.FromMinutes(10))
                {
                    int check = (int)TimeSpan.FromMinutes(10).Add(-(DateTime.Now - timeOpened)).TotalMilliseconds;
                    LastReminder(allDescriptions, (int)TimeSpan.FromMinutes(10).Add(-(DateTime.Now - timeOpened)).TotalMilliseconds);
                }
                notifier.Close();
            }
            else
            {
                notifier.Close();
            }
        }

       public async void LastReminder(string apts, int timeoutInMilliseconds)
       {
           await Task.Delay(timeoutInMilliseconds);
           Notification notifier = new Notification(apts +" is starting", false);
           if(notifier.ShowDialog() == DialogResult.Cancel)
           {
               notifier.Close();
           }
       }

        // Replace the contents of TodaysAppointments with
        // the appointments for the specified date
        private void GetAppointmentsOnSelectedDate(DateTime date)
        {
            _TodaysAppointments.Clear();
            foreach (IAppointment appointment in _Appointments.GetAppointmentsOnDate(date))
            {
                _TodaysAppointments.Add(appointment);
            }
        }
        
        //A lot of this would have been copied, so I thought it prudent to put it in it's own method.
        private bool AddEdit(IAppointment apt)
        {
            //We have to ensure that it's a new appointment before changing the start time.
            if(apt.Start < DateTime.Now && !_Appointments.Contains(apt))
            {
                //Set the Datetime as Now (This will be fixed in the class to be a 30min interval).
                ((Appointment)apt).Start = DateTime.Now;
                //Alert User that you can't add an appointment before the selected date
                //Alternately 
            }

            AppointmentForm editForm;
            if (apt.GetType() == typeof(RecurringAppointment))
            {
                editForm = new RecurringForm();
            }
            else
            {
                editForm = new AppointmentForm();
            }
            //Necessary to determine the appropriate times that aren't overlapped. 
            //Used to edit the Datasource of Length and StartTime
            //editForm.Apts = _Appointments;
            editForm.Apt = apt;

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                editForm.Close();
                return true;
            }
            else
            {
                editForm.Close();
                return false;
            }
        }

        //Determines how many times a rectangle overlaps each other, and is used to determine X-Location and Size of the rectanges.
        private int RectangleOverlappingAmount(List<KeyValuePair<IAppointment, Rectangle>> check, Dictionary<IAppointment, Rectangle> checkagainst)
        {
            int max = 0;
            foreach(var app in check)
            {
                var overlapped = _UiRect.Select(a => a).Where(a => a.Value.IntersectsWith(checkagainst[app.Key])).ToList();
                if (overlapped.Count() > max)
                {
                    max = overlapped.Count();
                }
            }
            return max;

        }

        //This is the method called from PanelViewPaint that resizes rectangles.
        private void OverlappingRectangles()
        {
            //Necessary because you can't edit an enumerator during a foreach loop.
            List<IAppointment> keys = new List<IAppointment>(_UiRect.Keys);
            //Necesarry to check against the unmodified rectangles (To see where they overlap).
            Dictionary<IAppointment, Rectangle> uiRectclone = new Dictionary<IAppointment, Rectangle>(_UiRect);
            int x_loc = 50;
            //Has to be sorted by date.
            keys.Sort((a, b) => a.Start.TimeOfDay.CompareTo(b.Start.TimeOfDay));

            foreach(IAppointment appointment in keys)
            {
                //Gets all the rectangles this rectangle overlaps with.
                var overlapped = _UiRect.Select(a => a).Where(a => a.Value.IntersectsWith(uiRectclone[appointment])).ToList();
                //Then determines the size these should be, 1 being the minimum.
                int max = Math.Max(1,RectangleOverlappingAmount(overlapped, uiRectclone));
                int new_width = _UiRect[appointment].Width / max;
                _UiRect[appointment] = new Rectangle(x_loc >= (panelDailyView.Width-100) ? 50 : x_loc, _UiRect[appointment].Y, new_width, _UiRect[appointment].Height);
                x_loc = max > 1 && (panelDailyView.Width-50)>=_UiRect[appointment].X ? _UiRect[appointment].Width + _UiRect[appointment].X: 50;
            }
        }

        //This was slightly modified, instead of returning what appointment was on the row, it instead see which appointment the e.x and e.y intersects with (It's rectangle.) 
        private IAppointment CheckForAppointment(MouseEventArgs e)
        {
            bool matchFound = false;
            IAppointment appointment = null;

            if (e.X < ApptOffset ||
                e.X > panelDailyView.ClientRectangle.Size.Width - vScrollBar.Width - ApptOffset)
            {
                return null;
            }

            IEnumerator<IAppointment> enumerator = _TodaysAppointments.GetEnumerator();
            while (enumerator.MoveNext() && !matchFound)
            {
                if(_UiRect.Count()>0 && _UiRect.ContainsKey(enumerator.Current) && _UiRect[enumerator.Current].Contains(e.Location))
                {
                    matchFound = true;
                    appointment = enumerator.Current;
                }
            }
            
            return appointment;
        }

        private void NewAppointment()
        {
            Appointment newAppointment = new Appointment(Utility.ConvertRowToDateTime(monthCalendar.SelectionRange.Start, _SelectedRow));
            if (AddEdit(newAppointment))
            {
                try
                {
                    _Appointments.Add(newAppointment);
                }
                catch(ItemsException e)
                {
                    MessageBox.Show(this, e.Message, "Error adding Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            GetAppointmentsOnSelectedDate(monthCalendar.SelectionRange.Start);
            panelDailyView.Invalidate();
        }

        private void NewRecurringAppointment()
        {
            RecurringAppointment newRecurring = new RecurringAppointment(Utility.ConvertRowToDateTime(monthCalendar.SelectionRange.Start, _SelectedRow));
            if(AddEdit(newRecurring))
            {
                try
                {
                    _Appointments.Add(newRecurring);
                    //Add Here. 
                    foreach(DateTime date in new List<DateTime>(newRecurring.OccuringDates))
                    {
                        try
                        {
                            var _TestApt = new Appointment(date + newRecurring.Start.TimeOfDay);
                            _TestApt.Length = newRecurring.Length;
                            _Appointments.Add(_TestApt);
                            _Appointments.Remove(_TestApt);
                        }
                        catch (ItemsException e)
                        {
                            MessageBox.Show("Recurring Appointment at " + date.ToShortDateString() + " overlaps too many appointments. Still adding the remaining.", "Recurring Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            newRecurring.OccuringDates.Remove(date);
                        }
                    }
                }
                catch (ItemsException e)
                {
                    MessageBox.Show(this, e.Message, "Error adding Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            panelDailyView.Invalidate();
            GetAppointmentsOnSelectedDate(monthCalendar.SelectionRange.Start);
        }
        #endregion
    }
}