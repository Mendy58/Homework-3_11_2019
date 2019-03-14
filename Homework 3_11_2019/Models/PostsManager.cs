using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Homework_3_11_2019.Models
{
    public class PostsManager
    {
        private string _connectionstring;
        public PostsManager(string _ConnectionString)
        {
            _connectionstring = _ConnectionString;
        }

        public List<Post> GetPosts()
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Post";
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Post> posts = new List<Post>();
            while(reader.Read())
            {
                posts.Add(new Post
                {
                    Id = (int)reader["Id"],
                    Name =(string)reader["Name"],
                    Description=(string)reader["Description"],
                    DateCreated=(DateTime)reader["DateCreated"],
                    PhoneNumber=(long)reader["PhoneNumber"]
                });
            }
            con.Close();
            con.Dispose();
            return posts;
        }
        public void DeletePost(int id)
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"delete from Post
                                where Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public int InsertPost(Post post)
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"insert into Post
                                Values(@Name,@Description,@Date,@Phone)
                                select SCOPE_IDENTITY() as [scope identity]";
            cmd.Parameters.AddWithValue("@Name", post.Name);
            cmd.Parameters.AddWithValue("@Description", post.Description);
            cmd.Parameters.AddWithValue("@Date", post.DateCreated);
            cmd.Parameters.AddWithValue("@Phone", post.PhoneNumber);
            con.Open();
            int id = (int)(decimal)cmd.ExecuteScalar();
            con.Close();
            con.Dispose();
            return id;
        }
    }
}