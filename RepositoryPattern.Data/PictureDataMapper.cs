﻿using RepositoryPattern.Model.Media;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RepositoryPattern.Data
{
    public class PictureDataMapper : AbstractDataMapper<Picture>,IPictureRepository
    {
        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override Picture Map(dynamic result)
        {
            var picture = new Picture
            {
                ID = result.pic_id,
                PictureBinary = result.pic_picture_binary,
                MimeType = result.pic_mime_type,
                SeoFileName = result.pic_seo_filename,
                IsNew = result.pic_is_new,
                FilePath = result.pic_file_path,
                AddUser = result.pic_add_user,
                AddDate = result.pic_add_date,
                ChangeDate = result.pic_change_date,
                ChangeUser = result.pic_change_user,
                DeleteFlag = result.pic_delete_flag
            };
            return picture;
        }

        public IEnumerable<Picture> GetAllPicture()
        {
            throw new NotImplementedException();
        }

        public void Add(Picture item)
        {
            var param = new
            {
                PictureBinary = item.PictureBinary,
                MimeType = item.MimeType,
                SeoFileName = item.SeoFileName,
                IsNew = item.IsNew,
                FilePath = item.FilePath,
                AddUser = "Jarheghan",
                AddDate = DateTime.Now,
                DeleteFlag = false
            };
            using (SqlCeConnection conn = Connection2)
            {
                conn.Open();
                var i = conn.Query<int>(@"INSERT INTO Picture 
                                           (pic_picture_binary,pic_mime_type,pic_seo_filename,pic_is_new,pic_file_path
                                            ,pic_add_user,pic_add_date,pic_delete_flag)
                                           VALUES(@PictureBinary,@MimeType,@SeoFileName,@IsNew,@FilePath,@AddUser,@AddDate,
                                               @DeleteFlag)
                                 select SCOPE_IDENTITY()", param);
                var j = conn.Query<int>(@"Select pic_id from picture where pic_file_path = @FilePath", new { FilePath = param.FilePath }).FirstOrDefault();
            }
        }

        public void Remove(Picture item)
        {
            throw new NotImplementedException();
        }

        public void Update(Picture item)
        {
            throw new NotImplementedException();
        }


        public Picture GetPictureById(int Id)
        {
            Picture picture = new Picture();
            using (SqlCeConnection conn = Connection2)
            {
                try
                {
                    conn.Open();
                    var pic = conn.Query<dynamic>(@"select * from picture where pic_id = @Id", new { Id = Id }).FirstOrDefault();
                    picture = Map(pic);
                    return picture;
                }
                catch { return null; }
            }
        }


        public Picture Create()
        {
            return null;
        }



        int IPictureRepository.Insert(Picture item)
        {
            var param = new
            {
                PictureBinary = item.PictureBinary,
                MimeType = item.MimeType,
                SeoFileName = item.SeoFileName,
                IsNew = item.IsNew,
                FilePath = item.FilePath,
                AddUser = "Jarheghan",
                AddDate = DateTime.Now,
                DeleteFlag = false
            };
            using (SqlCeConnection conn = Connection2)
            {
                conn.Open();
                var i = conn.Query<int>(@"INSERT INTO Picture 
                                           (pic_picture_binary,pic_mime_type,pic_seo_filename,pic_is_new,pic_file_path
                                            ,pic_add_user,pic_add_date,pic_delete_flag)
                                           VALUES(@PictureBinary,@MimeType,@SeoFileName,@IsNew,@FilePath,@AddUser,@AddDate,
                                               @DeleteFlag)", param);
                 var j = conn.Query<int>(@"select SCOPE_IDENTITY()", null).FirstOrDefault();
                return j;
            }
        }
    }
}
