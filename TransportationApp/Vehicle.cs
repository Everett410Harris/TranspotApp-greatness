using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TransportationApp
{
    public partial class Vehicle : Form
    {
        public Vehicle()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tech Louisville\Documents\transApp.mdf;Integrated Security=True;Connect Timeout=30");
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(LPlateTb.Text == "" || MakeCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MileageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {  
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTbl (VLp,VMake,VModel,VYear,VEngType,VColor,VMileage,VType,Booked) values(@VLp,@VMa,@VMo,@VY,@VEng,@VCo,@VMi,@VTy,@VB)", Con);
                    cmd.Parameters.AddWithValue("@VLp", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@VMa", MakeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VMo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MileageTb.Text);
                    cmd.Parameters.AddWithValue("@VTy", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");

                    Con.Close();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }
    }
}
