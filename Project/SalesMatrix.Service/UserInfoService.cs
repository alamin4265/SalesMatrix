using SalesMatrix.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesMatrix.Service.Model
{
    public class UserInfoService
    {
        private _SalesMatrixDB db = new _SalesMatrixDB();

        public void Create(User user)
        {
            if (user.UserName != null && user.UserName != "" && user.FullName != null && user.FullName != "" && user.Email != null && user.Email != "" && user.Password != null && user.Password != "")
            {
                if (db.Users.FirstOrDefault(r => r.UserName == user.UserName) == null)
                {
                    user.Password = EncodePassword(user.Password);
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    var ipList = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                    var ipAddress = ipList[0].ToString();

                    user.CreatedBy = null;
                    user.CreatedDate = DateTime.UtcNow;
                    user.CreatedFrom = ipAddress;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This user already exist");
                }
            }
            else
            {
                throw new Exception("Required field can't be empty");
            }
        }

        public void Update(User user)
        {
            if (user.UserName != null && user.UserName != "" && user.FullName != null && user.FullName != "" && user.Email != null && user.Email != "" && user.Password != null && user.Password != "")
            {
                if (db.Users.FirstOrDefault(r => r.UserName == user.UserName && r.UserId != user.UserId) == null)
                {
                    user.Role = db.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    var ipList = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                    var ipAddress = ipList[0].ToString();

                    user.ModifiedBy = null;
                    user.ModifiedDate = DateTime.UtcNow;
                    user.ModifiedFrom = ipAddress;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This user already exist");
                }
            }
            else
            {
                throw new Exception("Required field can't be empty");
            }
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> GetAllActiveUser()
        {
            return db.Users.Where(u => u.Status == true).ToList();
        }
        public List<User> GetAllInactiveUser()
        {
            return db.Users.Where(u => u.Status == false).ToList();
        }
        public List<User> GetAllUserByRoleId(int roleId)
        {
            return db.Users.Where(u => u.RoleId == roleId).ToList();
        }

        public bool UserNameExists(string userName)
        {
            return db.Users.Count(e => e.UserName == userName) > 0;
        }

        public bool UserExistsForEdit(string name, int id)
        {
            return db.Users.Count(e => e.UserName == name && e.UserId != id) > 0;
        }


        public string EncodePassword(string pass)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}
