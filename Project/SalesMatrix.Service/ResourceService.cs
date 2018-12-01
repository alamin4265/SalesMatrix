using SalesMatrix.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SalesMatrix.Service
{
    public class ResourceService
    {
        private _SalesMatrixDB db = new _SalesMatrixDB();

        public void Create(Resource resource)
        {
            if (resource.ResourceName != null && resource.ResourceName != "" && resource.ResourceType != null && resource.ResourceType != "" && resource.Description != null && resource.Description != "" && resource.Parent != null && resource.Parent != "" && resource.Sequence != null && resource.Sequence != "" && resource.Module != null && resource.Status != null)
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                var ipList = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                var ipAddress = ipList[0].ToString();

                resource.CreatedBy = null;
                resource.CreatedDate = DateTime.UtcNow;
                resource.CreatedFrom = ipAddress;
                db.Resources.Add(resource);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Required name can't be empty");
            }
        }
    }
}
