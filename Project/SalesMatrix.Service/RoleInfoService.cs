using SalesMatrix.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SalesMatrix.Service.Model
{
    public class RoleInfoService
    {
        private _SalesMatrixDB db = new _SalesMatrixDB();

        public void Create(Role role)
        {
            if (role.RoleName != null && role.RoleName != "")
            {
                if (db.Roles.FirstOrDefault(r => r.RoleName == role.RoleName) == null)
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    var ipList = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                    var ipAddress = ipList[0].ToString();

                    role.CreatedBy = null;
                    role.CreatedDate = DateTime.UtcNow;
                    role.CreatedFrom = ipAddress;
                    db.Roles.Add(role);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This role already exist");
                }
            }
            else
            {
                throw new Exception("Role can't be empty");
            }
        }

        public void Update(Role role)
        {
            if (role.RoleName != null && role.RoleName != "")
            {
                if (db.Roles.FirstOrDefault(r => r.RoleName == role.RoleName && r.RoleId!=role.RoleId) == null)
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    var ipList = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                    var ipAddress = ipList[0].ToString();

                    role.ModifiedBy = null;
                    role.ModifiedDate = DateTime.UtcNow;
                    role.ModifiedFrom = ipAddress;

                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This role already exist");
                }
            }
            else
            {
                throw new Exception("Role can't be empty");
            }
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
        }

        public List<dynamic> GetAll()
        {
            var list = (from r in db.Roles
                       select new
                       {
                           r.RoleId,
                           r.RoleName,
                           r.Description,
                           r.Status,
                           r.CreatedBy,
                           r.CreatedDate,
                           r.CreatedFrom,
                           r.ModifiedBy,
                           r.ModifiedDate,
                           r.ModifiedFrom,
                           Users = (from u in db.Users where u.RoleId == r.RoleId
                                                                            select new
                                                                            { u.UserId,u.UserName,u.FullName,u.Email,u.Status,u.Password,u.Picture,
                                                                              u.CreatedBy,u.CreatedDate,u.CreatedFrom,u.ModifiedBy,
                                                                              u.ModifiedDate,u.ModifiedFrom
                                                                            }).ToList()
                       }).ToList();
            return list.ToList<dynamic>();
        }

        public List<dynamic> GetAllActiveRole()
        {
            var list = (from r in db.Roles.Where(r=>r.Status==true)
                        select new
                        {
                            r.RoleId,
                            r.RoleName,
                            r.Description,
                            r.Status,
                            r.CreatedBy,
                            r.CreatedDate,
                            r.CreatedFrom,
                            r.ModifiedBy,
                            r.ModifiedDate,
                            r.ModifiedFrom,
                            Users = (from u in db.Users
                                     where u.RoleId == r.RoleId
                                     select new
                                     {
                                         u.UserId,
                                         u.UserName,
                                         u.FullName,
                                         u.Email,
                                         u.Status,
                                         u.Password,
                                         u.Picture,
                                         u.CreatedBy,
                                         u.CreatedDate,
                                         u.CreatedFrom,
                                         u.ModifiedBy,
                                         u.ModifiedDate,
                                         u.ModifiedFrom
                                     }).ToList()
                        }).ToList();
            return list.ToList<dynamic>();
        }

        public List<dynamic> GetAllInactiveRole()
        {
            var list = (from r in db.Roles.Where(r => r.Status == false)
                        select new
                        {
                            r.RoleId,
                            r.RoleName,
                            r.Description,
                            r.Status,
                            r.CreatedBy,
                            r.CreatedDate,
                            r.CreatedFrom,
                            r.ModifiedBy,
                            r.ModifiedDate,
                            r.ModifiedFrom,
                            Users = (from u in db.Users
                                     where u.RoleId == r.RoleId
                                     select new
                                     {
                                         u.UserId,
                                         u.UserName,
                                         u.FullName,
                                         u.Email,
                                         u.Status,
                                         u.Password,
                                         u.Picture,
                                         u.CreatedBy,
                                         u.CreatedDate,
                                         u.CreatedFrom,
                                         u.ModifiedBy,
                                         u.ModifiedDate,
                                         u.ModifiedFrom
                                     }).ToList()
                        }).ToList();
            return list.ToList<dynamic>();
        }

        public Role GetById(int id)
        {
            return db.Roles.Find(id);
        }

        public bool RoleExists(string name)
        {
            return db.Roles.Count(e => e.RoleName == name) > 0;
        }

        public bool RoleExistsForEdit(string name, int id)
        {
            return db.Roles.Count(e => e.RoleName == name && e.RoleId != id) > 0;
        }
    }
}
