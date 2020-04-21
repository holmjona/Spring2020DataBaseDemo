using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseDemo {
    static class DAL {
        static String connectString = "Data Source=localhost;Initial Catalog=SuperHeroes;User ID=db_reader;Password=password";
        static String editConnectString = "Data Source=localhost;Initial Catalog=SuperHeroes;User ID=db_writer;Password=password";

        public static List<SuperHero> GetAllSuperHeroes() {
            List<SuperHero> retList = new List<SuperHero>();

            // Get data from database
            SqlConnection conn = null;

            try {
                // Create Connection
                conn = new SqlConnection();
                //conn.ConnectionString = "Server=localhost;Database=SuperHeroes;User Id=holmjona;Password=password;";
                conn.ConnectionString = connectString;

                // Open Connection
                conn.Open();
                //tbOutput.Text = "I connected";

                // Create Command
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                //comm.CommandType = System.Data.CommandType.Text;
                //comm.CommandText = "SELECT * FROM SuperHeroes";
                comm.CommandText = "sprocSuperHeroesGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

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
                    retList.Add(sup);
                }

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } finally {
                // Close Connection
                if (conn != null) conn.Close();
            }
            return retList;
        }


        public static SuperHero GetSuperHero(int id) {
            SuperHero sup = null;
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(connectString);

                // Dynamically Dangerous!!
                //string sql = "SELECT * FROM SuperHeroes " +
                //    "WHERE SuperHeroID = " + id; // -1 OR "boo" = "boo" --
                //SqlCommand comm = new SqlCommand(sql,conn);

                // With Parameterization
                //string sql = "SELECT * FROM SuperHeroes " +
                //   "WHERE SuperHeroID = @ID"; 
                //SqlCommand comm = new SqlCommand(sql, conn);
                SqlCommand comm = new SqlCommand(
                    "sprocSuperHeroGet", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SuperHeroID", id);

                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read()) {
                    sup = GetSuperHero(dr);
                }

            }catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } finally {
                if (conn != null) conn.Close();
            }
            return sup;
        }

        private static SuperHero GetSuperHero(SqlDataReader dr) {
            SuperHero retSup = new SuperHero();
            retSup.ID = (int)dr["SuperHeroID"];
            retSup.FirstName = (string)dr["FirstName"];
            retSup.LastName = (string)dr["LastName"];
            retSup.Height = (decimal)dr["HeightInInches"];
            retSup.DateOfBirth = (DateTime)dr["DateOfBirth"];
            
            return retSup;
        }

        internal static int UpdateSuperHero(SuperHero superPerson) {
            int numberOfRowsAffected = 0;
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(editConnectString);

                // With Parameterization
                //string sql = "UPDATE SuperHeroes SET " +
                //        "FirstName = @FirstName, " +
                //        "LastName = @LastName, " +
                //        "HeightInInches = @Height " +
                //    "WHERE SuperHeroID = @ID";
                //SqlCommand comm = new SqlCommand(sql, conn);
                SqlCommand comm = new SqlCommand("sproc_SuperHeroUpdate",
                    conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                //comm.Parameters.AddWithValue("@FirstName", superPerson.FirstName);
                //comm.Parameters.AddWithValue("@LastName", superPerson.LastName);
                //comm.Parameters.AddWithValue("@Height", superPerson.Height);
                //comm.Parameters.AddWithValue("@ID", superPerson.ID);
                comm.Parameters.AddWithValue("@SuperHeroID", superPerson.ID);
                comm.Parameters.AddWithValue("@FirstName", superPerson.FirstName);
                comm.Parameters.AddWithValue("@LastName", superPerson.LastName);
                comm.Parameters.AddWithValue("@DateOfBirth", superPerson.DateOfBirth);
                comm.Parameters.AddWithValue("@EyeColor", 0);
                comm.Parameters.AddWithValue("@HeightInInches", superPerson.Height);
                comm.Parameters.AddWithValue("@AlterEgoID", superPerson.ID);
                comm.Parameters.AddWithValue("@SideKickID", 0);
                comm.Parameters.AddWithValue("@CostumeID", 1);

                comm.Connection.Open();
                numberOfRowsAffected = comm.ExecuteNonQuery();

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } finally {
                if (conn != null) conn.Close();
            }
            return numberOfRowsAffected;
        }


    }
}
