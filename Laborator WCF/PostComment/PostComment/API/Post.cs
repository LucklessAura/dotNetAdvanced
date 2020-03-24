using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
    public partial class Post
    {
        public bool AddPost()
        {
            using (Model1Container context = new Model1Container())
            {
                bool bResult = false;
                if (this.PostId == 0)
                {
                    var it = context.Entry<Post>(this).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }

        }

        public Post UpdatePost(Post newPost)
        {
            using (Model1Container context = new Model1Container())
            {
                Post oldPost = context.Posts.Find(newPost.PostId);
                if(oldPost == null)
                {
                    return null;

                }
                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                context.SaveChanges();
                return oldPost;
            }
        }

        public int DeletePost(int id)
        {
            using (Model1Container context = new Model1Container())
            {
                return context.Database.ExecuteSqlCommand("Delete from Post where postid = @p0", id);
            }
        }

        public Post GetPostById(int id)
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

        public List<Post> GetAllPosts()
        {
            using (Model1Container context = new Model1Container())
            {
                return context.Posts.Include("Comments").ToList<Post>();
            }
        }
    }
}
