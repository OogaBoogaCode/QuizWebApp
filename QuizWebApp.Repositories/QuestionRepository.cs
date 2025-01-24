using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QuizWebApp.Repositories
{
    public class QuestionRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection connection;

        public QuestionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<QuizWebApp.Types.Question> GetQuestionsByQuizID(int quizID)
        {
            List<QuizWebApp.Types.Question> questions = new List<QuizWebApp.Types.Question>();

            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spGetQuestionsByQuizID",connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QuizID", SqlDbType.Int).Value = quizID;

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
                                QuizWebApp.Types.Question question = new QuizWebApp.Types.Question();

                                // Set Quiz members to database values
                                question.QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID"));
                                question.QuizID = reader.GetInt32(reader.GetOrdinal("QuizID"));
                                question.Text = reader.GetString(reader.GetOrdinal("Text"));

                                // Append quiz to list after read
                                questions.Add(question);
                            }
                        }
                    }
                }
            }

            return questions;
        }
    }
}
