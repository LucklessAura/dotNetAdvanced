using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment.APIstatic
{
    public static class API
    {
        public static bool AddPost(Post post)
        {
            using (Model1Container context = new Model1Container())
            {
                bool bResult = false;
                if(post.PostId == 0)
                {
                    var it = context.Entry<Post>(post).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }

        public static int DeletePost(int id)
        {
            using (Model1Container context = new Model1Container())
            {
                return context.Database.ExecuteSqlCommand("Delete from Post where postid = @p0", id);
            }
        }

        public static Post GetPostById(int id)
        {
            using (Model1Container context = new Model1Container())
            {
                var items = from p in context.Posts where p.PostId == id select p;
                if(items != null)
                {
                    return items.Include(c => c.Comments).SingleOrDefault();
                }
                return null;
            }
        }

        public static List<Post> GetAllPosts()
        {
            using (Model1Container context = new Model1Container())
            {
                return context.Posts.Include("Comments").ToList<Post>();
            }
        }



        public static bool AddComment(Comment comment)
        {
            using (Model1Container context = new Model1Container())
            {
                bool bResult = false;
                if (comment == null || comment.PostPostId == 0 )
                {
                    return bResult;
                }
                if (comment.CommentId == 0)
                {
                    context.Entry<Comment>(comment).State = EntityState.Added;
                    Post p = context.Posts.Find(comment.PostPostId);
                    context.Entry<Post>(p).State = EntityState.Unchanged;
                    context.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }

        public static Comment UpdateComment(Comment newComment)
        {
            using (Model1Container contex = new Model1Container())
            {
                Comment oldComment = contex.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                {
                    oldComment.Text = newComment.Text;
                }
                if ((oldComment.PostPostId != newComment.PostPostId) && (newComment.PostPostId != 0))
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }
                contex.SaveChanges();
                return oldComment;
            }
        }


        public static Comment GetCommentById(int id)
        {
            using (Model1Container context = new Model1Container())
            {
                var items = from c in context.Comments where c.CommentId == id select c;
                return items.Include(p => p.Post).SingleOrDefault();
            }
        }
    }


    
}
