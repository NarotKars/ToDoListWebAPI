using ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dbSettings.DataAccess
{
    public class ToDoDAL
    {
        public void InsertToDo(ToDo sth)
        {
            string sql = "INSERT INTO ToDo(WhatToDo, Edit, Completed)";
            sql += $" VALUES('{sth.WhatToDo}' , '{sth.Edit}', '{sth.Completed}')";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }
            }
        }

        public ToDo ReadToDo(int id)
        {
            ToDo item=new ToDo();
            string sql= "SELECT Id, WhatToDo FROM ToDo WHERE Id = '{0}'";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection=new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(string.Format(sql,id),connection))
                    {
                        connection.Open();
                        using(SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while(dataReader.Read())
                            {
                                int i=0;
                                item.Id=dataReader.GetInt32(i++);
                                item.WhatToDo=dataReader.GetString(i++);
                                item.Edit=dataReader.GetString(i++);
                                item.Completed=dataReader.GetString(i++);
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }

            return item;
        }

        public void DeleteToDo(int id)
        {
            string sql="DELETE FROM ToDo WHERE Id = '{0}'";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection=new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(string.Format(sql,id),connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }
            }
            
        }
        public List<ToDo> GetToDosAsGenericList()
        {
            List<ToDo> users = new List<ToDo>();
            string sql = "SELECT Id, WhatToDo, Edit, Completed FROM ToDo";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dataReader.Read())
                            {
                                users.Add(new ToDo
                                {
                                    Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                    WhatToDo = dataReader.GetString(dataReader.GetOrdinal("WhatToDo")),
                                    Edit=dataReader.GetString(dataReader.GetOrdinal("Edit")),
                                    Completed=dataReader.GetString(dataReader.GetOrdinal("Completed"))
                                });
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
            return users;
        }
        public void updateToDo(ToDo sth)
        {   
            string sql="UPDATE ToDo " +
                       "SET WhatToDo = @WhatToDo, Edit = @Edit, Completed =@Completed "+
                       "Where Id=@Id";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();    
                        command.Parameters.Add("@Id",SqlDbType.Int).Value=sth.Id;
                        command.Parameters.Add("@WhatToDo", SqlDbType.VarChar).Value=sth.WhatToDo;
                        command.Parameters.Add("@Edit", SqlDbType.VarChar).Value=sth.Edit;
                        command.Parameters.Add("@Completed", SqlDbType.VarChar).Value=sth.Completed;
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                    
                }
            }
        }
    }
}