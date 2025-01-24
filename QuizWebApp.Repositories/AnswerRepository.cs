using Microsoft.Data.SqlClient;
using QuizWebApp.Types;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QuizWebApp.Repositories
{
    public class AnswerRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection connection;

        public AnswerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<QuizWebApp.Types.Answer> GetAnswersByQuestionID(int questionID)
        {
            List<QuizWebApp.Types.Answer> answers = new List<QuizWebApp.Types.Answer>();

            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spGetAnswersByQuestionID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QuestionID", SqlDbType.Int).Value = questionID;

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
                                QuizWebApp.Types.Answer answer = new QuizWebApp.Types.Answer();

                                // Set Quiz members to database values
                                answer.AnswerID = reader.GetInt32(reader.GetOrdinal("AnswerID"));
                                answer.QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID"));
                                answer.Text = reader.GetString(reader.GetOrdinal("Text"));
                                answer.IsCorrect = reader.GetBoolean(reader.GetOrdinal("IsCorrect"));

                                // Append quiz to list after read
                                answers.Add(answer);
                            }
                        }
                    }
                }
            }

            return answers;
        }

        public QuizWebApp.Types.Answer GetAnswerByAnswerID(int id)
        {
            QuizWebApp.Types.Answer answer = new QuizWebApp.Types.Answer();

            // Establish connection to DB
            using (var connection = new SqlConnection(connectionString))
            {
                // Call Stored Procedure
                using (SqlCommand cmd = new SqlCommand("dbo.spGetAnswerByAnswerID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AnswerID", SqlDbType.Int).Value = id;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Set Quiz members to database values
                                answer.AnswerID = reader.GetInt32(reader.GetOrdinal("AnswerID"));
                                answer.QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID"));
                                answer.Text = reader.GetString(reader.GetOrdinal("Text"));
                                answer.IsCorrect = reader.GetBoolean(reader.GetOrdinal("IsCorrect"));

                                
                            }
                        }
                    }
                }
            }

            return answer;
        }
    }
}
