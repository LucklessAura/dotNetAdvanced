using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
    public partial class Comment
    {
        public bool AddComment()
        {
            using (Model1Container contex = new Model1Container())
            {
                bool bResult = false;
                if(this == null || this.PostPostId == 0)
                {
                    return bResult;
                }
                if(this.CommentId ==0)
                {
                    contex.Entry<Comment>(this).State = System.Data.Entity.EntityState.Added;
                    Post p = contex.Posts.Find(this.PostPostId);
                    contex.Entry<Post>(p).State = System.Data.Entity.EntityState.Unchanged;
                    contex.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }

        public Comment UpdateComment(Comment newComment)
        {
            using (Model1Container context = new Model1Container())
            {
                Comment oldComment = context.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                {
                    oldComment.Text = newComment.Text;
                }
                if ((oldComment.PostPostId != newComment.PostPostId) && (newComment.PostPostId !=0))               
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }
                context.SaveChanges();
                return oldComment;
            }
        }


        public Comment GetCommentById(int id)
        {
            using (Model1Container context = new Model1Container())
            {
                var items = from c in context.Comments where c.CommentId == id select c;
                return items.Include(p => p.Post).SingleOrDefault();
            }
        }
    }
}
