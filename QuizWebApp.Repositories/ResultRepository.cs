using Microsoft.Data.SqlClient;
using QuizWebApp.Types;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QuizWebApp.Repositories
{
    public class ResultRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection connection;

        public ResultRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertResult(QuizWebApp.Types.Result result)
        {
            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spInsertResult", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = result.UserName;
                    cmd.Parameters.Add("@QuizDate", SqlDbType.DateTime2).Value = result.QuizDate;
                    cmd.Parameters.Add("@Score", SqlDbType.Decimal,5).Value = result.Score;
                    cmd.Parameters["@Score"].Precision = 18;
                    cmd.Parameters["@Score"].Scale = 8;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                    }
                }
            }
        }

        public List<QuizWebApp.Types.Result> GetResultsByUserName(string userName)
        {
            List<QuizWebApp.Types.Result> results = new List<QuizWebApp.Types.Result>();

            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spGetResultsByUserName", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // New Quiz Object to add to list per row in DB
                                QuizWebApp.Types.Result result = new QuizWebApp.Types.Result();

                                // Set Quiz members to database values
                                result.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                                result.QuizDate = reader.GetDateTime(reader.GetOrdinal("QuizDate"));
                                result.Score = reader.GetDecimal(reader.GetOrdinal("Score"));

                                // Append quiz to list after read
                                results.Add(result);
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
    
}
