using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QuizWebApp.Repositories
{
    public class QuizRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection connection;

        public QuizRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<QuizWebApp.Types.Quiz> GetQuizzes()
        {
            List<QuizWebApp.Types.Quiz> quizzes = new List<QuizWebApp.Types.Quiz>();

            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spGetAllQuizzes", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                                QuizWebApp.Types.Quiz quiz = new QuizWebApp.Types.Quiz();

                                // Set Quiz members to database values
                                quiz.QuizID = reader.GetInt32(reader.GetOrdinal("QuizID"));
                                quiz.Name = reader.GetString(reader.GetOrdinal("Name"));

                                // Nullable DB values much be checked
                                string Description = reader.GetString(reader.GetOrdinal("Description"));

                                // Check for DBNull
                                if (Description is not null)
                                {
                                    quiz.Description = Description;
                                }
                                
                                // Append quiz to list after read
                                quizzes.Add(quiz);
                            }
                        }
                    }
                }
            }

            return quizzes;
        }
    }
}
