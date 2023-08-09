using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BloggingApplication
{
    public class CommentManager
    {
        readonly ApplicationContext _Db;
        public CommentManager(ApplicationContext Db)
        {
            _Db = Db;
        }
        public async Task CreateComment(Comment CommentToBeAdded)
        {   
            try
            {
                await _Db.Comment.AddAsync(CommentToBeAdded); 
                await _Db.SaveChangesAsync();
            }
            catch (Exception ex)
            {   
                Console.WriteLine("error");
                Console.WriteLine(ex.GetBaseException().Message);
                
            }         
        }
        public async Task<List<Comment>> GetComments()
        {   
            try
            {

            return await _Db.Comment.ToListAsync();
            }

            catch
            {
                return new List<Comment>();
            }
        }
        public async Task UpdateComment(Comment UpdateInfo)
        {   

            try
            {

                Comment? CommentToBeUpdated = await _Db.Comment.FindAsync(UpdateInfo.CommentId);

                if (CommentToBeUpdated != null)
                {
                    if (CommentToBeUpdated.Text != null)
                    {
                        CommentToBeUpdated.Text = UpdateInfo.Text;
                    }
                    await _Db.SaveChangesAsync();
                }

                else 
                {
                    Console.WriteLine("No such Post Exists");   
                }             
            }

            catch
            {
                Console.WriteLine("Unable to perform Operation");
            }
        
        }
        public async Task DeleteComment(int Id)
        {   
            try
            {

                var QueryString = from comment in _Db.Comment where comment.CommentId == Id select comment;
                Comment CommentToBeDeleted = QueryString.First();
                
                if (CommentToBeDeleted != null)
                {
                    _Db.Comment.Remove(CommentToBeDeleted);
                    await _Db.SaveChangesAsync();
                }

                else
                {
                    Console.WriteLine("No such Post Exists");
                }
            }

            catch
            {
                Console.WriteLine("Unable to Delete the comment");
            }


        }
    }
}