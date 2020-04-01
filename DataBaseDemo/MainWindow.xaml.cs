using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace DataBaseDemo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) {
            // Get data from database
            SqlConnection conn = null;

            try {
                // Create Connection
                conn = new SqlConnection();
                //conn.ConnectionString = "Server=localhost;Database=SuperHeroes;User Id=holmjona;Password=password;";
                conn.ConnectionString = "Data Source=localhost;Initial Catalog=SuperHeroes;Integrated Security=True";

                // Open Connection
                conn.Open();
                //tbOutput.Text = "I connected";

                // Create Command
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = "SELECT * FROM SuperHeroes";

                // Execute Command
                SqlDataReader dr = comm.ExecuteReader();

                // Read Results

                // FIll from Database
                while (dr.Read()) {
                    SuperHero sup = new SuperHero();
                    sup.ID = (int)dr["SuperHeroID"];
                    sup.FirstName = (string)dr["FirstName"];
                    sup.LastName = (string)dr["LastName"];
                    sup.Height = (decimal)dr["HeightInInches"];
                    lbSupers.Items.Add(sup);
                }

                

            } catch (Exception ex) {
                tbOutput.Text = "Error: " + ex.Message;
            } finally {
                // Close Connection
                if(conn != null) conn.Close();
            }
        }
    }
}
