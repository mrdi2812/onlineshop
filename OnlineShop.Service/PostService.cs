using System;
using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System.Linq;
using OnlineShop.Common;

namespace OnlineShop.Service
{
    public interface IPostService
    {
        Post Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAll(string keyword);

        IEnumerable<Post> GetReatePosts(int id, int top);

        IEnumerable<Tag> GetListTagByPostID(int id);

        IEnumerable<Comment> GetListCommentByPostID(int id);

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);
       
        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();

        Tag GetTag(string tagId);

    }

    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        ITagRepository _tagRepository;
        IPostTagRepository _postTagRepository;
        ICommentRepository _commentRepository;
        IUnitOfWork _unitOfWork;


        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository, ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
            this._postTagRepository = postTagRepository;
            this._tagRepository = tagRepository;
            this._commentRepository = commentRepository;
        }

        public Post Add(Post post)
        {
            var newPost = _postRepository.Add(post);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.PostTag;
                        _tagRepository.Add(tag);
                    }

                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;
                    _postTagRepository.Add(postTag);
                }
            }
            return newPost;
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            //return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
            var query = _postRepository.GetMulti(x => x.Status && x.CategoryID == categoryId).OrderByDescending(x=>x.CreatedDate);         
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all post by tag
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);

        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _postTagRepository.DeleteMulti(x => x.PostID == post.ID);
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;
                    _postTagRepository.Add(postTag);
                }
            }
        }

        public IEnumerable<Tag> GetListTagByPostID(int id)
        {
            return _postTagRepository.GetMulti(x => x.PostID == id, new string[] { "Tag" }).Select(m => m.Tag);
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }
        public IEnumerable<Post> GetReatePosts(int id, int top)
        {
            var post = _postRepository.GetSingleById(id);
            return _postRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == post.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Comment> GetListCommentByPostID(int id)
        {
            return _commentRepository.GetMulti(x => x.PostId == id);
        }

    }
}